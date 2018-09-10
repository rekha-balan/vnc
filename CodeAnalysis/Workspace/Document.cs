using System.Reflection;
using System.Text;

namespace VNC.CodeAnalysis.Workspace
{
    public class Document
    {
        public static StringBuilder Display(string documentFullPath)
        {
            StringBuilder sb = new StringBuilder();

            Microsoft.CodeAnalysis.Document document = null;
            return Display(document);
        }

        static StringBuilder Display(Microsoft.CodeAnalysis.Document document)
        {
            StringBuilder sb = new StringBuilder();

                        sb.AppendLine(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name + " Not Implemented Yet");

            return sb;

        }
    }
}
