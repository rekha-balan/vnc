using System;

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;

using Microsoft.CodeAnalysis.VisualBasic;

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

            sb.AppendLine("Not Implemented Yet");

            return sb;

        }
    }
}
