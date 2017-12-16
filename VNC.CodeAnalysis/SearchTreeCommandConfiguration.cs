using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

namespace VNC.CodeAnalysis
{
    public class SearchTreeCommandConfiguration
    {
        public SyntaxTree SyntaxTree;

        public StringBuilder Results;

        public Boolean UseRegEx;
        public string RegEx;

        public ConfigurationOptions ConfigurationOptions;

        public Dictionary<string, Int32> Matches;
        public Dictionary<string, Int32> CRCMatchesToString;
        public Dictionary<string, Int32> CRCMatchesToFullString;
    }
}
