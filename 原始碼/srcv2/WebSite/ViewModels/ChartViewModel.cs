using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamZoo.Project.WebSite.ViewModels
{
    public class CHARTCONFIG
    {
        public static Dictionary<int, string> PAGE_CONFIG = new Dictionary<int, string>()
        {
            
            
            
            
            
            
            

            {   1, "能源供給" },
            {   3, "電力供給" },
            {   4, "能源消費" },
            {   6, "電力消費" },
            {   7, "能源指標" },
            {   8, "溫室氣體" }
        };


        public static Dictionary<string, Dictionary<string, string>> CONFIG = new Dictionary<string, Dictionary<string, string>>()
        {
            #region All CONFIG
            
            #region Page1
            {
                "能源供給能源供給結構(圓餅圖)",
                new Dictionary<string, string>()
                {
                    { "type", "pie" },
                    { "chartNameL", "chart1" },
                    { "chartNameR", "chart2" },
                    { "subTitle", "單位：%" },
                }
            },
            {
                "能源供給能源供給量(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart3" },
                    { "subTitle", "單位 : 萬公秉油當量" },
                }
            },
            #endregion

            #region Page2

            
            {
                "能源供給煤炭進口來源(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart4" },
                    { "subTitle", "單位 : 百萬公噸" },
                }
            },
            {
                "能源供給原油進口來源(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart5" },
                    { "subTitle", "單位 : 百萬桶" },
                }
            },
            {
                "能源供給天然氣進口來源(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart6" },
                    { "subTitle", "單位 : 百萬公噸" },
                }
            },
            #endregion

            #region Page 3

            
            {
                "電力供給發電結構(圓餅圖)",
                new Dictionary<string, string>()
                {
                    { "type", "pie" },
                    { "chartNameL", "chart1" },
                    { "chartNameR", "chart2" },
                    { "subTitle", "單位：%" },
                }
            },
            {
                "電力供給發電量(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart3" },
                    { "subTitle", "單位 : 億度" },
                }
            },
            {
                "電力供給再生能源發電結構(圓餅圖)",
                new Dictionary<string, string>()
                {
                    { "type", "pie" },
                    { "chartNameL", "chart4" },
                    { "chartNameR", "chart5" },
                    { "subTitle", "單位：%" },
                }
            },
            {
                "電力供給再生能源發電量(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart6" },
                    { "subTitle", "單位 : 億度" },
                }
            },
            {
                "電力供給再生能源變動趨勢(折線圖)",
                new Dictionary<string, string>()
                {
                    { "type", "line" },
                    { "chartId", "chart7" },
                    { "subTitle", "單位 : %" },
                }
            },

            {
                "電力供給發電裝置容量結構(圓餅圖)",
                new Dictionary<string, string>()
                {
                    { "type", "pie" },
                    { "chartNameL", "chart8" },
                    { "chartNameR", "chart9" },
                    { "subTitle", "單位：%" },
                }
            },
            {
                "電力供給發電裝置容量(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart10" },
                    { "subTitle", "單位 : 千瓩" },
                }
            },


            {
                "電力供給再生能源發電裝置容量結構(圓餅圖)",
                new Dictionary<string, string>()
                {
                    { "type", "pie" },
                    { "chartNameL", "chart11" },
                    { "chartNameR", "chart12" },
                    { "subTitle", "單位：%" },
                }
            },


            {
                "電力供給再生能源發電裝置容量(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart13" },
                    { "subTitle", "單位 : 千瓩" },
                }
            },

            {
                "電力供給電力備用容量率",
                new Dictionary<string, string>()
                {
                    { "type", "columnLine" },
                    { "htmlName", "_columnLine" },
                    { "chartId", "chart14" },
                    { "subTitle", "單位 : %" },
                    { "colNameLine", "電力備用容量率" },
                    { "colNameColumn", "政府核定目標值" },
                }
            },
            #endregion

            #region Page 4

            
            {
                "能源消費能源別國內能源消費結構(圓餅圖)",
                new Dictionary<string, string>()
                {
                    { "type", "pie" },
                    { "chartNameL", "chart1" },
                    { "chartNameR", "chart2" },
                    { "subTitle", "單位：%" },
                }
            },
            {
                "能源消費能源別國內能源消費量(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart3" },
                    { "subTitle", "單位 : 百萬公秉油當量" },
                }
            },

            #endregion

            #region Page 5


            
            {
                "能源消費部門別國內能源消費結構(圓餅圖)",
                new Dictionary<string, string>()
                {
                    { "type", "pie" },
                    { "chartNameL", "chart4" },
                    { "chartNameR", "chart5" },
                    { "subTitle", "單位：%" },
                }
            },
            {
                "能源消費部門別國內能源消費量(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart6" },
                    { "subTitle", "單位 : 百萬公秉油當量" },
                }
            },
            #endregion

            #region Page6 

            
            {
                "電力消費電力消費結構(圓餅圖)",
                new Dictionary<string, string>()
                {
                    { "type", "pie" },
                    { "chartNameL", "chart1" },
                    { "chartNameR", "chart2" },
                    { "subTitle", "單位：%" },
                }
            },
            {
                "電力消費電力消費量(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart3" },
                    { "subTitle", "單位 : 億度" },
                }
            },

            #endregion

            #region Page 7

            
            {
                "能源指標能源生產力與密集度",
                new Dictionary<string, string>()
                {
                    { "type", "columnLine" },
                    { "htmlName", "_columnLine71" },
                    { "chartId", "chart1" },
                    { "subTitle", "" },
                    { "colNameLine", "能源生產力(元/公升油當量)" },
                    { "colNameColumn", "能源密集度(公升油當量/千元)" },
                }
            },
                {
                "能源指標人均能源消費量與每人實質GDP",
                new Dictionary<string, string>()
                {
                    { "type", "columnLine" },
                    { "htmlName", "_columnLine72" },
                    { "chartId", "chart2" },
                    { "subTitle", "" },
                    { "colNameLine", "平均每人能源消費量(公升油當量/人)" },
                    { "colNameColumn", "平均每人GDP(千元/人)" },
                }
            },
                  {
                "能源指標人均能源消費量與用電量",
                new Dictionary<string, string>()
                {
                    { "type", "columnLine" },
                    { "htmlName", "_columnLine73" },
                    { "chartId", "chart3" },
                    { "subTitle", "" },
                    { "colNameLine", "平均每人能源消費量(公升油當量/人)" },
                    { "colNameColumn", "平均每人用電量(度/人)" },
                }
            },

            {
                "能源指標能源依存度(折線圖)",
                new Dictionary<string, string>()
                {
                    { "type", "line" },
                    { "chartId", "chart4" },
                    { "subTitle", "單位 : %" },
                }
            },
            {
                "能源指標能源集中度(折線圖)",
                new Dictionary<string, string>()
                {
                    { "type", "line" },
                    { "chartId", "chart5" },
                    { "subTitle", "單位 : %" },
                }
            },
            {
                "能源指標平均每人負擔能源進口值(折線圖)",
                new Dictionary<string, string>()
                {
                    { "type", "line" },
                    { "chartId", "chart6" },
                    { "subTitle", "單位 : 新臺幣萬元" },
                }
            },
            {
                "能源指標進口能源總值(折線圖)",
                new Dictionary<string, string>()
                {
                    { "type", "line" },
                    { "chartId", "chart7" },
                    { "subTitle", "單位 : 百萬美元" },
                }
            },

            #endregion

            #region Page 8
            
            {
                "溫室氣體電力排放係數(折線圖)",
                new Dictionary<string, string>()
                {
                    { "type", "line" },
                    { "chartId", "chart1" },
                    { "subTitle", "單位 : CO<sub>2</sub>e/度" },
                }
            },

             {
                "溫室氣體二氧化碳排放量(部門別含電-部門法)(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart2" },
                    { "subTitle", "單位 : 百萬公噸" },
                }
            },
                {
                "溫室氣體二氧化碳排放量(燃料別-參考法)(長條圖)",
                new Dictionary<string, string>()
                {
                    { "type", "column" },
                    { "chartId", "chart3" },
                    { "subTitle", "單位 : 百萬公噸" },
                }
            },
	        #endregion
            #endregion

        };
    }

    public class PageOfChartViewModel
    {
        public int Id { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string PageName { get; set; }

        public List<ChartViewModel> ChartList { get; set; }
        public PageOfChartViewModel(int Id, int Start, int End)
        {
            this.Id = Id;
            this.PageName = CHARTCONFIG.PAGE_CONFIG[Id];
            this.Start = Start;
            this.End = End;
            ChartList = new List<ChartViewModel>();
        }
    }

    public class ChartViewModel
    {
        public static Dictionary<string, string> COLOR = new Dictionary<string, string>()
        {
{ "煤及煤產品", "#a5a5a5" },
{ "澳大利亞", "#1a2d68" },
{ "沙烏地阿拉伯", "#006c35" },
{ "卡達", "#6e183b" },
{ "抽蓄水力", "#00b6be" },
{ "慣常水力", "#66cad9" },
{ "能源部門自用", "#49a148" },
{ "煤炭", "#a5a5a5" },
{ "原油及石油產品", "#ce5f46" },
{ "印尼", "#c01a28" },
{ "科威特", "#009977" },
{ "馬來西亞", "#ffcd00" },
{ "燃煤", "#a5a5a5" },
{ "地熱", "#89b875" },
{ "石油產品", "#ce5f46" },
{ "工業部門", "#959da1" },
{ "原油", "#ce5f46" },
{ "天然氣", "#47bbf7" },
{ "俄羅斯", "#2f3b87" },
{ "美國", "#db241c" },
{ "燃油", "#ce5f46" },
{ "太陽光電", "#c9c92c" },
{ "運輸部門", "#915c3f" },
{ "核能", "#eb8927" },
{ "南非", "#ffb81c" },
{ "阿聯大公國", "#00742b" },
{ "燃氣", "#47bcf7" },
{ "風力", "#759f89" },
{ "生質能及廢棄物", "#009b63" },
{ "農業部門", "#ffcc4e" },
{ "再生能源", "#76b54b" },
{ "加拿大", "#ff0000" },
{ "伊拉克", "#febe08" },
{ "生質能", "#07764a" },
{ "電力", "#fdb933" },
{ "服務業部門", "#a865a9" },
{ "中國大陸", "#f6e61f" },
{ "阿曼", "#008a1d" },
{ "巴布亞紐幾內亞", "#000000" },
{ "廢棄物", "#399b3f" },
{ "太陽熱能", "#79975d" },
{ "住宅部門", "#f56d1f" },
{ "其他", "#66cc99" },
{ "熱能", "#d77f99" },
{ "非能源消費", "#a29548" },
{ "水力", "#0075b2" },
{ "屋頂型", "#a6ce38" },
{ "地面型", "#6fba2c" },
{ "離岸", "#9bd3ae" },
{ "陸域", "#76f2da" },
{ "化石燃料", "#823c0b" },
{ "氣體燃料", "#47bbf7" },
{ "液體燃料", "#ce5f46" },
{ "固體燃料", "#a5a5a5" },
{ "石油依存度", "#ed5007" },
{ "進口能源依存度", "#242aa3" },
{ "發電能源種類集中度", "#ed5007" },
{ "能源供應種類集中度", "#242aa3" },
        };

        public static string GetColor (string key)
        {
            if (COLOR.ContainsKey(key))
                return COLOR[key];
            else
                return "#33a8b6";
        }

        
        public string Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public string Type { get; set; }

        
        public string htmlName { get; set; }

        public List<string> CategoriesOfColumnData { get; set; }
        public Dictionary<string, double> PieData { get; set; }

        public Dictionary<string, List<double>> ColumnData { get; set; }

        
        public string ColumnAName { get; set; }
        public List<double> ColumnAData { get; set; }
        public string ColumnBName { get; set; }

        public List<double> ColumnBData { get; set; }

        public ChartViewModel()
        {
            this.PieData = new Dictionary<string, double>();
            this.ColumnData = new Dictionary<string, List<double>>();
        }
    }
}
