using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Globalization;
using JamZoo.Project.WebSite.Library;
using System.Runtime.Caching;

namespace JamZoo.Project.WebSite.Controllers
{
    using Service;
    using Models;
    using ViewModels;
    using DatabaseRow;
    using System.Text;
    using System.IO;
    using Newtonsoft.Json;

    public class DatabaseController : BaseWebController
    {
        EnergyService EnergySrv = new EnergyService();
        FlowService FlowSrv = new FlowService();
        MemoryCache Cache = MemoryCache.Default;
        static string KEY_LASTUPDATE = "資料庫查詢的更新日期";
        static string KEY_DDL_LAST_DAY = "資料庫查詢下拉日期";

        string[] KEY_Q = new string[] { "零", "一", "二", "三", "四" };

        SystemService systemSrv = new SystemService();


        List<string> _excludeColumn = new List<string>()
            {
                "深度",
                "年季月",
                "縮排",
                "小數點位數",
            };

        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Search(DatabaseSearchPage viewModel)
        {
            string PageName = viewModel.PageName;

            viewModel.EnergyDropDownList = EnergySrv.GetDropDownList(PageName, "", "EnergySelectedValue");
            viewModel.FlowDropDownList = FlowSrv.GetDropDownList(PageName, "", "FlowSelectedValue");

            viewModel.LastUpdate = systemSrv.GetValueByKey(KEY_LASTUPDATE);
            viewModel.DdlLastDate = systemSrv.GetValueByKey(KEY_DDL_LAST_DAY);

            if (string.IsNullOrEmpty(viewModel.End))
            {
                viewModel.End = (viewModel.GetLastUpdate民國年).ToString() + viewModel.GetLastUpdate民國月.ToString("00");
            }
            if (string.IsNullOrEmpty(viewModel.Start))
            {
                if (viewModel.PeriodType == "M" || string.IsNullOrEmpty(viewModel.PeriodType))
                {

                    DateTime dtStart = viewModel.RangeEnd.AddYears(-2);
                    viewModel.Start = dtStart.Year - 1911 + dtStart.Month.ToString("00");
                }
                else if (viewModel.PeriodType == "Q")
                {

                    DateTime dtStart = viewModel.RangeEnd.AddYears(-2);
                    viewModel.Start = dtStart.Year - 1911 + "01";
                }
                else if (viewModel.PeriodType == "Y")
                {

                    DateTime dtStart = viewModel.RangeEnd.AddYears(-5);
                    viewModel.Start = dtStart.Year - 1911 + "01";

                }
            }


            return View(viewModel);
        }

