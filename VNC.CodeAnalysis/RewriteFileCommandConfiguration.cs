using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

namespace VNC.CodeAnalysis
{
    public class RewriteFileCommandConfiguration
    {
        public SyntaxTree SyntaxTree;
        public string FilePath;

        public StringBuilder Results;

        public Boolean UseRegEx;
        public string RegEx;

        public string TargetPattern;
        public string ReplacementPattern;
        public string Comment;

        public ConfigurationOptions ConfigurationOptions;

        public Dictionary<string, Int32> Replacements;
        public Dictionary<string, Int32> CRCMatchesToString;
        public Dictionary<string, Int32> CRCMatchesToFullString;
    }
}
