using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNC.AssemblyHelper
{
    [Serializable]
    public class MethodInformation
    {
        public string Source { get; set; }
        public string Application { get; set; }
        public string Assembly { get; set; }
        public string Type { get; set; }
        public string IsStatic { get; set; }
        public string IsPublic { get; set; }
        public string IsPrivate { get; set; }
        public string ReturnType { get; set; }
        public string Method { get; set; }
        public string Parameters { get; set; }
        public string RetValParameters { get; set; }
        public string OutParameters { get; set; }
        public string OptionalParameters { get; set; }
        public string ByRefParameters { get; set; }
        public string MD5 { get; set; }

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
                    "Type",
                    "IsStatic",
                    "IsPublic",
                    "IsPrivate",
                    "ReturnType",
                    "Method",
                    "Parameters",
                    "RetVal Parameters",
                    "Out Parameters",
                    "Optional Parameters",
                    "ByRef Parameters",
                    "MD5"
                };

            return headers;         
        }
    }
}