        public ActionResult List(DatabaseSearchPage viewModel)
        {
            if (int.Parse(viewModel.End) < int.Parse(viewModel.Start))
            {

                TempData["alert"] = "搜尋的結束日期必需大於開始日期";
                return RedirectToAction("Search", new { PageId = viewModel.PageId });
            }


            Cache.Remove(viewModel.GetCacheKey());
            var calendar = new TaiwanCalendar();
            var culture = CultureInfo.CreateSpecificCulture("zh-TW");
            culture.DateTimeFormat.Calendar = calendar;

            viewModel.DetailPages = new List<DatabaseDetailPage>();

            int start = int.Parse(viewModel.Start);
            int end = int.Parse(viewModel.End);

            #region   先取得 left 跟 right side 的資料
            var FlowData = FlowSrv.GetList("", new FlowListModel() { SelectedValues = viewModel.FlowSelectedValue, PageSize = 999 });
            var EnergyData = EnergySrv.GetList("", new EnergyListModel() { SelectedValues = viewModel.EnergySelectedValue, PageSize = 999 });








            List<int> RightRowNo1 = new List<int>();
            foreach (var FlowRow in FlowData.Data)
            {
                RightRowNo1.AddRange(FlowRow.RowNo1Ary);
            }
            int[] AryRightRowNo1 = RightRowNo1.ToArray();

            List<int> LeftRowNo1 = new List<int>();
            foreach (var EnergyRow in EnergyData.Data)
            {
                LeftRowNo1.AddRange(EnergyRow.RowNo1Ary);
            }

            int[] AryLeftRowNo1 = LeftRowNo1.ToArray();
            #endregion

            #region 處理各 Page 所需要的資料

            if (viewModel.PageName == DatabaseTypeList.Data[0].Name)
            {

                viewModel.OriginData_Db = FlowSrv.basedb.wesdes50_db.Where(p =>
                       AryRightRowNo1.Contains(p.row_no1) &&
                       p.yr_mnth >= start && p.yr_mnth < end
                   ).ToList();
            }
            else if (viewModel.PageName == DatabaseTypeList.Data[1].Name)
            {

                viewModel.CoalData = FlowSrv.basedb.coal50.Where(p =>
                       AryLeftRowNo1.Contains(p.row_no1) &&
                      p.yr_mnth >= start && p.yr_mnth < end
                  ).ToList();
            }
            else if (viewModel.PageName == DatabaseTypeList.Data[2].Name)
            {

                viewModel.EnergyData_Db = FlowSrv.basedb.energy50_db.Where(p =>
                        p.row_no1 == 911 &&
                       p.yr_mnth >= start && p.yr_mnth < end
                   ).ToList();
            }
            else if (viewModel.PageName == DatabaseTypeList.Data[3].Name)
            {

                viewModel.PowerData_Db = FlowSrv.basedb.power50_db.Where(p =>
                        p.row_no1 == 200 &&
                       p.yr_mnth >= start && p.yr_mnth < end
                   ).ToList();
            }
            else if (viewModel.PageName == DatabaseTypeList.Data[4].Name)
            {

                if (viewModel.PeriodType.ToLower() == "y")
                {

                    using (DbContext.Entities db = new DbContext.Entities())
                    {

                        viewModel.PowerData = db.power50.SqlQuery("select  * from  power50 where yr_mnth in (" +
                            "  SELECT max([yr_mnth])   FROM [power50]   where[row_no1] = 200 and  yr_mnth>=" + start + " and yr_mnth <=" + end +
                            "  group by case when  len(yr_mnth) = 4 then left( [yr_mnth], 2)  when len(yr_mnth) = 5 then left([yr_mnth],3) end  " +
                            "    )  and row_no1 =200  ").ToList();
                    }

                }
                else
                {
                    viewModel.PowerData = FlowSrv.basedb.power50.Where(p =>
                        p.row_no1 == 200 &&
                       p.yr_mnth >= start && p.yr_mnth < end
                   ).ToList();
                }

            }
            else if (viewModel.PageName == DatabaseTypeList.Data[5].Name)
            {

                viewModel.FuelData_Db = FlowSrv.basedb.fuel50_db.Where(p =>
                        p.row_no1 == 901 &&
                       p.yr_mnth >= start && p.yr_mnth < end
                   ).ToList();
            }

            #endregion

            #region 處理能源別資料 (欄位)
            foreach (var EnergyRow in EnergyData.Data)
            {
                DatabaseDetailPage newPage = new DatabaseDetailPage();
                newPage.EnergyInfo = EnergyRow;
                viewModel.DetailPages.Add(newPage);


                DataTable table = new DataTable();
                table.Columns.Add("日期", typeof(string));
                table.Columns.Add("深度", typeof(int));
                table.Columns.Add("年季月", typeof(string));
                table.Columns.Add("小數點位數", typeof(int));

                #region 欄位名字的設定
                viewModel.InitTableHeader(table, culture);
                #endregion

                #region 進口來源國的邏輯

                if (viewModel.PageName == DatabaseTypeList.Data[1].Name)
                {

                    string[] 所有國家簡碼陣列 = EnergyRow.ColdIdAry;

                    foreach (string countryCode in 所有國家簡碼陣列)
                    {
                        var _row = table.NewRow();
                        // 移除其他國家項目
                        if (!InquireDetailPage.CountryCodeNameMapping.ContainsKey(countryCode) ||
                            (new List<string>() { "zz", "z1", "z2", "z3", "z4" }).Contains(countryCode))
                        {
                            continue;
                        }
                        _row["日期"] = InquireDetailPage.CountryCodeNameMapping[countryCode];
                        _row["深度"] = 0;
                        _row["年季月"] = viewModel.PeriodType;
                        _row["小數點位數"] = 0;


                        foreach (DataColumn Header in table.Columns)
                        {
                            if (Header.ColumnName == "日期"
                                || Header.ColumnName == "深度"
                                || Header.ColumnName == "年季月"
                                || Header.ColumnName == "小數點位數")
                            {
                                continue;
                            }

                            object o = viewModel.PageRowType;
                            if (o == null) continue;
                            o.GetType().GetMethod("AddCell").Invoke(o,
                            new Object[] {
                                viewModel, _row, Header, EnergyRow, countryCode
                            });
                        }
                        table.Rows.Add(_row);
                    }
                }

                #endregion

                #region 常用能源指標

                else if (viewModel.PageName == DatabaseTypeList.Data[2].Name)
                {
                    var _row = table.NewRow();
                    _row["日期"] = EnergyRow.Name;
                    _row["深度"] = 0;
                    _row["年季月"] = viewModel.PeriodType;
                    _row["小數點位數"] = 3;


                    foreach (DataColumn Header in table.Columns)
                    {
                        if (Header.ColumnName == "日期"
                            || Header.ColumnName == "深度"
                            || Header.ColumnName == "年季月"
                            || Header.ColumnName == "小數點位數")
                        {
                            continue;
                        }

                        object o = viewModel.PageRowType;
                        if (o == null) continue;
                        o.GetType().GetMethod("AddCell").Invoke(o,
                        new Object[] {
                                viewModel, _row, Header, EnergyRow
                        });
                    }
                    table.Rows.Add(_row);
                }

                #endregion

                #region 除了進口來源國' 常用能源指標 以外的羅輯

                else
                {
                    string 欄位名 = EnergyRow.ColdIdAry[viewModel.UnitType];


                    foreach (var FlowRow in FlowData.Data)
                    {
                        var _row = table.NewRow();
                        _row["日期"] = FlowRow.Name;
                        _row["深度"] = FlowRow.Depth;
                        _row["年季月"] = viewModel.PeriodType;
                        _row["小數點位數"] = 0;



                        foreach (DataColumn Header in table.Columns)
                        {
                            if (Header.ColumnName == "日期"
                                || Header.ColumnName == "深度"
                                || Header.ColumnName == "年季月"
                                || Header.ColumnName == "小數點位數")
                            {
                                continue;
                            }
                            object o = viewModel.PageRowType;
                            if (o == null) continue;
                            o.GetType().GetMethod("CreateRow").Invoke(o,
                            new Object[] {
                            viewModel, _row, Header, FlowRow, EnergyRow, 欄位名
                            });





                        }

                        table.Rows.Add(_row);
                    }
                }

                #endregion


                #region 調整表頭
                bool isGotStartForDisplay = false;
                foreach (DataColumn column in table.Columns)
                {
                    if (_excludeColumn.Contains(column.ColumnName))
                    {
                        continue;
                    }
                    else if (column.ColumnName == "日期")
                    {

                        continue;
                    }
                    else
                    {
                        string strYearType = viewModel.yearType == "0" ? "" : "";
                        string newColumnName = column.ColumnName;

                        if (viewModel.PeriodType.ToLower() == "y")
                        {

                            if (viewModel.yearType == "0")
                            {
                                newColumnName = strYearType + (int.Parse(column.ColumnName) + 1911) + "年";
                            }
                            else
                            {
                                newColumnName = strYearType + column.ColumnName + "年";
                            }
                        }
                        else if (viewModel.PeriodType.ToLower() == "q")
                        {

                            string[] _temp = column.ColumnName.Split(new char[] { 'Q' }, StringSplitOptions.RemoveEmptyEntries);
                            string _y = _temp[0];

                            if (viewModel.yearType == "0")
                            {
                                _y = (int.Parse(_temp[0]) + 1911).ToString();
                            }

                            string _q = _temp[1];

                            newColumnName = strYearType + _y + "年第" + KEY_Q[int.Parse(_q)] + "季";
                        }
                        else if (viewModel.PeriodType.ToLower() == "m")
                        {

                            string _y = "";
                            string _m = "";
                            if (column.ColumnName.Length == 5)
                            {
                                _y = column.ColumnName.Substring(0, 3);
                                _m = column.ColumnName.Substring(3, 2);
                            }
                            else if (column.ColumnName.Length == 4)
                            {
                                _y = column.ColumnName.Substring(0, 2);
                                _m = column.ColumnName.Substring(2, 2);
                            }

                            if (viewModel.yearType == "0")
                            {
                                _y = (int.Parse(_y) + 1911).ToString();
                            }
                            newColumnName = strYearType + _y + "年" + _m + "月";
                        }
                        column.ColumnName = newColumnName;
                        if (!isGotStartForDisplay)
                        {
                            viewModel.StartForDisplay = newColumnName;
                            isGotStartForDisplay = true;
                        }
                        viewModel.EndForDisplay = newColumnName;

                    }
                }
                #endregion

                newPage.Table = table;
            }
            #endregion

            Cache.AddOrGetExisting(viewModel.GetCacheKey(), viewModel, DateTime.Now.AddHours(1));

            return View(viewModel);
        }

