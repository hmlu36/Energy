using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JamZoo.Project.WebSite.Controllers.DatabaseRow
{
    public class Base
    {
        public void AddRow<T>(DataRow _row, T m, string ColumnName, string 欄位名)
        {
            
            if (_row[ColumnName] == DBNull.Value)
            {
                _row[ColumnName] = -1;
            }
            else if (m == null)
            {
                _row[ColumnName] = -1;
            }

            if (m != null)
            {
                var _prop = m.GetType().GetProperty(欄位名);
                if (_prop != null)
                {
                    var _value = _prop.GetValue(m);
                    if (_value != null)
                    {
                        Double _temp = 0;
                        Double.TryParse(_row[ColumnName].ToString(), out _temp);
                        if (_temp == -1)
                        {
                            _temp = 0;
                        }
                        _row[ColumnName] = _temp + Double.Parse(_value.ToString());
                        
                    }
                }
            }
        }

    }
}
