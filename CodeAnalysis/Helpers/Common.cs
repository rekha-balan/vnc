using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VNC.CodeAnalysis.Helpers
{
    public class Common
    {
        public static Regex InitializeRegEx(string pattern, StringBuilder messages, RegexOptions options = RegexOptions.None)
        {
            Regex expression;

            try
            {
                expression = new Regex(pattern, options);
            }
            catch (Exception ex)
            {
                messages.AppendLine(string.Format("Error in RegEx >{0}< Error:({1}), using >.*<",
                    pattern, ex.Message));

                expression = new Regex(".*", RegexOptions.IgnoreCase);
            }

            return expression;
        }
    }
}
