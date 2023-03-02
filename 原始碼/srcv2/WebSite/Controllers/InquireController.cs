using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Globalization;
using JamZoo.Project.WebSite.Library;

namespace JamZoo.Project.WebSite.Controllers
{
    using Service;
    using Models;
    using ViewModels;
    using Newtonsoft.Json;

    public class InquireController : BaseWebController
    {
        ParentService parentSrv = new ParentService();
        ChildService childSrv = new ChildService();
        static string KEY_LASTUPDATE = "簡易查詢的更新日期";
        static string KEY_DDL_LAST_DAY = "簡易查詢下拉日期";
        SystemService systemSrv = new SystemService();

        public ActionResult Index()
        {
            return View();
        }
        
        
        public ActionResult List(InquireListPage viewModel, string id)
        {
            if (id == null)
            {
                return RedirectToAction("List", new { id = "0" });
            }


            viewModel.LastUpdate = systemSrv.GetValueByKey(KEY_LASTUPDATE);
            viewModel.DdlLastDate = systemSrv.GetValueByKey(KEY_DDL_LAST_DAY);

            if (string.IsNullOrEmpty(viewModel.Start))
            {
                viewModel.Start = (viewModel.GetLastUpdate民國年 - 9).ToString() + "01";
            }
            if (string.IsNullOrEmpty(viewModel.End))
            {
                viewModel.End = (viewModel.GetLastUpdate民國年).ToString() + viewModel.GetLastUpdate民國月.ToString("00");
                if (id == "9")
                {
                    viewModel.End = "10912";
                }
                else
                {
                    viewModel.End = "11012";
                }

            }
            viewModel.YearOrMonth = "Y";
            var calendar = new TaiwanCalendar();
            var culture = CultureInfo.CreateSpecificCulture("zh-TW");
            culture.DateTimeFormat.Calendar = calendar;
            

            if (viewModel.Start.Equals(viewModel.End))
            {
                viewModel.ChartType = "6";
            }
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            

            if (int.Parse(viewModel.End) < int.Parse(viewModel.Start))
            {
                
                TempData["alert"] = "搜尋的結束日期必需大於開始日期";
                return RedirectToAction("List", new { Id = viewModel.Id });
            }


            ParentListModel parentList =
                parentSrv.GetList(null, new ParentListModel() { EnergyName = id });

            foreach (ParentModel parent in parentList.Data)
            {
                parent.Child = childSrv.GetList(null, new ChildListModel() { PageSize = 100, ParentId = parent.Id });
            }

            viewModel.Data = parentList;
            viewModel.LastUpdate = systemSrv.GetValueByKey(KEY_LASTUPDATE);

            return View(viewModel);
        }

        
        
        public ActionResult Detail(InquireDetailPage viewModel, string download)
        {
            viewModel.LastUpdate = systemSrv.GetValueByKey(KEY_LASTUPDATE);
            viewModel.DdlLastDate = systemSrv.GetValueByKey(KEY_DDL_LAST_DAY);

            if (string.IsNullOrEmpty(viewModel.Start))
            {
                viewModel.Start = (viewModel.GetLastUpdate民國年 - 9).ToString() + "01";
            }
            if (string.IsNullOrEmpty(viewModel.End))
            {
                viewModel.End = (viewModel.GetLastUpdate民國年).ToString() + viewModel.GetLastUpdate民國月.ToString("00");
            }

            var calendar = new TaiwanCalendar();
            var culture = CultureInfo.CreateSpecificCulture("zh-TW");
            culture.DateTimeFormat.Calendar = calendar;
            viewModel.Parent = parentSrv.Get(string.Empty, viewModel.Id);

            if (viewModel.ChartType == "6")
            {
                
                
                
                viewModel.TempStart = viewModel.Start;
                viewModel.Start = viewModel.End;
            }
            else
            {
                if (!string.IsNullOrEmpty(viewModel.TempStart))
                {
                    viewModel.Start = viewModel.TempStart;
                    viewModel.TempStart = null;
                }
                
            }

            if (int.Parse(viewModel.End) < int.Parse(viewModel.Start))
            {
                
                TempData["alert"] = "搜尋的結束日期必需大於開始日期";
                return RedirectToAction("Detail", new { Id = viewModel.Id });
            }

            
            if (InquireDetailPage.EnergyParentIdListOnlyForYear.IndexOf(viewModel.Id) != -1 && viewModel.YearOrMonth == "M")
            {
                viewModel.Start = "09801";
                viewModel.End = "10812";
                viewModel.YearOrMonth = "Y";
            }

            if (viewModel.YearOrMonth == "Y")
            {
                
                viewModel.End = viewModel.End.Substring(0, 3) + "12";
            }

            
            List<int> iii = new List<int>();
            viewModel.RowForChild = new List<ChildModel>();

            #region Init Data Table
            DataTable table = new DataTable();
            table.Columns.Add("縮排", typeof(int));
            table.Columns.Add("日期", typeof(string));
            table.Columns.Add("小數點位數", typeof(int));

            for (int i = 0; i <= viewModel.RangeTotalMonth; i++)
            {
                DateTime r1 = viewModel.RangeStart.AddMonths(i);
                string s1 = r1.ToString("yyyMM", culture);
                table.Columns.Add(s1, typeof(double));
            }

            #endregion

            
            viewModel.ChildList =
                childSrv.GetList(null, new ChildListModel() { PageSize = 100, ParentId = viewModel.Id, SelfId = "" });

            #region Child Node
            
            foreach (var Lv1Row in viewModel.ChildList.Data)
            {
                Lv1Row.IndentName = "fixed_side";
                Lv1Row.IndentNum = 0;
                Lv1Row.ChildList =
                childSrv.GetList(null, new ChildListModel() { PageSize = 100, SelfId = Lv1Row.Id });

                
                iii.Add(Lv1Row.Industryid);

                viewModel.RowForChild.Add(Lv1Row);
                
                foreach (var Lv2Row in Lv1Row.ChildList.Data)
                {
                    Lv2Row.IndentName = "table_sub_indent";
                    Lv2Row.IndentNum = 1;
                    Lv2Row.ChildList =
                        childSrv.GetList(null, new ChildListModel() { PageSize = 100, SelfId = Lv2Row.Id });

                    
                    iii.Add(Lv2Row.Industryid);


                    viewModel.RowForChild.Add(Lv2Row);
                    
                    foreach (var Lv3Row in Lv2Row.ChildList.Data)
                    {
                        Lv3Row.IndentName = "table_sub_indent2";
                        Lv3Row.IndentNum = 2;
                        Lv3Row.ChildList =
                            childSrv.GetList(null, new ChildListModel() { PageSize = 100, SelfId = Lv3Row.Id });

                        
                        iii.Add(Lv3Row.Industryid);

                        viewModel.RowForChild.Add(Lv3Row);
                    }

                }
            }
            #endregion

            #region 抓資料 - wesdes50
            int[] i3 = iii.ToArray();

            int start = int.Parse(viewModel.Start);
            int end = int.Parse(viewModel.End);


            
            if (InquireDetailPage.EnergyParentIdList.IndexOf(viewModel.Id) != -1)
            {
                
                var data = childSrv.basedb.energy50_db.Where(p =>
                    p.row_no1 == 201 &&
                    p.yr_mnth >= start && p.yr_mnth <= end
                    ).OrderBy(p => p.yr_mnth);

                viewModel.EnergyData = data.ToList();
            }
            
            else if (InquireDetailPage.進口來源國的子層編號.IndexOf(viewModel.Id) != -1)
            {
                var data = childSrv.basedb.coal50.Where(p =>
                    i3.Contains(p.row_no1) &&
                    p.yr_mnth >= start && p.yr_mnth <= end
                    ).OrderBy(p => p.yr_mnth);
                viewModel.CoalData = data.ToList();
            }
            
            else if (viewModel.Id == "P402" || viewModel.Id == "P403")
            {
                var data = childSrv.basedb.power50.Where(p =>
                    p.row_no1 == 200 &&
                    p.yr_mnth >= start && p.yr_mnth <= end
                    ).OrderBy(p => p.yr_mnth);

                viewModel.PowerData = data.ToList();
            }
            else
            {
                var data = childSrv.basedb.wesdes50.Where(p =>
                    i3.Contains(p.row_no1) &&
                    p.yr_mnth >= start && p.yr_mnth <= end
                    ).OrderBy(p => p.yr_mnth);

                viewModel.Data = data.ToList();
            }
            #endregion

            #region Extract data to data table

            for (int i = 0; i < viewModel.RowForChild.Count(); i++)
            {
                #region 進口來源國的邏輯

                if (InquireDetailPage.進口來源國的子層編號.IndexOf(viewModel.Id) != -1)
                {
                    string[] aryAllCountryCode = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });
                    
                    for (int ccIdx = 0; ccIdx < aryAllCountryCode.Count(); ccIdx++)
                    {
                        var countryCode = aryAllCountryCode[ccIdx];
                        var Row = table.NewRow();
                        Row["縮排"] = 0;
                        Row["日期"] = InquireDetailPage.CountryCodeNameMapping[countryCode];
                        Row["小數點位數"] = 3;

                        #region 處理每一筆資料
                        for (int j = 0; j <= viewModel.RangeTotalMonth; j++)
                        {
                            DateTime r1 = viewModel.RangeStart.AddMonths(j);
                            int s1 = int.Parse(r1.ToString("yyyMM", culture));

                            var m = viewModel.CoalData.Where(p => p.yr_mnth == s1 && p.row_no1 == viewModel.RowForChild[i].Industryid).FirstOrDefault();
                            if (m != null)
                            {
                                var _prop = m.GetType().GetProperty(countryCode);
                                if (_prop != null)
                                {
                                    var _value = _prop.GetValue(m);
                                    if (_value != null)
                                    {
                                        Row[s1.ToString()] = Double.Parse(_value.ToString());
                                    }
                                    else
                                    {
                                        Row[s1.ToString()] = -1;
                                    }
                                }
                                else
                                {
                                    Row[s1.ToString()] = -1;
                                }
                            }
                            else
                            {
                                Row[s1.ToString()] = -1;
                            }
                        }
                        #endregion

                        table.Rows.Add(Row);
                    }

                }

