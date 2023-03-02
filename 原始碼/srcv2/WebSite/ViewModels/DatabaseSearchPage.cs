using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace JamZoo.Project.WebSite.ViewModels
{
    using DbContext;
    using Models;
    public class MainPage
    {
        public static Dictionary<string, Dictionary<int, string>> UnitList = new Dictionary<string, Dictionary<int, string>>()
        {
            {
                "能源供需", 
                new Dictionary<int, string>()
                {
                    {  0, "原始單位" },
                    {  1, "熱值單位" },
                    {  2, "油當量單位" }
                }
            }
        };

    }
    public class DatabaseSearchPage
    {
        
        
        
        
        
        
        
        public static string 日期標頭轉換 (string ori, string PeriodType, string yearType)
        {
            string temp = string.Empty;

            
            
            
            
            
            
            
            

            if (PeriodType == "M")
            {

            }

            temp += ori;

            return temp;
        }
        public static string 季別轉換(string ori)
        {
            string temp = ori;
            temp = temp.Replace("Q1", "第一季");
            temp = temp.Replace("Q2", "第二季");
            temp = temp.Replace("Q3", "第三季");
            temp = temp.Replace("Q4", "第四季");
            return temp;
        }

        
        
        
        public string LastUpdate { get; set; }

        
        
        
        public string DdlLastDate { get; set; }

        public int GetLastUpdate民國年
        {
            get
            {
                int _DEFAULT = 109;
                if (!string.IsNullOrEmpty(DdlLastDate))
                {
                    _DEFAULT = int.Parse(DdlLastDate.Substring(0, 3));
                }
                return _DEFAULT;
            }
        }

        public int GetLastUpdate民國月
        {
            get
            {
                int _DEFAULT = 08;

                if (!string.IsNullOrEmpty(DdlLastDate))
                {
                    _DEFAULT = int.Parse(DdlLastDate.Substring(4, 2));
                }
                return _DEFAULT;
            }
        }

        
        
        
        
        public string GetCacheKey ()
        {
            string KEY = string.Format("{0}{1}{2}{3}{4}{5}{6}", 
                this.PageId,
                this.Start ?? string.Empty,
                this.End ?? string.Empty,
                this.PeriodType ?? string.Empty,
                this.UnitType,
                 this.FlowSelectedValue != null ?string.Join("_", this.FlowSelectedValue) : string.Empty,
                 this.EnergySelectedValue != null ?string.Join("_", this.EnergySelectedValue) : string.Empty
               );

            return KEY;
        }

        public DatabaseType PageInfo 
        {
            get
            {
                return DatabaseTypeList.Data[this.PageId];
            }
        }
        public int PageId { get; set; }

        
        
        
        public int UnitType { get; set; }

        
        
        
        public string yearType { get; set; }

        
        
        
        public string PeriodType { get; set; }

        
        
        
        public string Start { get; set; }

        
        
        
        public string StartForDisplay { get; set; }

        
        
        
        public string End { get; set; }

        
        
        
        public string EndForDisplay { get; set; }

        #region Date Time Config
        public DateTime RangeStart
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Start)) {

                    DateTime _RANGE_START = DateTime.ParseExact(this.Start + "01", "yyyMMdd", CultureInfo.InvariantCulture).AddYears(1911);
                    return _RANGE_START;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public DateTime RangeEnd
        {
            get
            {
                if (!string.IsNullOrEmpty(this.End))
                {
                    DateTime _RANGE_END = DateTime.ParseExact(this.End + "01", "yyyMMdd", CultureInfo.InvariantCulture).AddYears(1911);
                    return _RANGE_END;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public int RangeTotalMonth
        {
            get
            {
                
                int _RANGE_TOTALMONTH = ((this.RangeEnd.Year - this.RangeStart.Year) * 12) + this.RangeEnd.Month - this.RangeStart.Month;
                return _RANGE_TOTALMONTH;

            }
        }

        public int RangeTotalQuarterly
        {
            get
            {
                return Convert.ToInt32(Math.Ceiling((float)(this.RangeTotalMonth/ 3)));
            }
        }

        public int RangeTotalYear
        {
            get
            {
                DateTime zeroTime = new DateTime(1, 1, 1);
                try
                {
                    return (zeroTime + this.RangeEnd.Subtract(this.RangeStart)).Year - 1;
                } 
                catch
                {
                    return 0;
                }
            }
        }

        public void InitTableHeader(System.Data.DataTable table, CultureInfo culture)
        {
            if (this.PeriodType.ToLower() == "m")
            {

                for (int i = 0; i < this.RangeTotalMonth; i++)
                {
                    DateTime r1 = this.RangeStart.AddMonths(i);
                    string display = r1.ToString("yyyMM", culture);
                    table.Columns.Add(display, typeof(double));
                    if (i == 0)
                    {
                        this.StartForDisplay = display;
                    } 
                    if (i == this.RangeTotalMonth - 1)
                    {
                        this.EndForDisplay = display;
                    }
                }
            }
            else if (this.PeriodType.ToLower() == "q")
            {
                for (int i = 0; i < this.RangeTotalQuarterly; i++)
                {
                    DateTime r1 = this.RangeStart.AddMonths(i * 3);
                    string display = string.Format("{0}Q{1}",
                        r1.ToString("yyy", culture),
                        Math.Ceiling((float)r1.Month / 3)
                       );
                    table.Columns.Add(display, typeof(double));
                    if (i == 0)
                    {
                        this.StartForDisplay = display;
                    }
                    if (i == this.RangeTotalQuarterly - 1)
                    {
                        this.EndForDisplay = display;
                    }
                }
            }
            else if (this.PeriodType.ToLower() == "y")
            {
                for (int i = 0; i < this.RangeTotalYear; i++)
                {
                    DateTime r1 = this.RangeStart.AddYears(i);
                    string display = r1.ToString("yyy", culture);
                    table.Columns.Add(display, typeof(double));
                    if (i == 0)
                    {
                        this.StartForDisplay = display;
                    }
                    if (i == this.RangeTotalYear - 1)
                    {
                        this.EndForDisplay = display;
                    }
                }
            }

        }
        #endregion

        public string PageName
        {
            get
            {
                return ViewModels.DatabaseTypeList.Data[PageId].Name;
            }
        }
        
        public string[] EnergySelectedValue { get; set; }
        public string[] FlowSelectedValue { get; set; }

        public List<DropItem> EnergyDropDownList { get; set; }

        public List<DropItem> FlowDropDownList { get; set; }


        
        
        
        public List<DatabaseDetailPage> DetailPages { get; set; }

        public int DetailI { get; set; }

        
        
        
        public string ChartType { get; set; }


        public List<wesdes50_db> OriginData_Db { get; set; }
        public List<power50_db> PowerData_Db { get; set; }

        public List<power50> PowerData { get; set; }


        
        
        
        public List<energy50_db> EnergyData_Db { get; set; }

        
        
        
        public List<fuel50_db> FuelData_Db { get; set; }

        
        
        
        
        public List<coal50> CoalData { get; set; }


        
        
        
        public object PageRowType
        {
            get
            {
                Type t = Type.GetType("JamZoo.Project.WebSite.Controllers.DatabaseRow.Page" + this.PageId.ToString());
                object o = Activator.CreateInstance(t);
                return o;
            }
        }


        public DatabaseSearchPage  ()
        {
            
            
        }

    }

    public class DropItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        
        
        
        public string SelectName { get; set; }

        public int Depth { get; set; }

        public List<DropItem> ChildList { get; set; }

        public bool ShowCheckBox { get; set; }

        public bool HasChild
        {
            get
            {
                return ChildList.Count() > 0;
            }
        }

        public DropItem (string SelectName)
        {
            ChildList = new List<DropItem>();
            this.SelectName = SelectName;
            this.ShowCheckBox = true;
        }
    }
}