        public void AddRow<T>(DataRow _row, T m, string ColumnName, string 欄位名)
        {

            if (_row[ColumnName] == DBNull.Value)
            {
                _row[ColumnName] = 0;
            }

            if (m != null)
            {
                var _prop = m.GetType().GetProperty(欄位名);
                if (_prop != null)
                {
                    var _value = _prop.GetValue(m);
                    if (_value != null)
                    {
                        float _temp = (float)_row[ColumnName];
                        _row[ColumnName] = _temp + float.Parse(_value.ToString());
                    }
                }
            }
        }

        public ActionResult Detail(string CacheKey, string ChartType, int I = -1)
        {
            if (I == -1)
            {
                return Redirect("~/Database/");
            }

            var viewModel = (DatabaseSearchPage)Cache.Get(CacheKey);
            if (viewModel == null)
            {
                return Redirect("~/Database/");
            }
            viewModel.DetailI = I;
            viewModel.ChartType = ChartType;

            #region 地圖的話要產一個 json
            if (viewModel.PageId == 5)
            {


                var currentDetail = viewModel.DetailPages[viewModel.DetailI];

                var tb = currentDetail.Table;
                StringBuilder mapJson = new StringBuilder();
                Dictionary<string, int> dict = new Dictionary<string, int>();

                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    string hcKey = "0";
                    int sum = 0;

                    for (int j = 0; j < tb.Columns.Count; j++)
                    {
                        if (tb.Columns[j].ColumnName == "日期")
                        {
                            string countryName = tb.Rows[i][j].ToString();
                            hcKey = revertContryCode(countryName);
                            if (!dict.ContainsKey(hcKey))
                            {
                                dict[hcKey] = 0;
                            }
                            continue;
                        }
                        int tempSum = 0;
                        if (Int32.TryParse(tb.Rows[i][j].ToString(), out tempSum))
                        {
                            sum += tempSum;
                        }
                    }

                    if (!dict.ContainsKey(hcKey))
                    {
                        dict[hcKey] = sum;
                    }
                    else
                    {
                        dict[hcKey] += sum;
                    }
                }

                foreach (var _k in dict.Keys)
                {

                    if (dict[_k] > 0)
                    {
                        mapJson.AppendLine("{ \"hc-key\":\"" + _k + "\",\"value\":" + dict[_k] + "},");
                    }
                }

                currentDetail.MapJson = mapJson.ToString();
            }
            #endregion

