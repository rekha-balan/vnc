using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace VNC.CodeAnalysis.SyntaxWalkers.VB
{
    public class StopOrEndStatement : VNCVBSyntaxWalkerBase
    {
        public override void VisitStopOrEndStatement(StopOrEndStatementSyntax node)
        {
            //if (_targetPatternRegEx.Match(node.Identifier.ToString()).Success)
            //{
                RecordMatchAndContext(node, BlockType.None);
            //}

            base.VisitStopOrEndStatement(node);
        }
    }
}