                #endregion

                #region 除了進口來源國以外的羅輯

                else
                {
                    var Row = table.NewRow();
                    Row["縮排"] = viewModel.RowForChild[i].IndentNum;
                    Row["日期"] = viewModel.RowForChild[i].Name;

                    
                    if (!string.IsNullOrEmpty(viewModel.RowForChild[i].AltNameArray[viewModel.UnitType]))
                    {
                        Row["日期"] = viewModel.RowForChild[i].AltNameArray[viewModel.UnitType];
                    }


                    Row["小數點位數"] = viewModel.RowForChild[i].DecimalPlaces;

                    for (int j = 0; j <= viewModel.RangeTotalMonth; j++)
                    {
                        DateTime r1 = viewModel.RangeStart.AddMonths(j);
                        int s1 = int.Parse(r1.ToString("yyyMM", culture));

                        if (InquireDetailPage.EnergyParentIdList.IndexOf(viewModel.Id) != -1)
                        {
                            #region 常用能源指標 (P004 ~ P009)
                            var m = viewModel.EnergyData.Where(p => p.yr_mnth == s1 && p.row_no1 == 201).FirstOrDefault();

                            if (m != null)
                            {
                                if (string.IsNullOrEmpty(viewModel.RowForChild[i].Columnidall))
                                {
                                    Row[s1.ToString()] = -1.0f;
                                    continue;
                                }
                                string[] aryColumnIdAll = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });

                                
                                if (viewModel.Id == "P007")
                                {
                                    if (viewModel.YearOrMonth == "Y")
                                    {
                                        
                                        aryColumnIdAll = new string[] { "ei007" };
                                    }
                                    else
                                    {
                                        
                                        aryColumnIdAll = new string[] { "ei005" };
                                    }
                                }

                                double totalValue = 0.0f;
                                foreach (string columnId in aryColumnIdAll)
                                {
                                    var _prop = m.GetType().GetProperty(columnId);
                                    if (_prop != null)
                                    {
                                        var _value = _prop.GetValue(m);

                                        if (_value != null)
                                        {
                                            
                                            totalValue += Convert.ToDouble(_value);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                Row[s1.ToString()] = totalValue;
                            }
                            else
                            {
                                Row[s1.ToString()] = -1.0f;
                            }
                            #endregion

                        }
                        else if (viewModel.Id == "P402" || viewModel.Id == "P403")
                        {
                            #region 電力 P402
                            var m = viewModel.PowerData.Where(p => p.yr_mnth == s1 && p.row_no1 == 200).FirstOrDefault();

                            if (m != null)
                            {
                                if (string.IsNullOrEmpty(viewModel.RowForChild[i].Columnidall))
                                {
                                    Row[s1.ToString()] = -1.0f;
                                    continue;
                                }
                                string[] aryColumnIdAll = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });

                                double totalValue = 0.0f;
                                foreach (string columnId in aryColumnIdAll)
                                {
                                    var _prop = m.GetType().GetProperty(columnId);
                                    if (_prop != null)
                                    {
                                        var _value = _prop.GetValue(m);

                                        if (_value != null)
                                        {
                                            
                                            totalValue += Convert.ToDouble(_value);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                Row[s1.ToString()] = totalValue;
                            }
                            else
                            {
                                Row[s1.ToString()] = -1.0f;
                            }
                            #endregion
                        }
                        else
                        {
                            #region 非電力
                            var m = viewModel.Data.Where(p => p.yr_mnth == s1 && p.row_no1 == viewModel.RowForChild[i].Industryid).FirstOrDefault();

                            if (m != null)
                            {
                                if (string.IsNullOrEmpty(viewModel.RowForChild[i].Columnidall))
                                {
                                    Row[s1.ToString()] = -1;
                                    continue;
                                }
                                string[] aryColumnIdAll = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });
                                string columnId = string.Empty;

                                if (aryColumnIdAll.Length > viewModel.UnitType)
                                {
                                    columnId = aryColumnIdAll[viewModel.UnitType];
                                }

                                var _prop = m.GetType().GetProperty(columnId);
                                if (_prop != null)
                                {
                                    var _value = _prop.GetValue(m);
                                    if (_value != null)
                                    {
                                        Row[s1.ToString()] = Double.Parse(_value.ToString());
                                    }
                                    else
                                    {
                                        Row[s1.ToString()] = -1.0f;
                                    }
                                }
                                else
                                {
                                    Row[s1.ToString()] = -1.0f;
                                }
                            }
                            else
                            {
                                Row[s1.ToString()] = -1.0f;
                            }
                            #endregion
                        }
                    }

                    table.Rows.Add(Row);
                }
                #endregion
            }
            #endregion
            viewModel.Table = table;

