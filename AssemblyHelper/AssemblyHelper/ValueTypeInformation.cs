using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNC.AssemblyHelper
{
    [Serializable]
    public class ValueTypeInformation
    {
        public string Source { get; set; }
        public string Applications { get; set; }
        public string Assembly { get; set; }
        public string DeclaringType { get; set; }
        public string Type { get; set; }
        public string Field { get; set; }
        public string FieldType { get; set; }
        public string IsArray { get; set; }
        public string IsEnum { get; set; }
        public string IsValueType { get; set; }
        public string Attribute { get; set; }
        public string AttributeValue { get; set; }
        public string AttributeToString { get; set; }

        public static string[] GetHeadersSourceContext()
        {
            string[] headers =
                {
                    "Source",
                    "Application"
                };

            return headers.Concat(GetHeaders()).ToArray();
        }

        public static string[] GetHeaders()
        {
            string[] headers =
                {
                    "Assembly",
                    "DeclaringType",
                    "Type",
                    "Field",
                    "FieldType",
                    "IsArray",
                    "IsEnum",
                    "IsValueType",
                    "Attribute",
                    "AttributeValue",
                    "AttributeToString"
                };

            return headers;
        }
    }
}
