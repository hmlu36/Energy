using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamZoo.Project.WebSite.ViewModels
{
    public static class EnergyList
    {
        public static Dictionary<int, Energy> Data = new Dictionary<int, Energy>() {
            { 0, new Energy { Id = 0, Name = "總體能源供需", Css = "purple_acc" } },
            { 1, new Energy { Id = 0, Name = "煤及煤產品", Css = "gray_acc" } },
            { 2, new Energy { Id = 0, Name = "原油及石油產品", Css = "coffee_acc" } },
            { 3, new Energy { Id = 0, Name = "天然氣", Css = "blue_acc" } },
            { 4, new Energy { Id = 0, Name = "電力", Css = "yellow_acc" } },
            { 5, new Energy { Id = 0, Name = "生質能及廢棄物", Css = "green1_acc" } },
            { 6, new Energy { Id = 0, Name = "太陽熱能", Css = "green2_acc" } },
            { 7, new Energy { Id = 0, Name = "熱能", Css = "pink_acc" } },
            { 8, new Energy { Id = 0, Name = "能源指標", Css = "pink_acc" } },
            { 9, new Energy { Id = 0, Name = "溫室氣體", Css = "pink_acc" } },
        };
    }

    public class Energy
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Css { get; set; }
    }
}