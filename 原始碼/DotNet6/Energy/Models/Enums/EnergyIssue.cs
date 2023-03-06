using System.ComponentModel;

namespace Energy.Models.Enums
{
    public enum EnergyIssue
    {
        [Description("能源供需")]
        EnergySupplyDemand = 1,

        [Description("能源進口來源")]
        EnergyImportSource = 6,

        [Description("常用能源指標")]
        CommonEnergyIndicators = 5,

        [Description("發電量")]
        PowerGeneration = 2,

        [Description("發電裝置容量")]
        PowerCapacity = 3,

        [Description("發電燃料投入")]
        PowerFuelInput = 4
    }
}
