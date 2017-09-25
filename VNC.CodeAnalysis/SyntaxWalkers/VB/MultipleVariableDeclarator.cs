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
    public class MultipleVariableDeclarator : VNCVBTypedSyntaxWalkerBase
    {

        public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            
            if (node.Declarators.Count() > 1)
            {
                Messages.AppendLine(String.Format("{0} {1}",
                    GetNodeContext(node),
                    node.ToString()));
            }

            base.VisitLocalDeclarationStatement(node);
        }
    }
}
