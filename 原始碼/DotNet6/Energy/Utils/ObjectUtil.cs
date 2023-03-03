using System.Reflection;

namespace Energy.Utils
{
    public class ObjectUtil
    {
        public static object CreateInstance(string typeName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetType(typeName);

            return Activator.CreateInstance(type);
        }

    }
}
