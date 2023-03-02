using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data;

namespace JamZoo.Project.WebSite.ViewModels
{
    using Models;
    public class InquireListPage
    {
        public string LastUpdate { get; set; }
        public ParentListModel Data { get; set; }
        public string Id { get; set; }
        public string Start { get; set; }
        public string TempStart { get; set; }
        public string End { get; set; }
        public string TempEnd { get; set; }

        public string YearOrMonth { get; set; }

        
        
        
        
        
        public int UnitType { get; set; }
        public string ChartType { get; set; }
        public InquireListPage()
        {
            YearOrMonth = "Y";
            ChartType = "1";
            UnitType = 0;
            
            
            


            this.Start = (DateTime.Now.Year - 1911 - 10).ToString() + "01";
            
            

        }



        
        
        
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
        #region Date Time Config
        public DateTime RangeStart
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Start))
                {
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

        #endregion
    }

}
