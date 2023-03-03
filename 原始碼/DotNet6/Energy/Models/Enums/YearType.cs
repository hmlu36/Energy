using System.ComponentModel;

namespace Energy.Models.Enums
{
    /// <summary>
    /// 年度別
    /// </summary>
    public enum YearType
    {
        [Description("民國年")]
        AD,

        [Description("西元年")]
        ROC
    }
}
