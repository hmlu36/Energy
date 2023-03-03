namespace Energy.Models.ViewModels
{
    public class ResultModel<T>
    {
        /// <summary>
        /// 執行成功或失敗
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 回傳資料
        /// </summary>
        public T Data { get; set; }
    }
}
