using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Energy.Utils
{
    /// <summary>
    /// Swagger schema filter to modify description of enum types so they
    /// show the XML docs attached to each member of the enum.
    /// </summary>
    public class DescribeEnumMembers : ISchemaFilter
    {
        private readonly XDocument xmlComments;
        private readonly string assemblyName;

        /// <summary>
        /// Initialize schema filter.
        /// </summary>
        /// <param name="xmlComments">Document containing XML docs for enum members.</param>
        public DescribeEnumMembers(XDocument xmlComments)
        {
            this.xmlComments = xmlComments;
            this.assemblyName = DetermineAssembly(xmlComments);
        }

        /// <summary>
        /// Pre-amble to use before the enum items
        /// </summary>
        public static string Prefix { get; set; } = "<p>Possible values:</p>";

        /// <summary>
        /// Format to use, 0 : value, 1: Name, 2: Description
        /// </summary>
        public static string Format { get; set; } = "<b>{0} - {1}</b>: {2}";

        /// <summary>
        /// Apply this schema filter.
        /// </summary>
        /// <param name="schema">Target schema object.</param>
        /// <param name="context">Schema filter context.</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;

            // Only process enums and...
            if (!type.IsEnum)
            {
                return;
            }

            // ...only the comments defined in their origin assembly
            if (type.Assembly.GetName().Name != assemblyName)
            {
                return;
            }
            var sb = new StringBuilder(schema.Description);

            if (!string.IsNullOrEmpty(Prefix))
            {
                sb.AppendLine(Prefix);
            }

            sb.AppendLine("<ul>");

            // TODO: Handle flags better e.g. Hex formatting
            foreach (var name in Enum.GetValues(type))
            {
                // Allows for large enums
                var value = Convert.ToInt64(name);

                // 取得 enum 的 field
                FieldInfo fieldInfo = type.GetField(name.ToString());

                // 取得 field 上的 Description Attribute
                DescriptionAttribute descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

                // 取得 Description 屬性的值
                string description = descriptionAttribute?.Description;

                sb.AppendLine(string.Format("<li>" + Format + "</li>", value, name, description));
            }

            sb.AppendLine("</ul>");

            schema.Description = sb.ToString();
        }

        private string DetermineAssembly(XDocument doc)
        {
            var name = ((IEnumerable)doc.XPathEvaluate("/doc/assembly")).Cast<XElement>().ToList().FirstOrDefault();

            return name?.Value;
        }
    }
}
