using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;

namespace JamZoo.Project.WebSite.Library
{
    using NPOI.HSSF.UserModel;
    using NPOI.XSSF.UserModel;
    using ViewModels;

    public static class DataTableLibrary
    {
        
        public static DataTable ToDataTable<T>(this IList<T> data, List<string> exclusiveProp)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];

                if (exclusiveProp.Contains(prop.Name)) continue;

                if (prop.PropertyType.IsGenericType
                    && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                    )
                {
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                }
                else
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            foreach (T item in data)
            {
                object[] values = new object[table.Columns.Count];
                int idx = 0;
                foreach (DataColumn column in table.Columns)
                {
                    values[idx] = props[column.ColumnName].GetValue(item);
                    idx++;
                }
                table.Rows.Add(values);
            }

            
            
            
            
            
            
            
            
            
            
            return table;
        }
    }

    public class DataTableRenderToExcel
    {
        public static string getAmtOfChar(int amt, char c)
        {
            string returnStr = "";

            for (int i = 0; i < amt; i++)
            {
                returnStr += c;
            }

            return returnStr;
        }

        public static XSSFWorkbook NewBook ()
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            return workbook;
        }
        public static XSSFWorkbook RenderDataTableToExcelX2Vtit(XSSFWorkbook workbook, string sheetName, DataTable SourceTable,
                    string UnitName,
                    List<string> ExcludeColumnNames)
        {
            sheetName = sheetName.Replace("/", "");
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet(sheetName);

            XSSFCellStyle oStyle = CellStyle(workbook);
            
            XSSFCellStyle stringnum = (XSSFCellStyle)workbook.CreateCellStyle();
            
            XSSFDataFormat df = (XSSFDataFormat)workbook.CreateDataFormat();
            stringnum.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0.000");
            stringnum.DataFormat = 4;
            stringnum.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            stringnum.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            stringnum.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            stringnum.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;


            #region 單位: xxx
            XSSFRow headerRow0 = (XSSFRow)sheet.CreateRow(0);
            var _h0 = headerRow0.CreateCell(0);
            _h0.SetCellType(NPOI.SS.UserModel.CellType.String);
            string new_UnitName = UnitName.Replace("/10&#8311;", "10,000,000");
            _h0.SetCellValue("單位:" + new_UnitName);
            
            _h0.CellStyle = oStyle;
            #endregion


            #region 標題
            XSSFRow headerRow = (XSSFRow)sheet.CreateRow(1);
            int _Ordinal = 0;
            foreach (DataColumn column in SourceTable.Columns)
            {
                if (ExcludeColumnNames.Contains(column.ColumnName))
                {
                    continue;
                }
                var _h = headerRow.CreateCell(_Ordinal);
                _Ordinal++;

                _h.SetCellType(NPOI.SS.UserModel.CellType.String);
                _h.SetCellValue(column.ColumnName);
                
                _h.CellStyle = oStyle;
            }

            #endregion

            
            #region 設定表格內容
            int rowIndex = 2;

            foreach (DataRow row in SourceTable.Rows)
            {
                XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);

                _Ordinal = 0;
                int colIdx = 0;
                int 深度 = (int)row["深度"];
                int 小數點位數 = (int)row["小數點位數"];
                foreach (DataColumn column in SourceTable.Columns)
                {
                    if (ExcludeColumnNames.Contains(column.ColumnName))
                    {
                        continue;
                    }
                    var _r = dataRow.CreateCell(_Ordinal);
                    sheet.AutoSizeColumn(colIdx);
                    if (column.ColumnName == "日期")
                    {
                        _Ordinal++;
                        _r.SetCellValue(縮排文字(row[column].ToString(), 深度));
                        
                        _r.CellStyle = oStyle;
                        _r.SetCellType(NPOI.SS.UserModel.CellType.String);
                    }
                    else
                    {
                        _Ordinal++;

                        string _tempValue = row[column].ToString();
                        if (_tempValue == "-1")
                        {
                            #region 設定格式
                            _r.SetCellType(NPOI.SS.UserModel.CellType.String);
                            
                            _r.CellStyle = oStyle;
                            #endregion
                            _r.SetCellValue("-");
                        }
                        else
                        {
                            #region 設定格式

                            string 小數點格式化 = getAmtOfChar(小數點位數, '0');



                            _r.SetCellType(NPOI.SS.UserModel.CellType.String);
                            if (小數點格式化 == "")
                            {
                                stringnum.DataFormat = df.GetFormat("#,##0" + getAmtOfChar(小數點位數, '0'));
                            }
                            else
                            {
                                stringnum.DataFormat = df.GetFormat("#,##0." + getAmtOfChar(小數點位數, '0'));
                            }
                            
                            _r.CellStyle = stringnum;
                            #endregion
                            double v = 0f;
                            double.TryParse(row[column].ToString(), out v);
                            _r.SetCellValue(v);
                        }
                    }
                    colIdx++;
                }

                rowIndex++;
            }
            #endregion


            return workbook;

            #region 封存不用
            
            
            
            
            

            
            
            
            #endregion
        }
        public static XSSFWorkbook RenderDataTableToExcelX2(XSSFWorkbook workbook, string sheetName, DataTable SourceTable,
            string UnitName,
            List<string> ExcludeColumnNames)
        {
            sheetName = sheetName.Replace("/", "");
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet(sheetName);

            #region 單位: xxx
            XSSFRow headerRow0 = (XSSFRow)sheet.CreateRow(0);
            var _h0 = headerRow0.CreateCell(0);
            _h0.SetCellType(NPOI.SS.UserModel.CellType.String);
            string new_UnitName = UnitName.Replace("/10&#8311;", "10,000,000");
            _h0.SetCellValue("單位:" + new_UnitName);
            _h0.CellStyle = CellStyle(workbook);
            #endregion


            #region 標題
            XSSFRow headerRow = (XSSFRow)sheet.CreateRow(1);
            int _Ordinal = 0;
            foreach (DataColumn column in SourceTable.Columns)
            {
                if (ExcludeColumnNames.Contains(column.ColumnName))
                {
                    continue;
                }
                var _h = headerRow.CreateCell(_Ordinal);
                _Ordinal++;

                _h.SetCellType(NPOI.SS.UserModel.CellType.String);
                _h.SetCellValue(column.ColumnName);
                _h.CellStyle = CellStyle(workbook);
            }

            #endregion

            
            #region 設定表格內容
            int rowIndex = 2;

            foreach (DataRow row in SourceTable.Rows)
            {
                XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);

                _Ordinal = 0;
                int colIdx = 0;
                int 深度 = (int)row["深度"];
                int 小數點位數 = (int)row["小數點位數"];
                foreach (DataColumn column in SourceTable.Columns)
                {
                    if (ExcludeColumnNames.Contains(column.ColumnName))
                    {
                        continue;
                    }
                    var _r = dataRow.CreateCell(_Ordinal);
                    sheet.AutoSizeColumn(colIdx);
                    if (column.ColumnName == "日期")
                    {
                        _Ordinal++;
                        _r.SetCellValue(縮排文字(row[column].ToString(), 深度));
                        _r.CellStyle = CellStyle(workbook);
                        _r.SetCellType(NPOI.SS.UserModel.CellType.String);
                    }
                    else
                    {
                        _Ordinal++;

                        string _tempValue = row[column].ToString();
                        if (_tempValue == "-1")
                        {
                            #region 設定格式
                            _r.SetCellType(NPOI.SS.UserModel.CellType.String);
                            _r.CellStyle = CellStyle(workbook);
                            #endregion
                            _r.SetCellValue("-");
                        }
                        else
                        {
                            #region 設定格式
                            XSSFCellStyle stringnum = (XSSFCellStyle)workbook.CreateCellStyle();
                            
                            XSSFDataFormat df = (XSSFDataFormat)workbook.CreateDataFormat();

                            string 小數點格式化 = getAmtOfChar(小數點位數, '0');

                            if (小數點格式化 == "")
                            {
                                stringnum.DataFormat = df.GetFormat("#,##0" + getAmtOfChar(小數點位數, '0'));
                            }
                            else
                            {
                                stringnum.DataFormat = df.GetFormat("#,##0." + getAmtOfChar(小數點位數, '0'));
                            }
                            
                            
                            stringnum.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                            stringnum.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                            stringnum.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                            stringnum.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

                            _r.SetCellType(NPOI.SS.UserModel.CellType.String);
                            _r.CellStyle = stringnum;
                            #endregion
                            double v = 0f;
                            double.TryParse(row[column].ToString(), out v);
                            _r.SetCellValue(v);
                        }
                    }
                    colIdx++;
                }

                rowIndex++;
            }
            #endregion


            return workbook;

            #region 封存不用
            
            
            
            
            

            
            
            
            #endregion
        }

        public static XSSFCellStyle CellStyle(XSSFWorkbook workbook)
        {
            XSSFCellStyle Style = workbook.CreateCellStyle() as XSSFCellStyle;
            Style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            Style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            Style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            Style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            return Style;
        }

        public static XSSFWorkbook 簡易查詢下載Excel(DataTable SourceTable, InquireDetailPage viewModel)
        {
            string UnitName = (viewModel.Parent.UnitListUpperArray[viewModel.UnitType]);
            if (string.IsNullOrEmpty(UnitName))
            {
                UnitName = "無";
            }
 
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet();

            
            
            

            
            XSSFRow headerRow = (XSSFRow)sheet.CreateRow(0);

            
            int headerColIdx = 0;
            foreach (DataColumn column in SourceTable.Columns)
            {
                if (column.ColumnName == "日期")
                {
                    var h1 = headerRow.CreateCell(0);
                    h1.SetCellValue(column.ColumnName);
                    h1.CellStyle = CellStyle(workbook);
                    var h2 = headerRow.CreateCell(1);
                    h2.SetCellValue("單位");
                    h2.CellStyle = CellStyle(workbook);
                    headerColIdx++;
                    headerColIdx++;
                }
                else if (column.ColumnName == "小數點位數" 
                    || column.ColumnName == "縮排")
                {
                    continue;
                }
                else
                {
                    var h = headerRow.CreateCell(headerColIdx);
                    h.SetCellType(NPOI.SS.UserModel.CellType.Numeric);
                    h.SetCellValue(column.ColumnName);
                    h.CellStyle = CellStyle(workbook);
                    headerColIdx++;
                }
            }

            
            int rowIndex = 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);

                int ColIdx = 0;
                int 小數點位數 = 3;
                int 縮排 = 0;
                foreach (DataColumn column in SourceTable.Columns)
                {
                    if (column.ColumnName == "縮排")
                    {
                        縮排 = Int32.Parse(row[column].ToString());
                        continue;
                    }
                    if (column.ColumnName == "小數點位數")
                    {
                        小數點位數 = Int32.Parse(row[column].ToString());
                        continue;
                    }

                    if (column.ColumnName == "日期")
                    {
                        #region 把項目及單位拆成兩欄
                        string regex單位pattern = @"^(.*)\((.*)\)$";
                        string title = (row[column].ToString());

                        var matcheds = Regex.Matches(title, regex單位pattern, RegexOptions.Singleline);

                        if (matcheds.Count > 0 && matcheds[0].Groups.Count == 3)
                        {
                            var c1 = dataRow.CreateCell(0);
                            c1.SetCellValue(縮排文字(matcheds[0].Groups[1].ToString(), 縮排));
                            c1.CellStyle = CellStyle(workbook);
                            var c2 = dataRow.CreateCell(1);
                            c2.SetCellValue(matcheds[0].Groups[2].ToString());
                            c2.CellStyle = CellStyle(workbook);
                        }
                        else
                        {
                            var c1 = dataRow.CreateCell(0);
                            c1.SetCellValue(縮排文字(title, 縮排));
                            c1.CellStyle = CellStyle(workbook);
                            var c2 = dataRow.CreateCell(1);
                            
                            string temp單位 = "-";
                            if (viewModel.UnitType < viewModel.Parent.UnitListArray.Length)
                            {
                                temp單位 = viewModel.Parent.UnitListArray[viewModel.UnitType];
                            }
                            if (string.IsNullOrEmpty(temp單位))
                            {
                                temp單位 = "-";
                            }
                            string rawTemp單位 = System.Web.HttpUtility.HtmlDecode(temp單位);
                            c2.SetCellValue(rawTemp單位);
                            c2.CellStyle = CellStyle(workbook);
                        }
                        sheet.AutoSizeColumn(ColIdx);
                        ColIdx++;
                        sheet.AutoSizeColumn(ColIdx);
                        ColIdx++;
                        #endregion
                    }
                    else
                    {
                        var _c = dataRow.CreateCell(ColIdx);
                        _c.CellStyle = CellStyle(workbook);
                        string _tempValue = row[column].ToString();
                        if (_tempValue == "-1")
                        {
                            _tempValue = "-";
                            _c.SetCellValue(_tempValue);
                        }
                        else
                        {
                            float _temp = 0;
                            float.TryParse(_tempValue, out _temp);
                            _c.SetCellValue(_temp.ToString("N" + 小數點位數));
                            

                            _c.CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                        }
                       
                        sheet.AutoSizeColumn(ColIdx);
                        ColIdx++;
                    }
                }

                rowIndex++;
            }

            return workbook;
            
            
            

            
            
            

            
        }

        public static string 縮排文字 (string value, int amtOfSpece)
        {
            for (int i = 0; i < amtOfSpece; i++)
            {
                value = "　" + value;
            }
            return value;
        }

        public static Stream RenderDataTableToExcel(DataTable SourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);

            
            foreach (DataColumn column in SourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            
            int rowIndex = 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);

                foreach (DataColumn column in SourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }

        public static void RenderDataTableToExcel(DataTable SourceTable, string FileName)
        {
            MemoryStream ms = RenderDataTableToExcel(SourceTable) as MemoryStream;
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();

            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();

            data = null;
            ms = null;
            fs = null;
        }

        public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, string SheetName, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheet(SheetName);

            DataTable table = new DataTable();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                    dataRow[j] = row.GetCell(j).ToString();
            }

            ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(SheetIndex);

            DataTable table = new DataTable();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                    
                }

                table.Rows.Add(dataRow);
            }

            ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }
    }
}
