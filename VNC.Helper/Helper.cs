using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNC
{
    public static class Helper
    {
        /// <summary>
        /// Surrounds string in double quotes "<string>"</string>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WrapInDblQuotes(this string text)
        {
            return WrapIn(text, "\"");
        }

        /// <summary>
        /// Surrounds string in ends <ends><string></ends>"</string>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ends"></param>
        /// <returns></returns>
        public static string WrapIn(this string text, string ends)
        {
            return ends + text + ends;
        }
    }
}