            #region 年月的切換
            
            if (viewModel.YearOrMonth == "Y")
            {
                viewModel.Table = GroupByDataTable(viewModel.Table);
            }

            #endregion

            #region 調整表頭
            foreach (DataColumn column in viewModel.Table.Columns)
            {
                if (column.ColumnName == "日期")
                    continue;
                else if (column.ColumnName == "小數點位數"
                    || column.ColumnName == "縮排")
                    continue;
                else
                {
                    if (viewModel.YearOrMonth.ToLower() == "y")
                    {
                        column.ColumnName = "" + column.ColumnName + "年";
                    }
                    else
                    {
                        string _y = "";
                        string _m = "";
                        if (column.ColumnName.Length == 4)
                        {
                            _y = column.ColumnName.Substring(0, 2);
                            _m = column.ColumnName.Substring(2, 2);
                        }
                        else
                        {
                            _y = column.ColumnName.Substring(0, 3);
                            _m = column.ColumnName.Substring(3, 2);
                        }
                        column.ColumnName = "" + _y + "年" + _m + "月";
                    }
                    
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(download) && download.Equals("匯出"))
            {
                var workbook = DataTableRenderToExcel.簡易查詢下載Excel(viewModel.Table, viewModel);

                var ms = new NpoiMemoryStream();
                ms.AllowClose = false;
                workbook.Write(ms);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                ms.AllowClose = true;
                return File(ms, "application/ms-excel", viewModel.Parent.Name + "_report.xlsx");
            }
            else if (!string.IsNullOrEmpty(download) && download.Equals("匯出ODS"))
            {
                var workbook = DataTableRenderToExcel.簡易查詢下載Excel(viewModel.Table, viewModel);

                
                
                string guidFileName = Guid.NewGuid().ToString();
                string SourceFilePath = @"C:\temp\" + guidFileName + ".xlsx";
                FileStream file = new FileStream(SourceFilePath, FileMode.Create);
                workbook.Write(file);
                file.Close();
                workbook.Close();

                
                var excelAPP = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook book = excelAPP.Workbooks.Open(SourceFilePath);
                string odfPath = SourceFilePath.Replace("xlsx", "ods");
                book.SaveAs(odfPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenDocumentSpreadsheet);
                excelAPP.Visible = false;
                book.Close();
                excelAPP.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelAPP);
                excelAPP = null;
                GC.Collect();

                System.Diagnostics.Process[] excelProcess = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                foreach (var item in excelProcess)
                {
                    item.Kill();
                }

                
                byte[] outPutFile = null;
                using (FileStream fs = new FileStream(odfPath, FileMode.Open))
                {
                    outPutFile = new byte[fs.Length];
                    fs.Read(outPutFile, 0, outPutFile.Length);
                }

                
                System.IO.File.Delete(SourceFilePath);
                
                System.IO.File.Delete(odfPath);

                
                string _newFileName = Path.GetFileName(odfPath).Replace(guidFileName, viewModel.Parent.Name + "_report");
                return File(outPutFile, "application/vnd.oasis.opendocument.spreadsheet", _newFileName);
            } else if (!string.IsNullOrEmpty(download) && download.Equals("JSON"))
            {
                
                DataTable dt = viewModel.Table;
                dt.Columns.RemoveAt(2);
                dt.Columns.RemoveAt(0);
                dt.Columns["日期"].ColumnName = "類別";

;
                return Content(JsonConvert.SerializeObject(dt, Formatting.None,
                new JsonSerializerSettings
                { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), "application/json");
            }
            else
            {
                return View(viewModel);
            }
        }
        [HttpGet]
        public JsonResult DetailJson(InquireDetailPage viewModel, string download)
        {
            viewModel.LastUpdate = systemSrv.GetValueByKey(KEY_LASTUPDATE);
            viewModel.DdlLastDate = systemSrv.GetValueByKey(KEY_DDL_LAST_DAY);

            if (string.IsNullOrEmpty(viewModel.Start))
            {
                viewModel.Start = (viewModel.GetLastUpdate民國年 - 9).ToString() + "01";
            }
            if (string.IsNullOrEmpty(viewModel.End))
            {
                viewModel.End = (viewModel.GetLastUpdate民國年).ToString() + viewModel.GetLastUpdate民國月.ToString("00");
            }

            var calendar = new TaiwanCalendar();
            var culture = CultureInfo.CreateSpecificCulture("zh-TW");
            culture.DateTimeFormat.Calendar = calendar;
            viewModel.Parent = parentSrv.Get(string.Empty, viewModel.Id);

            if (viewModel.ChartType == "6")
            {
                
                
                
                viewModel.TempStart = viewModel.Start;
                viewModel.Start = viewModel.End;
            }
            else
            {
                if (!string.IsNullOrEmpty(viewModel.TempStart))
                {
                    viewModel.Start = viewModel.TempStart;
                    viewModel.TempStart = null;
                }
                
            }

            if (int.Parse(viewModel.End) < int.Parse(viewModel.Start))
            {
                
                TempData["alert"] = "搜尋的結束日期必需大於開始日期";
                return null;
            }

            
            if (InquireDetailPage.EnergyParentIdListOnlyForYear.IndexOf(viewModel.Id) != -1 && viewModel.YearOrMonth == "M")
            {
                viewModel.Start = "09801";
                viewModel.End = "10812";
                viewModel.YearOrMonth = "Y";
            }

            if (viewModel.YearOrMonth == "Y")
            {
                
                viewModel.End = viewModel.End.Substring(0, 3) + "12";
            }

            
            List<int> iii = new List<int>();
            viewModel.RowForChild = new List<ChildModel>();

            #region Init Data Table
            DataTable table = new DataTable();
            table.Columns.Add("縮排", typeof(int));
            table.Columns.Add("日期", typeof(string));
            table.Columns.Add("小數點位數", typeof(int));

            for (int i = 0; i <= viewModel.RangeTotalMonth; i++)
            {
                DateTime r1 = viewModel.RangeStart.AddMonths(i);
                string s1 = r1.ToString("yyyMM", culture);
                table.Columns.Add(s1, typeof(double));
            }

            #endregion

            
            viewModel.ChildList =
                childSrv.GetList(null, new ChildListModel() { PageSize = 100, ParentId = viewModel.Id, SelfId = "" });

            #region Child Node
            
            foreach (var Lv1Row in viewModel.ChildList.Data)
            {
                Lv1Row.IndentName = "fixed_side";
                Lv1Row.IndentNum = 0;
                Lv1Row.ChildList =
                childSrv.GetList(null, new ChildListModel() { PageSize = 100, SelfId = Lv1Row.Id });

                
                iii.Add(Lv1Row.Industryid);

                viewModel.RowForChild.Add(Lv1Row);
                
                foreach (var Lv2Row in Lv1Row.ChildList.Data)
                {
                    Lv2Row.IndentName = "table_sub_indent";
                    Lv2Row.IndentNum = 1;
                    Lv2Row.ChildList =
                        childSrv.GetList(null, new ChildListModel() { PageSize = 100, SelfId = Lv2Row.Id });

                    
                    iii.Add(Lv2Row.Industryid);


                    viewModel.RowForChild.Add(Lv2Row);
                    
                    foreach (var Lv3Row in Lv2Row.ChildList.Data)
                    {
                        Lv3Row.IndentName = "table_sub_indent2";
                        Lv3Row.IndentNum = 2;
                        Lv3Row.ChildList =
                            childSrv.GetList(null, new ChildListModel() { PageSize = 100, SelfId = Lv3Row.Id });

                        
                        iii.Add(Lv3Row.Industryid);

                        viewModel.RowForChild.Add(Lv3Row);
                    }

                }
            }
            #endregion

            #region 抓資料 - wesdes50
            int[] i3 = iii.ToArray();

            int start = int.Parse(viewModel.Start);
            int end = int.Parse(viewModel.End);


            
            if (InquireDetailPage.EnergyParentIdList.IndexOf(viewModel.Id) != -1)
            {
                
                var data = childSrv.basedb.energy50_db.Where(p =>
                    p.row_no1 == 201 &&
                    p.yr_mnth >= start && p.yr_mnth <= end
                    ).OrderBy(p => p.yr_mnth);

                viewModel.EnergyData = data.ToList();
            }
            
            else if (InquireDetailPage.進口來源國的子層編號.IndexOf(viewModel.Id) != -1)
            {
                var data = childSrv.basedb.coal50.Where(p =>
                    i3.Contains(p.row_no1) &&
                    p.yr_mnth >= start && p.yr_mnth <= end
                    ).OrderBy(p => p.yr_mnth);
                viewModel.CoalData = data.ToList();
            }
            
            else if (viewModel.Id == "P402" || viewModel.Id == "P403")
            {
                var data = childSrv.basedb.power50.Where(p =>
                    p.row_no1 == 200 &&
                    p.yr_mnth >= start && p.yr_mnth <= end
                    ).OrderBy(p => p.yr_mnth);

                viewModel.PowerData = data.ToList();
            }
            else
            {
                var data = childSrv.basedb.wesdes50.Where(p =>
                    i3.Contains(p.row_no1) &&
                    p.yr_mnth >= start && p.yr_mnth <= end
                    ).OrderBy(p => p.yr_mnth);

                viewModel.Data = data.ToList();
            }
            #endregion

            #region Extract data to data table

            for (int i = 0; i < viewModel.RowForChild.Count(); i++)
            {
                #region 進口來源國的邏輯

                if (InquireDetailPage.進口來源國的子層編號.IndexOf(viewModel.Id) != -1)
                {
                    string[] aryAllCountryCode = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });
                    
                    for (int ccIdx = 0; ccIdx < aryAllCountryCode.Count(); ccIdx++)
                    {
                        var countryCode = aryAllCountryCode[ccIdx];
                        var Row = table.NewRow();
                        Row["縮排"] = 0;
                        Row["日期"] = InquireDetailPage.CountryCodeNameMapping[countryCode];
                        Row["小數點位數"] = 3;

                        #region 處理每一筆資料
                        for (int j = 0; j <= viewModel.RangeTotalMonth; j++)
                        {
                            DateTime r1 = viewModel.RangeStart.AddMonths(j);
                            int s1 = int.Parse(r1.ToString("yyyMM", culture));

                            var m = viewModel.CoalData.Where(p => p.yr_mnth == s1 && p.row_no1 == viewModel.RowForChild[i].Industryid).FirstOrDefault();
                            if (m != null)
                            {
                                var _prop = m.GetType().GetProperty(countryCode);
                                if (_prop != null)
                                {
                                    var _value = _prop.GetValue(m);
                                    if (_value != null)
                                    {
                                        Row[s1.ToString()] = Double.Parse(_value.ToString());
                                    }
                                    else
                                    {
                                        Row[s1.ToString()] = -1;
                                    }
                                }
                                else
                                {
                                    Row[s1.ToString()] = -1;
                                }
                            }
                            else
                            {
                                Row[s1.ToString()] = -1;
                            }
                        }
                        #endregion

                        table.Rows.Add(Row);
                    }

                }

