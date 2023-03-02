using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamZoo.Project.WebSite.Controllers.IComparer
{
    using DbContext;
    
    
    
    public class ColumnComparer : IComparer<IGrouping<string, T_ChartData>>
    {
        public string ChartName { get; set; }
        public ColumnComparer(string ChartName)
        {
            this.ChartName = ChartName;
        }

        public int Compare(IGrouping<string, T_ChartData> x, IGrouping<string, T_ChartData> y)
        {
            if (ColumnNameSort.SORT.ContainsKey(this.ChartName + x.Key) && ColumnNameSort.SORT.ContainsKey(this.ChartName + y.Key))
            {
                if (ColumnNameSort.SORT[this.ChartName + x.Key] < ColumnNameSort.SORT[this.ChartName + y.Key])
                    return 1;
                else if (ColumnNameSort.SORT[this.ChartName + x.Key] > ColumnNameSort.SORT[this.ChartName + y.Key])
                    return -1;
                else
                    return 0;

            }

            return 0;
        }
    }
}
