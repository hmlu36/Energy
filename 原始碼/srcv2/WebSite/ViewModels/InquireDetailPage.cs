using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data;

namespace JamZoo.Project.WebSite.ViewModels
{
    using Models;
    using DbContext;

    public class InquireDetailPage
    {
        #region 國家代碼對應表_資料庫欄位轉國家

        public static Dictionary<string, string> CountryCodeNameMapping = new Dictionary<string, string>() {
          { "fc", "剛果" },
            { "mk", "科威特" },
            { "mq", "伊拉克" },
            { "sa", "阿根廷" },
            /*
             2021.06.28 客戶需求 # 只要把國家註解掉就不會被撈到
             1.前台不顯示（包括含下載的文件）
            ．原油進口來源國的阿拉斯加
            ．液化天然氣的阿拉斯加
            */
            
            { "ed", "丹麥" },
            { "ff", "南非" },
            { "ai", "印尼" },
            { "fss", "南蘇丹" },
            { "fh", "查德" },
            { "am", "馬來西亞" },
            { "pb", "巴布亞紐幾內亞" },
            { "mz", "科沙中立區" },
            { "pa", "澳大利亞" },
            { "aj", "日本" },
            { "my", "葉門" },
            { "ab", "汶萊" },
            { "np", "巴拿馬" },
            { "fn", "奈及利亞" },
            { "na", "美國" },
            { "ms", "沙烏地阿拉伯" },
            { "fl", "利比亞" },
            { "ek", "西班牙" },
            { "mu", "阿聯大公國" },
            { "au", "烏克蘭" },
            { "ea", "奧地利" },
            { "en", "挪威" },
            { "fo", "安哥拉" },
            { "aa", "亞塞拜然" },
            { "fs", "蘇丹" },
            { "fk", "喀麥隆" },
            { "sbr", "巴西" },
            { "eg", "德國" },
            { "ak", "南韓" },
            { "ail", "以色列" },
            { "nc", "加拿大" },
            { "ag", "喬治亞共和國" },
            { "fu", "赤道幾內亞" },
            { "ah", "哈薩克" },
            { "eb", "比利時" },
            { "sc", "哥倫比亞" },
            { "fmz", "莫三比克" },
            { "sh", "智利" },
            { "ac", "中國大陸" },
            { "av", "越南" },
            { "se", "厄瓜多爾" },
            { "atr", "土耳其" },
            { "fg", "加彭" },
            { "sm", "墨西哥" },
            { "sp", "祕魯" },
            { "mo", "阿曼" },
            { "at", "泰國" },
            { "su", "烏拉圭" },
            { "ap", "菲律賓" },
            { "fe", "埃及" },
            { "eu", "俄羅斯" },
            { "zz", "其他國家" },
            { "z1", "其他國家" },
            { "z2", "其他國家" },
            { "z3", "其他國家" },
            { "z4", "其他國家" },
            { "ee", "英國" },
            { "fa", "阿爾及利亞" },
            { "mr", "卡達" },
            { "ain", "印度" },
            { "mn", "伊朗" },
            { "scl", "千里達及托巴哥共和國" },
            { "total", "進口總量" },
            { "ank", "北韓" },
            { "amo", "蒙古" },
            { "fr", "加納共和國" },

        };

        #endregion

        #region 國家代碼對應表_國家中文名轉英文代碼

        public static Dictionary<string, string> CountryNameToCountryCode = new Dictionary<string, string>() {
            { "剛果", "cg" },
            { "科威特", "kw" },
            { "伊拉克", "iq" },
            { "阿根廷", "ar" },
            { "阿拉斯加", "us" },
            { "丹麥", "dk" },
            { "南非", "za" },
            { "印尼", "id" },
            { "南蘇丹", "ss" },
            { "查德", "td" },
            { "馬來西亞", "my" },
            { "巴布亞紐幾內亞", "pg" },
            { "科沙中立區", "0" },
            { "澳大利亞", "au" },
            { "日本", "jp" },
            { "葉門", "ye" },
            { "汶萊", "bn" },
            { "巴拿馬", "pa" },
            { "奈及利亞", "ng" },
            { "美國", "us" },
            { "沙烏地阿拉伯", "sa" },
            { "利比亞", "ly" },
            { "西班牙", "es" },
            { "阿聯大公國", "ae" },
            { "烏克蘭", "ua" },
            { "奧地利", "ez" },
            { "挪威", "no" },
            { "安哥拉", "ao" },
            { "亞塞拜然", "az" },
            { "蘇丹", "sd" },
            { "喀麥隆", "cm" },
            { "巴西", "br" },
            { "德國", "de" },
            { "南韓", "kr" },
            { "以色列", "il" },
            { "加拿大", "ca" },
            { "喬治亞共和國", "ge" },
            { "赤道幾內亞", "gq" },
            { "哈薩克", "kz" },
            { "比利時", "be" },
            { "哥倫比亞", "co" },
            { "莫三比克", "mz" },
            { "智利", "cl" },
            { "中國大陸", "cn" },
            { "越南", "vn" },
            { "厄瓜多爾", "ec" },
            { "土耳其", "tr" },
            { "加彭", "ga" },
            { "墨西哥", "mx" },
            { "祕魯", "pe" },
            { "阿曼", "om" },
            { "泰國", "th" },
            { "烏拉圭", "uy" },
            { "菲律賓", "ph" },
            { "埃及", "eg" },
            { "俄羅斯", "ru" },
            { "其他國家", "zz" },
            { "英國", "gb" },
            { "阿爾及利亞", "dz" },
            { "卡達", "qa" },
            { "印度", "in" },
            { "伊朗", "ir" },
            { "千里達及托巴哥共和國", "tt" },
            { "進口總量", "0" },
            { "北韓", "kp" },
            { "蒙古", "mn" },
            { "加納共和國", "gh" },
        };
        #endregion

        
        
        
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




        
        public static List<string> EnergyParentIdList = new List<string>() {
            "P003",
            "P004", "P005",
            "P006", "P007",
            "P008", "P009",

            
            "P801",
            "P802",
            "P803",
            "P804",
            "P901",
            "P902",
        };

        
        public static List<string> EnergyParentIdListOnlyForYear = new List<string>() {
            "P004", "P008", "P009"
        };

        public static List<string> 進口來源國的子層編號 = new List<string>() {
            "P105", "P106", "P107", "P104", "P205", "P305", "P108"
        };

        public string Id { get; set; }
        public string Start { get; set; }
        public string TempStart { get; set; }
        public string End { get; set; }
        public string TempEnd { get; set; }

        public string YearOrMonth { get; set; }

        
        
        
        
        
        public int UnitType { get; set; }

        #region Date Time Config
        public DateTime RangeStart {
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

        public string ChartType { get; set; }


        #region Phase2 的新結構
        public ParentModel Parent { get; set; }

        public ChildListModel ChildList { get; set; }

        public  List<ChildModel> RowForChild { get; set; }

        #endregion

        public ChildModel Child { get; set; }

        public List<wesdes50> Data { get; set; }
        public List<power50> PowerData { get; set; }

        public List<energy50_db> EnergyData { get; set; }

        
        
        
        public List<coal50> CoalData { get; set; }

        public DataTable Table { get; set; }

        public InquireDetailPage()
        {
            YearOrMonth = "Y";
            ChartType = "1";
            UnitType = 0;
            
            
        }
    }
}
