using System.ComponentModel;

namespace Energy.Models.Enums
{
    /// <summary>
    /// 週期別
    /// </summary>
    public enum PeriodType
    {
        [Description("月")]
        M,

        [Description("季")]
        Q,

        [Description("年")]
        Y
    }
}
