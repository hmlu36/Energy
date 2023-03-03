using Energy.Models.Enums;

namespace Energy.Models.ViewModels.Database
{
    /// <summary>
    /// 資料庫查詢條件
    /// </summary>
    public class DatabaseCriteria
    {
        /// <summary>
        /// 年度
        /// </summary>
        public YearType YearType { get; set; }

        /// <summary>
        /// 週期
        /// </summary>
        public PeriodType PeriodType { get; set; }

        /// <summary>
        /// 起始日
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// 結束日
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string UnitType { get; set; }

        /// <summary>
        /// 選取能源別類型
        /// </summary>
        public List<string> EnergySelectedValue { get; set; } = new List<string>() { "1_1" };

        /// <summary>
        /// 選取流程別類型
        /// </summary>
        public List<string> FlowSelectedValue { get; set; } = new List<string>() { "fl_1_3_6", "fl_1_3_7", "fl_1_4" };
    }
}
