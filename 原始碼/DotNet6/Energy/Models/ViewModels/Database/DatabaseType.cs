using Energy.Models.Enums;

namespace Energy.Models.ViewModels.Database
{
    /// <summary>
    /// 資料庫查詢類型
    /// </summary>
    public class DatabaseType
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 顯示名稱
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 左邊選單標題
        /// </summary>
        public string Left { get; set; }

        /// <summary>
        /// 右邊選單標題
        /// </summary>
        public string Right { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public List<string> UnitList { get; set; } = new List<string>();

        /// <summary>
        /// 能源別下拉選單
        /// </summary>
        public IEnumerable<DropItem> EnergyDropDownList { get; set; }

        /// <summary>
        /// 流程別下拉選單
        /// </summary>
        public IEnumerable<DropItem> FlowDropDownList { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 最後更新日期
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// 下拉選單最新時間
        /// </summary>
        public DateTime DdlLastDate { get; set; }

        /// <summary>
        /// 年分別
        /// </summary>
        public IEnumerable<YearType> YearTypes { get; set; }

        /// <summary>
        /// 季節別
        /// </summary>
        public IEnumerable<PeriodType> PeriodTypes { get; set; }
    }
}
