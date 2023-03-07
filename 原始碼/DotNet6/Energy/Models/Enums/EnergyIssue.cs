using System.ComponentModel;

namespace Energy.Models.Enums
{
    public enum EnergyIssue
    {
        /// <summary>
        /// 能源供需
        /// </summary>
        [Description("能源供需")]
        EnergySupplyDemand = 1,

        /// <summary>
        /// 能源進口來源
        /// </summary>
        [Description("能源進口來源")]
        EnergyImportSource = 6,

        /// <summary>
        /// 常用能源指標
        /// </summary>
        [Description("常用能源指標")]
        CommonEnergyIndicators = 5,

        /// <summary>
        /// 發電量
        /// </summary>
        [Description("發電量")]
        PowerGeneration = 2,

        /// <summary>
        /// 發電裝置容量
        /// </summary>
        [Description("發電裝置容量")]
        PowerCapacity = 3,

        /// <summary>
        /// 發電燃料投入
        /// </summary>
        [Description("發電燃料投入")]
        PowerFuelInput = 4
    }
}
