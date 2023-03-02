namespace Energy.Models.ViewModels
{
    /// <summary>
    /// 資料庫查詢條件
    /// </summary>
    public class DatabaseCriteria
    {
        /// <summary>
        /// 年度
        /// </summary>
        public string YearType { get; set; }

        /// <summary>
        /// 週期
        /// </summary>
        public string PeriodType { get; set; }

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
        public List<string> EnergySelectedValue { get; set; }

        /// <summary>
        /// 選取流程別類型
        /// </summary>
        public List<string> FlowSelectedValue { get; set; }
    }
}