            return View(viewModel);
        }

        public ActionResult JSON(string CacheKey, string ChartType, string Ods)
        {
            var viewModel = (DatabaseSearchPage)Cache.Get(CacheKey);
            if (viewModel == null)
            {
                return Redirect("~/Database/");
            }
























            var results = new List<DatabaseJson>();

            foreach (var DP in viewModel.DetailPages)
            {

                DatabaseJson newPage = new DatabaseJson();
                newPage.DataName = DP.EnergyInfo.Name;
                newPage.Table = DP.Table;
                newPage.Table.Columns["日期"].ColumnName = "類別";
                newPage.Table.Columns.RemoveAt(3);
                newPage.Table.Columns.RemoveAt(1);
                results.Add(newPage);
            }

            return Content(JsonConvert.SerializeObject(results, Formatting.None,
            new JsonSerializerSettings
            { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), "application/json");



        }



        public ActionResult Excel(string CacheKey, string ChartType, string Ods)
        {
            var viewModel = (DatabaseSearchPage)Cache.Get(CacheKey);
            if (viewModel == null)
            {
                return Redirect("~/Database/");
            }

            var workbook = DataTableRenderToExcel.NewBook();
            foreach (var DP in viewModel.DetailPages)
            {
                var currentDetail = DP;

                string sheetName = DP.EnergyInfo.Name;

                string strUnitName = DP.EnergyInfo.UnitUpperAry[viewModel.UnitType];

                DataTableRenderToExcel.RenderDataTableToExcelX2Vtit(workbook, sheetName, currentDetail.Table, strUnitName, _excludeColumn);
            }

            if (string.IsNullOrEmpty(Ods))
            {

                var ms = new NpoiMemoryStream();
                ms.AllowClose = false;
                workbook.Write(ms);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                ms.AllowClose = true;

                return File(ms, "application/ms-excel", viewModel.PageInfo.DisplayName + "_report.xlsx");
            }
            else
            {


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


                string _newFileName = Path.GetFileName(odfPath).Replace(guidFileName, viewModel.PageInfo.DisplayName + "_report");
                return File(outPutFile, "application/vnd.oasis.opendocument.spreadsheet", _newFileName);

            }

            #region 封存






















            #endregion

        }

        string revertContryCode(string countryName)
        {
            if (InquireDetailPage.CountryNameToCountryCode.ContainsKey(countryName))
            {
                return InquireDetailPage.CountryNameToCountryCode[countryName];

            }
            return "0";
        }
    }

    public class NpoiMemoryStream : MemoryStream
    {
        public NpoiMemoryStream()
        {





            AllowClose = true;
        }

        public bool AllowClose { get; set; }

        public override void Close()
        {
            if (AllowClose)
                base.Close();
        }
    }
}

