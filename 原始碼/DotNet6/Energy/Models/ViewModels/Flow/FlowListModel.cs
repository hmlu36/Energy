namespace Energy.Models.ViewModels.Flow
{
    public class FlowListModel : PagerModel
    {
        public string Search { get; set; }

        public string[] SelectedValues { get; set; }

        public List<FlowModel> Data { get; set; }
    }
}
