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
    public class NamespaceStatement : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitNamespaceStatement(NamespaceStatementSyntax node)
        {
            if (_targetPatternRegEx.Match(node.Name.ToString()).Success)
            {
                RecordMatch(node, BlockType.None);
            }

            base.VisitNamespaceStatement(node);
        }
    }
}
