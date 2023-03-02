using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamZoo.Project.WebSite.ViewModels
{
    public static class DatabaseTypeList
    {
        public static Dictionary<int, DatabaseType> Data = new Dictionary<int, DatabaseType>() {
            { 0, new DatabaseType { Id = 0, Name = "能源供需", DisplayName = "能源供需-OECD格式", Css = "", Left = "能源別", Right = "流程別", UnitList = "原始單位,熱值單位,油當量單位" } },
            { 1, new DatabaseType { Id = 1, Name = "能源進口來源", DisplayName = "能源進口來源", Left = "能源別", Right = "", UnitList = "原始單位,," } },
            { 2, new DatabaseType { Id = 2, Name = "常用能源指標", DisplayName = "常用能源指標", Left = "項目", Right = "", UnitList = "原始單位,," } },

            { 3, new DatabaseType { Id = 3, Name = "發電量", DisplayName = "發電量", Left = "類別", Right = "能源別", UnitList = "度,," } },
            { 4, new DatabaseType { Id = 4, Name = "發電裝置容量", DisplayName = "發電裝置容量", Left = "類別", Right = "能源別", UnitList = "千瓩,," } },
           
            { 5, new DatabaseType { Id = 5, Name = "發電燃料投入", DisplayName = "發電燃料投入", Left = "能源別", Right = "類別", UnitList = "原始單位,熱值單位,油當量單位" } },
        };
    }

    
    
    
    public class DatabaseType
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }

        public string Css { get; set; }

        
        
        
        public string Left { get; set; }

        
        
        
        public string Right { get; set; }

        
        
        
        public bool ReverseLeftRight { get; set; }

        public string UnitList { get; set; }

        public string[] UnitListAry {
            get
            {
                if (!string.IsNullOrEmpty(this.UnitList))
                {
                    return this.UnitList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return new string[3];
                }
            }
        }

        public DatabaseType()
        {
            ReverseLeftRight = false;
        }
    }
}
