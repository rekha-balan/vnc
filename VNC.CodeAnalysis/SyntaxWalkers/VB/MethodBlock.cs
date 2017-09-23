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
    public class MethodBlock : VNCVBSyntaxWalkerBase
    {
        public override void VisitMethodBlock(MethodBlockSyntax node)
        {
            if (identifierNameRegEx.Match(node.SubOrFunctionStatement.Identifier.ToString()).Success)
            {
                string messageContext = "";

                if (DisplayClassOrModuleName)
                {
                    messageContext = Helpers.VB.GetContainingType(node);
                }

                Messages.AppendLine(String.Format("{0} {1}",
                    messageContext,
                    node.ToString()));
            }

            base.VisitMethodBlock(node);
        }
    }
}
