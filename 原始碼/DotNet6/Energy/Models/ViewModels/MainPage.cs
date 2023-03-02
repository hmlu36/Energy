namespace Energy.Models.ViewModels
{
    public class MainPage
    {
        public static Dictionary<string, Dictionary<int, string>> UnitList = new Dictionary<string, Dictionary<int, string>>()
        {
            {
                "能源供需",
                new Dictionary<int, string>()
                {
                    {  0, "原始單位" },
                    {  1, "熱值單位" },
                    {  2, "油當量單位" }
                }
            }
        };
    }
}
