using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JamZoo.Project.WebSite.ViewModels
{
    using Models;

    
    
    
    public class DatabaseDetailPage
    {
        public string GetColumnNameExt (DatabaseSearchPage m, DatabaseDetailPage d, string ColumnName)
        {

            if (m.PageId == 3 && m.UnitType == 0)
            {
                string[] temp = d.EnergyInfo.UnitUpperAry[m.UnitType].Split(new char[] { '@' });
                if (temp.Length != 4) return string.Empty;

                
                
                
                
                if (ColumnName == "總計")
                {
                    return string.Format("({0})", temp[0]);
                }
                else if (ColumnName == "台電公司")
                {
                    return string.Format("({0})", temp[1]);
                }
                else if (ColumnName == "民營電廠")
                {
                    return string.Format("({0})", temp[2]);
                }
                else if (ColumnName == "自用發電設備")
                {
                    return string.Format("({0})", temp[3]);
                }

            }

            return string.Empty;
        }

        public static string GetThClass(int Depth)
        {
            
            
            
            if (Depth == 0)
            {
                return "fixed_side";
            }
            else if (Depth == 1)
            {
                return "table_sub_indent";
            }
            else
            {
                return "table_sub_indent" + Depth.ToString();
            }
        }

        
        
        
        public EnergyModel EnergyInfo { get; set; }

        
        
        
        public DataTable Table { get; set; }

        
        
        
        public string MapJson { get; set; }


    }
}
