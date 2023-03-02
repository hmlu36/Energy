using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JamZoo.Project.WebSite.Extensions
{
    public static class MyExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = (int)Enum.Parse(enumObj.GetType(), e.ToString()), Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObj);
        }

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj, params TEnum[] exclude)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         where !exclude.Contains(e)
                         select new { Id = (int)Enum.Parse(enumObj.GetType(), e.ToString()), Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObj);
        }
    }
}
