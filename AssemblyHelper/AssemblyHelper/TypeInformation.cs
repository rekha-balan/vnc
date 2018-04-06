using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNC.AssemblyHelper
{
    /// <summary>
    /// Holds information for a .NET Type
    /// </summary>
    /// 
    [Serializable]
    public class TypeInformation
    {
        public string Source { get; set; }
        public string Application { get; set; }
        public string Assembly { get; set; }
        public string FullName { get; set; }
        public string DeclaringType { get; set; }
        public string Name { get; set; }
        public string IsPublic { get; set; }
        public string IsNotPublic { get; set; }
        public string IsValueType { get; set; }
        public string IsPrimitive { get; set; }
        public string IsEnum { get; set; }
        public string IsInterface { get; set; }
        public string IsClass { get; set; }
        public string IsAbstract { get; set; }
        public string IsSealed { get; set; }
        public string IsNested { get; set; }
        public string IsNestedPublic { get; set; }
        public string IsNestedPrivate { get; set; }
        public string HasElementType { get; set; }
        public string IsArray { get; set; }
        public string IsByRef { get; set; }
        public string IsPointer { get; set; }

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
                    "FullName",
                    "DeclaringType",
                    "Name",
                    "IsPublic",
                    "IsNotPublic",
                    "IsValueType",
                    "IsPrimitive",
                    "IsEnum",
                    "IsInterface",
                    "IsClass",
                    "IsAbstract",
                    "IsSealed",
                    "IsNested",
                    "IsNestedPublic",
                    "IsNestedPrivate",
                    "HasElementType",
                    "IsArray",
                    "IsByRef",
                    "IsPointer"
                };

            return headers;
        }
    }
}
