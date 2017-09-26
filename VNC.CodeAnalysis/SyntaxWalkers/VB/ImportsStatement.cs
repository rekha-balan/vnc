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
            if (identifierNameRegEx.Match(node.ImportsClauses.ToString()).Success)
            {
                Messages.AppendLine(String.Format("{0} {1}",
                    GetNodeContext(node),
                    node.ToString()));
            }

            base.VisitImportsStatement(node);
        }
    }
}
