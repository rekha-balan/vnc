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
    public class MethodStatement : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitMethodStatement(MethodStatementSyntax node)
        {
            if (identifierNameRegEx.Match(node.Identifier.ToString()).Success)
            {
                Messages.AppendLine(String.Format("{0} {1}",
                    GetNodeContext(node),
                    node.ToString()));
            }

            base.VisitMethodStatement(node);
        }
    }
}
