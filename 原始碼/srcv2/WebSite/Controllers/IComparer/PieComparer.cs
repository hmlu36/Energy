using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamZoo.Project.WebSite.Controllers.IComparer
{
    using DbContext;
    
    
    
    public class PieComparer : IComparer<T_ChartData>
    {
        public int Compare(T_ChartData t1, T_ChartData t2)
        {
            string x = t1.Chart + t1.ColumnName;
            string y = t2.Chart + t2.ColumnName;
            if (ColumnNameSort.SORT.ContainsKey(x) && ColumnNameSort.SORT.ContainsKey(y))
            {
                if (ColumnNameSort.SORT[x] < ColumnNameSort.SORT[y])
                    return 1;
                else if (ColumnNameSort.SORT[x] > ColumnNameSort.SORT[y])
                    return -1;
                else
                    return 0;

            }

            return 0;
        }
    }
}
