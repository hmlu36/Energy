namespace Energy.Models.ViewModels
{
    /// <summary>
    /// 分頁模式
    /// </summary>
    public class PagerModel
    {
        public int TotalRecords { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public int PageSize { get; set; }
        public int Page { get; set; }

        public int Skip
        {
            get
            {
                if (Page <= 1)
                {
                    return 0;
                }
                else
                {
                    return (Page - 1) * PageSize;
                }
            }
        }

        public int Take
        {
            get
            {
                return PageSize;
            }
        }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecords / (double)PageSize);
            }
        }

        public PagerModel()
        {
            PageSize = 50;
            Page = 1;
        }
    }
}
