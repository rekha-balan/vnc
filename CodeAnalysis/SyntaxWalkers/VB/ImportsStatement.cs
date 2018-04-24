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
    public class ImportsStatement : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitImportsStatement(ImportsStatementSyntax node)
        {
            if (_targetPatternRegEx.Match(node.ImportsClauses.ToString()).Success)
            {
                //var imports = node.Parent.DescendantNodes().OfType<ImportsStatementSyntax>();

                RecordMatchAndContext(node, BlockType.None);
            }

            base.VisitImportsStatement(node);
        }
    }
}
