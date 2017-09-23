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
    public class ArgumentList : VNCVBSyntaxWalkerBase
    {
        public override void VisitArgumentList(ArgumentListSyntax node)
        {
            
            //if (identifierNameRegEx.Match(node.Identifier.ToString()).Success)
            if (identifierNameRegEx.Match(node.ToString()).Success)
            {
                string messageContext = "";

                if (DisplayClassOrModuleName)
                {
                    messageContext = Helpers.VB.GetContainingType(node);
                }

                if (DisplayMethodName)
                {
                    messageContext += string.Format(" Method:({0, -35})", Helpers.VB.GetContainingMethod(node));
                }

                Messages.AppendLine(String.Format("{0} {1}",
                    messageContext,
                    node.ToString()));

                //Messages.AppendLine(String.Format("{0}", node.ToString()));
            }

            base.VisitArgumentList(node);
        }
    }
}
