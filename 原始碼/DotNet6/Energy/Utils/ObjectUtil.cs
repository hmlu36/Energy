using System.Reflection;
using System.Text.RegularExpressions;

namespace Energy.Utils
{
    public class ObjectUtil
    {
        // 取得class所有欄位
        public static IEnumerable<string> GetClassMemberNames(string className)
        {
            Type type = Type.GetType(className);
            if (type == null)
            {
                throw new ArgumentException($"Class '{className}' not found");
            }

            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();

            string[] fieldNames = fields.Select(f => f.Name).ToArray();
            string[] propertyNames = properties.Select(p => p.Name).ToArray();

            return fieldNames.Concat(propertyNames);
        }

        // 轉成db欄位
        // ex: YrMnth => yr_mnth
        //     RowNo1 => row_no1
        public static string[] GetClassDbMemeberNames(string className)
        {
            return GetClassMemberNames(className).Select(s => Regex.Replace(s, @"([a-z])([A-Z])", "$1_$2").ToLower()).ToArray();
        }
    }
}