                #endregion

                #region 除了進口來源國以外的羅輯

                else
                {
                    var Row = table.NewRow();
                    Row["縮排"] = viewModel.RowForChild[i].IndentNum;
                    Row["日期"] = viewModel.RowForChild[i].Name;

                    
                    if (!string.IsNullOrEmpty(viewModel.RowForChild[i].AltNameArray[viewModel.UnitType]))
                    {
                        Row["日期"] = viewModel.RowForChild[i].AltNameArray[viewModel.UnitType];
                    }


                    Row["小數點位數"] = viewModel.RowForChild[i].DecimalPlaces;

                    for (int j = 0; j <= viewModel.RangeTotalMonth; j++)
                    {
                        DateTime r1 = viewModel.RangeStart.AddMonths(j);
                        int s1 = int.Parse(r1.ToString("yyyMM", culture));

                        if (InquireDetailPage.EnergyParentIdList.IndexOf(viewModel.Id) != -1)
                        {
                            #region 常用能源指標 (P004 ~ P009)
                            var m = viewModel.EnergyData.Where(p => p.yr_mnth == s1 && p.row_no1 == 201).FirstOrDefault();

                            if (m != null)
                            {
                                if (string.IsNullOrEmpty(viewModel.RowForChild[i].Columnidall))
                                {
                                    Row[s1.ToString()] = -1.0f;
                                    continue;
                                }
                                string[] aryColumnIdAll = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });

                                
                                if (viewModel.Id == "P007")
                                {
                                    if (viewModel.YearOrMonth == "Y")
                                    {
                                        
                                        aryColumnIdAll = new string[] { "ei007" };
                                    }
                                    else
                                    {
                                        
                                        aryColumnIdAll = new string[] { "ei005" };
                                    }
                                }

                                double totalValue = 0.0f;
                                foreach (string columnId in aryColumnIdAll)
                                {
                                    var _prop = m.GetType().GetProperty(columnId);
                                    if (_prop != null)
                                    {
                                        var _value = _prop.GetValue(m);

                                        if (_value != null)
                                        {
                                            
                                            totalValue += Convert.ToDouble(_value);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                Row[s1.ToString()] = totalValue;
                            }
                            else
                            {
                                Row[s1.ToString()] = -1.0f;
                            }
                            #endregion

                        }
                        else if (viewModel.Id == "P402" || viewModel.Id == "P403")
                        {
                            #region 電力 P402
                            var m = viewModel.PowerData.Where(p => p.yr_mnth == s1 && p.row_no1 == 200).FirstOrDefault();

                            if (m != null)
                            {
                                if (string.IsNullOrEmpty(viewModel.RowForChild[i].Columnidall))
                                {
                                    Row[s1.ToString()] = -1.0f;
                                    continue;
                                }
                                string[] aryColumnIdAll = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });

                                double totalValue = 0.0f;
                                foreach (string columnId in aryColumnIdAll)
                                {
                                    var _prop = m.GetType().GetProperty(columnId);
                                    if (_prop != null)
                                    {
                                        var _value = _prop.GetValue(m);

                                        if (_value != null)
                                        {
                                            
                                            totalValue += Convert.ToDouble(_value);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                Row[s1.ToString()] = totalValue;
                            }
                            else
                            {
                                Row[s1.ToString()] = -1.0f;
                            }
                            #endregion
                        }
                        else
                        {
                            #region 非電力
                            var m = viewModel.Data.Where(p => p.yr_mnth == s1 && p.row_no1 == viewModel.RowForChild[i].Industryid).FirstOrDefault();

                            if (m != null)
                            {
                                if (string.IsNullOrEmpty(viewModel.RowForChild[i].Columnidall))
                                {
                                    Row[s1.ToString()] = -1;
                                    continue;
                                }
                                string[] aryColumnIdAll = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });
                                string columnId = string.Empty;

                                if (aryColumnIdAll.Length > viewModel.UnitType)
                                {
                                    columnId = aryColumnIdAll[viewModel.UnitType];
                                }

                                var _prop = m.GetType().GetProperty(columnId);
                                if (_prop != null)
                                {
                                    var _value = _prop.GetValue(m);
                                    if (_value != null)
                                    {
                                        Row[s1.ToString()] = Double.Parse(_value.ToString());
                                    }
                                    else
                                    {
                                        Row[s1.ToString()] = -1.0f;
                                    }
                                }
                                else
                                {
                                    Row[s1.ToString()] = -1.0f;
                                }
                            }
                            else
                            {
                                Row[s1.ToString()] = -1.0f;
                            }
                            #endregion
                        }
                    }

                    table.Rows.Add(Row);
                }
                #endregion
            }
            #endregion
            viewModel.Table = table;

            #region 年月的切換
            
            if (viewModel.YearOrMonth == "Y")
            {
                viewModel.Table = GroupByDataTable(viewModel.Table);
            }

            #endregion

            #region 調整表頭
            foreach (DataColumn column in viewModel.Table.Columns)
            {
                if (column.ColumnName == "日期")
                    continue;
                else if (column.ColumnName == "小數點位數"
                    || column.ColumnName == "縮排")
                    continue;
                else
                {
                    if (viewModel.YearOrMonth.ToLower() == "y")
                    {
                        column.ColumnName = "" + column.ColumnName + "年";
                    }
                    else
                    {
                        string _y = "";
                        string _m = "";
                        if (column.ColumnName.Length == 4)
                        {
                            _y = column.ColumnName.Substring(0, 2);
                            _m = column.ColumnName.Substring(2, 2);
                        }
                        else
                        {
                            _y = column.ColumnName.Substring(0, 3);
                            _m = column.ColumnName.Substring(3, 2);
                        }
                        column.ColumnName = "" + _y + "年" + _m + "月";
                    }
                    
                }
            }
            #endregion

                DataTable dt = viewModel.Table;
                dt.Columns.RemoveAt(2);
                dt.Columns.RemoveAt(0);
                dt.Columns["日期"].ColumnName = "類別";

                var RetuenValue = 
                 JsonConvert.SerializeObject(dt, Formatting.None,
                         new JsonSerializerSettings
                         { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                 
            return Json(RetuenValue.ToString(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DetailAjax(InquireDetailPage viewModel, string download)
        {
            viewModel.LastUpdate = systemSrv.GetValueByKey(KEY_LASTUPDATE);
            viewModel.DdlLastDate = systemSrv.GetValueByKey(KEY_DDL_LAST_DAY);

            if (string.IsNullOrEmpty(viewModel.Start))
            {
                viewModel.Start = (viewModel.GetLastUpdate民國年 - 2).ToString() + "01";
            }
            if (string.IsNullOrEmpty(viewModel.End))
            {
                viewModel.End = (viewModel.GetLastUpdate民國年).ToString() + viewModel.GetLastUpdate民國月.ToString("00");
            }

            var calendar = new TaiwanCalendar();
            var culture = CultureInfo.CreateSpecificCulture("zh-TW");
            culture.DateTimeFormat.Calendar = calendar;
            viewModel.Parent = parentSrv.Get(string.Empty, viewModel.Id);

            if (viewModel.ChartType == "6")
            {
                
                
                
                viewModel.TempStart = viewModel.Start;
                viewModel.Start = viewModel.End;
            }
            else
            {
                if (!string.IsNullOrEmpty(viewModel.TempStart))
                {
                    viewModel.Start = viewModel.TempStart;
                    viewModel.TempStart = null;
                }
                
            }

            if (int.Parse(viewModel.End) < int.Parse(viewModel.Start))
            {
                
                TempData["alert"] = "搜尋的結束日期必需大於開始日期";
                return RedirectToAction("Detail", new { Id = viewModel.Id });
            }

            
            if (InquireDetailPage.EnergyParentIdListOnlyForYear.IndexOf(viewModel.Id) != -1 && viewModel.YearOrMonth == "M")
            {
                viewModel.Start = "09801";
                viewModel.End = "10812";
                viewModel.YearOrMonth = "Y";
            }

            if (viewModel.YearOrMonth == "Y")
            {
                
                viewModel.End = viewModel.End.Substring(0, 3) + "12";
            }

            
            List<int> iii = new List<int>();
            viewModel.RowForChild = new List<ChildModel>();

            #region Init Data Table
            DataTable table = new DataTable();
            table.Columns.Add("縮排", typeof(int));
            table.Columns.Add("日期", typeof(string));
            table.Columns.Add("小數點位數", typeof(int));

            for (int i = 0; i <= viewModel.RangeTotalMonth; i++)
            {
                DateTime r1 = viewModel.RangeStart.AddMonths(i);
                string s1 = r1.ToString("yyyMM", culture);
                table.Columns.Add(s1, typeof(double));
            }

            #endregion

            
            viewModel.ChildList =
                childSrv.GetList(null, new ChildListModel() { PageSize = 100, ParentId = viewModel.Id, SelfId = "" });

            #region Child Node
            
            foreach (var Lv1Row in viewModel.ChildList.Data)
            {
                Lv1Row.IndentName = "fixed_side";
                Lv1Row.IndentNum = 0;
                Lv1Row.ChildList =
                childSrv.GetList(null, new ChildListModel() { PageSize = 100, SelfId = Lv1Row.Id });

                
                iii.Add(Lv1Row.Industryid);

                viewModel.RowForChild.Add(Lv1Row);
                
                foreach (var Lv2Row in Lv1Row.ChildList.Data)
                {
                    Lv2Row.IndentName = "table_sub_indent";
                    Lv2Row.IndentNum = 1;
                    Lv2Row.ChildList =
                        childSrv.GetList(null, new ChildListModel() { PageSize = 100, SelfId = Lv2Row.Id });

                    
                    iii.Add(Lv2Row.Industryid);


                    viewModel.RowForChild.Add(Lv2Row);
                    
                    foreach (var Lv3Row in Lv2Row.ChildList.Data)
                    {
                        Lv3Row.IndentName = "table_sub_indent2";
                        Lv3Row.IndentNum = 2;
                        Lv3Row.ChildList =
                            childSrv.GetList(null, new ChildListModel() { PageSize = 100, SelfId = Lv3Row.Id });

                        
                        iii.Add(Lv3Row.Industryid);

                        viewModel.RowForChild.Add(Lv3Row);
                    }

                }
            }
            #endregion

            #region 抓資料 - wesdes50
            int[] i3 = iii.ToArray();

            int start = int.Parse(viewModel.Start);
            int end = int.Parse(viewModel.End);


            
            if (InquireDetailPage.EnergyParentIdList.IndexOf(viewModel.Id) != -1)
            {
                
                
                
                
                var data = childSrv.basedb.energy50_db.Where(p =>
                    p.row_no1 == 911 &&
                    p.yr_mnth >= start && p.yr_mnth <= end
                    ).OrderBy(p => p.yr_mnth);
                viewModel.EnergyData = data.ToList();
            }
            
            else if (InquireDetailPage.進口來源國的子層編號.IndexOf(viewModel.Id) != -1)
            {
                var data = childSrv.basedb.coal50.Where(p =>
                    i3.Contains(p.row_no1) &&
                    p.yr_mnth >= start && p.yr_mnth <= end
                    ).OrderBy(p => p.yr_mnth);
                viewModel.CoalData = data.ToList();
            }
            
            else if (viewModel.Id == "P402" || viewModel.Id == "P403")
            {
                if (viewModel.Id == "P403")
                {
                    
                    
                    
                    
                    



                    using (DbContext.Entities db = new DbContext.Entities())
                    {

                        viewModel.PowerData = db.power50.SqlQuery("select  * from  power50 where yr_mnth in (" +
                            "  SELECT max([yr_mnth])   FROM [power50]   where[row_no1] = 200 and  yr_mnth>=" + start + " and yr_mnth <=" + end +
                            "  group by case when  len(yr_mnth) = 4 then left( [yr_mnth], 2)  when len(yr_mnth) = 5 then left([yr_mnth],3) end  " +
                            "    )  and row_no1 =200  ").ToList();
                    }

                }
                else { 
                    var data = childSrv.basedb.power50.Where(p =>
                        p.row_no1 == 200 &&
                        p.yr_mnth >= start && p.yr_mnth <= end
                        ).OrderBy(p => p.yr_mnth);
                    viewModel.PowerData = data.ToList();

                }


            }
            else
            {
                var data = childSrv.basedb.wesdes50.Where(p =>
                    i3.Contains(p.row_no1) &&
                    p.yr_mnth >= start && p.yr_mnth <= end
                    ).OrderBy(p => p.yr_mnth);

                viewModel.Data = data.ToList();
            }
            #endregion

            #region Extract data to data table

            for (int i = 0; i < viewModel.RowForChild.Count(); i++)
            {
                #region 進口來源國的邏輯

                if (InquireDetailPage.進口來源國的子層編號.IndexOf(viewModel.Id) != -1)
                {
                    string[] aryAllCountryCode = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });
                    
                    for (int ccIdx = 0; ccIdx < aryAllCountryCode.Count(); ccIdx++)
                    {
                        var countryCode = aryAllCountryCode[ccIdx];
                        
                        if (countryCode.Equals("nu")){
                            break;
                        }
                        var Row = table.NewRow();
                        Row["縮排"] = 0;
                        Row["日期"] = InquireDetailPage.CountryCodeNameMapping[countryCode];
                        Row["小數點位數"] = 3;

                        #region 處理每一筆資料
                        for (int j = 0; j <= viewModel.RangeTotalMonth; j++)
                        {
                            DateTime r1 = viewModel.RangeStart.AddMonths(j);
                            int s1 = int.Parse(r1.ToString("yyyMM", culture));

                            
                           var m = viewModel.CoalData.Where(p => p.yr_mnth == s1 && p.row_no1 == viewModel.RowForChild[i].Industryid).FirstOrDefault();
 
                            if (m != null)
                            {
                                var _prop = m.GetType().GetProperty(countryCode);
                                if (_prop != null)
                                {
                                    var _value = _prop.GetValue(m);
                                    if (_value != null)
                                    {
                                        Row[s1.ToString()] = Double.Parse(_value.ToString());
                                    }
                                    else
                                    {
                                        
                                    }
                                }
                                else
                                {

                                   
                                }
                            }
                            else
                            {

                                if (viewModel.Id == "P108" || viewModel.Id == "P205" || viewModel.Id == "P305")
                                {
                                    Row[s1.ToString()] = 0;
                                }
                                else { 
                                 
                                }
                            }
                        }
                        #endregion

                        table.Rows.Add(Row);
                    }

                }

                #endregion

                #region 除了進口來源國以外的羅輯

                else
                {
                    var Row = table.NewRow();
                    Row["縮排"] = viewModel.RowForChild[i].IndentNum;
                    Row["日期"] = viewModel.RowForChild[i].Name;

                    
                    if (!string.IsNullOrEmpty(viewModel.RowForChild[i].AltNameArray[viewModel.UnitType]))
                    {
                        Row["日期"] = viewModel.RowForChild[i].AltNameArray[viewModel.UnitType];
                    }


                    Row["小數點位數"] = viewModel.RowForChild[i].DecimalPlaces;

                    for (int j = 0; j <= viewModel.RangeTotalMonth; j++)
                    {
                        DateTime r1 = viewModel.RangeStart.AddMonths(j);
                        int s1 = int.Parse(r1.ToString("yyyMM", culture));

                        if (InquireDetailPage.EnergyParentIdList.IndexOf(viewModel.Id) != -1)
                        {
                            #region 常用能源指標 (P004 ~ P009)

                            var m = viewModel.EnergyData.Where(p => p.yr_mnth == s1 && p.row_no1 == 201).FirstOrDefault();
                            if (viewModel.Id == "P801" || viewModel.Id == "P802" || viewModel.Id == "P803" || viewModel.Id == "P804" || viewModel.Id == "P901" || viewModel.Id == "P902")
                            {
                                m = viewModel.EnergyData.Where(p => p.yr_mnth == s1 && p.row_no1 == 911).FirstOrDefault();
                            }
                            if (m != null)
                            {
                                if (string.IsNullOrEmpty(viewModel.RowForChild[i].Columnidall))
                                {
                                    Row[s1.ToString()] = -1.0f;
                                    continue;
                                }
                                string[] aryColumnIdAll = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });

                                
                                if (viewModel.Id == "P007")
                                {
                                    if (viewModel.YearOrMonth == "Y")
                                    {
                                        
                                        aryColumnIdAll = new string[] { "ei007" };
                                    }
                                    else
                                    {
                                        
                                        aryColumnIdAll = new string[] { "ei005" };
                                    }
                                }

                                double totalValue = 0.0f;
                                foreach (string columnId in aryColumnIdAll)
                                {
                                    var _prop = m.GetType().GetProperty(columnId);
                                    if (_prop != null)
                                    {
                                        var _value = _prop.GetValue(m);

                                        if (_value != null)
                                        {
                                            
                                            totalValue += Convert.ToDouble(_value);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                Row[s1.ToString()] = totalValue;
                            }
                            else
                            {
                                if (viewModel.Id == "P801" || viewModel.Id == "P802" || viewModel.Id == "P803" || viewModel.Id == "P804" || viewModel.Id == "P901" || viewModel.Id == "P902")
                                {
                                    Row[s1.ToString()] = 0;
                                }
                                else{ 
                                    Row[s1.ToString()] = -1.0f;
                                }
                            }
                            #endregion

                        }
                        else if (viewModel.Id == "P402" || viewModel.Id == "P403")
                        {
                            #region 電力 P402
                            var m = viewModel.PowerData.Where(p => p.yr_mnth == s1 && p.row_no1 == 200).FirstOrDefault();

                            if (m != null)
                            {
                                if (string.IsNullOrEmpty(viewModel.RowForChild[i].Columnidall))
                                {
                                    Row[s1.ToString()] = -1.0;
                                    continue;
                                }
                                string[] aryColumnIdAll = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });

                                double totalValue = 0.0;
                                foreach (string columnId in aryColumnIdAll)
                                {
                                    var _prop = m.GetType().GetProperty(columnId);
                                    if (_prop != null)
                                    {
                                        var _value = _prop.GetValue(m);

                                        if (_value != null)
                                        {
                                            
                                            totalValue += Convert.ToDouble(_value);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                Row[s1.ToString()] = totalValue;
                            }
                            else
                            {
                                if (viewModel.Id == "P403")
                                {
                                    Row[s1.ToString()] = 0;
                                }
                                else { 
                                    Row[s1.ToString()] = -1.0;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region 非電力
                            var m = viewModel.Data.Where(p => p.yr_mnth == s1 && p.row_no1 == viewModel.RowForChild[i].Industryid).FirstOrDefault();

                            if (m != null)
                            {
                                if (string.IsNullOrEmpty(viewModel.RowForChild[i].Columnidall))
                                {
                                    Row[s1.ToString()] = -1;
                                    continue;
                                }
                                string[] aryColumnIdAll = viewModel.RowForChild[i].Columnidall.Split(new char[] { ',' });
                                string columnId = string.Empty;

                                if (aryColumnIdAll.Length > viewModel.UnitType)
                                {
                                    columnId = aryColumnIdAll[viewModel.UnitType];
                                }

                                var _prop = m.GetType().GetProperty(columnId);
                                if (_prop != null)
                                {
                                    var _value = _prop.GetValue(m);
                                    if (_value != null)
                                    {
                                        Row[s1.ToString()] = Double.Parse(_value.ToString());
                                    }
                                    else
                                    {
                                        Row[s1.ToString()] = -1.0f;
                                    }
                                }
                                else
                                {
                                    Row[s1.ToString()] = -1.0f;
                                }
                            }
                            else
                            {
                                Row[s1.ToString()] = -1.0f;
                            }
                            #endregion
                        }
                    }

                    table.Rows.Add(Row);
                }
                #endregion
            }
            #endregion
            viewModel.Table = table;

            #region 年月的切換
            
            if (viewModel.YearOrMonth == "Y")
            {
                if (  viewModel.Id == "P801" || viewModel.Id == "P802" || viewModel.Id == "P803" || viewModel.Id == "P804" || viewModel.Id == "P901" || viewModel.Id == "P902"
                    || viewModel.Id == "P102" || viewModel.Id == "P103"
                    || viewModel.Id == "P203" || viewModel.Id == "P204" || viewModel.Id == "P205"
                     || viewModel.Id == "P302" || viewModel.Id == "P303" || viewModel.Id == "P304"
                      || viewModel.Id == "P402" ||  viewModel.Id == "P403" || viewModel.Id == "P405"
                      || viewModel.Id == "P502" || viewModel.Id == "P503" || viewModel.Id == "P504"
                       || viewModel.Id == "p604" || viewModel.Id == "P703" || viewModel.Id == "P704")
                {
                    viewModel.Table = GroupByDataTable(viewModel.Table,false);
                }
                else { 
                    viewModel.Table = GroupByDataTable(viewModel.Table);
                }
            }

            #endregion

            #region 調整表頭
            foreach (DataColumn column in viewModel.Table.Columns)
            {
                if (column.ColumnName == "日期")
                    continue;
                else if (column.ColumnName == "小數點位數"
                    || column.ColumnName == "縮排")
                    continue;
                else
                {
                    if (viewModel.YearOrMonth.ToLower() == "y")
                    {
                        column.ColumnName = "" + column.ColumnName + "年";
                    }
                    else
                    {
                        string _y = "";
                        string _m = "";
                        if (column.ColumnName.Length == 4)
                        {
                            _y = column.ColumnName.Substring(0, 2);
                            _m = column.ColumnName.Substring(2, 2);
                        }
                        else
                        {
                            _y = column.ColumnName.Substring(0, 3);
                            _m = column.ColumnName.Substring(3, 2);
                        }
                        column.ColumnName = "" + _y + "年" + _m + "月";
                    }
                    
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(download) && download.Equals("匯出"))
            {
                var workbook = DataTableRenderToExcel.簡易查詢下載Excel(viewModel.Table, viewModel);

                var ms = new NpoiMemoryStream();
                ms.AllowClose = false;
                workbook.Write(ms);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                ms.AllowClose = true;
                return File(ms, "application/ms-excel", viewModel.Parent.Name + "_report.xlsx");
            }
            else if (!string.IsNullOrEmpty(download) && download.Equals("匯出ODS"))
            {
                var workbook = DataTableRenderToExcel.簡易查詢下載Excel(viewModel.Table, viewModel);

                
                
                string guidFileName = Guid.NewGuid().ToString();
                string SourceFilePath = @"C:\temp\" + guidFileName + ".xlsx";
                FileStream file = new FileStream(SourceFilePath, FileMode.Create);
                workbook.Write(file);
                file.Close();
                workbook.Close();

                
                var excelAPP = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook book = excelAPP.Workbooks.Open(SourceFilePath);
                string odfPath = SourceFilePath.Replace("xlsx", "ods");
                book.SaveAs(odfPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenDocumentSpreadsheet);
                excelAPP.Visible = false;
                book.Close();
                excelAPP.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelAPP);
                excelAPP = null;
                GC.Collect();

                System.Diagnostics.Process[] excelProcess = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                foreach (var item in excelProcess)
                {
                    item.Kill();
                }

                
                byte[] outPutFile = null;
                using (FileStream fs = new FileStream(odfPath, FileMode.Open))
                {
                    outPutFile = new byte[fs.Length];
                    fs.Read(outPutFile, 0, outPutFile.Length);
                }

                
                System.IO.File.Delete(SourceFilePath);
                
                System.IO.File.Delete(odfPath);

                
                string _newFileName = Path.GetFileName(odfPath).Replace(guidFileName, viewModel.Parent.Name + "_report");
                return File(outPutFile, "application/vnd.oasis.opendocument.spreadsheet", _newFileName);
            }
            else
            {
                return View(viewModel);
            }
        }
        public DataTable GroupByDataTable (DataTable table, Boolean IsDivision = true)
        {
            DataTable newTable = new DataTable();
            newTable.Columns.Add("縮排", typeof(int));
            newTable.Columns.Add("日期", typeof(string));
            newTable.Columns.Add("小數點位數", typeof(int));

            
            
            
            
            
            for (int i = 3; i < table.Columns.Count; i++)
            {

                string monthColumnName = table.Columns[i].ColumnName;
                string yearColumnName = monthColumnName.Remove(monthColumnName.Length - 2);

                if (!newTable.Columns.Contains(yearColumnName))
                {
                    newTable.Columns.Add(yearColumnName, typeof(Double));
                }
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var Row = newTable.NewRow();
                Row["縮排"] = table.Rows[i][0].ToString();
                Row["日期"] = table.Rows[i][1].ToString();
                Row["小數點位數"] = table.Rows[i][2].ToString();

                for (int i2 = 3; i2 < newTable.Columns.Count; i2++)
                {
                    double totalValue = 0.0;

                    #region 做統計的計算

                    
                    for (int TbClIdx = 3; TbClIdx < table.Columns.Count; TbClIdx++)
                    {
                        string monthColumnName = table.Columns[TbClIdx].ColumnName;
                        string yearColumnName = monthColumnName.Remove(monthColumnName.Length - 2);
                        if (yearColumnName == newTable.Columns[i2].ColumnName)
                        {
                            double temp;
                            if (double.TryParse(table.Rows[i][TbClIdx].ToString(), out temp))
                            {
                                totalValue += temp;
                            }
                        }
                    }
                    #endregion

                    if (totalValue >= -12 && totalValue <= -1)
                    {
                        Row[i2] = -1;
                    }
                    else
                    {
                        if (IsDivision) { 
                            Row[i2] = totalValue/1000;
                        }
                        else
                        {
                            Row[i2] = totalValue;
                        }
                    }
                }

                newTable.Rows.Add(Row);
            }

            return newTable;
        }
    }
}




