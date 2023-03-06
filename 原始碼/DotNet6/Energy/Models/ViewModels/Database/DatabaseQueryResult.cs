namespace Energy.Models.ViewModels.Database
{
    public class DatabaseQueryResult
    {
        /// <summary>
        /// 項目標題
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// 標題
        /// </summary>
        public IEnumerable<dynamic> Header { get; set; } = null!;

        /// <summary>
        /// 內容
        /// </summary>
        public IEnumerable<dynamic> Content { get;set; } = null!;
    }
}
