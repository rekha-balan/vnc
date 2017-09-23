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
            if (identifierNameRegEx.Match(node.Name.ToString()).Success)
            {
                Messages.AppendLine(String.Format("{0}",
                    node.ToString()));
            }

            base.VisitNamespaceStatement(node);
        }
    }
}
