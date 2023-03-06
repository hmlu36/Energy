namespace Energy.Models.ViewModels.Database
{
    public class DatabaseQueryResult
    {
        public string Title { get; set; } = null!;
        public IEnumerable<dynamic> Header { get; set; } = null!;
        public IEnumerable<dynamic> Content { get;set; } = null!;
    }
}
