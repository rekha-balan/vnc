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
    public class SimpleAsClause : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitSimpleAsClause(SimpleAsClauseSyntax node)
        {
            var n = node;

            var s = node.ToString();

            if (_targetPatternRegEx.Match(node.ToString()).Success)
            {
                RecordMatchAndContext(node, BlockType.None);
            }

            base.VisitSimpleAsClause(node);
        }
    }
}
