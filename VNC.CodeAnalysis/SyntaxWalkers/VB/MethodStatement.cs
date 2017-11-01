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
        public Boolean ShowParameters = false;

        public override void VisitMethodStatement(MethodStatementSyntax node)
        {
            if (identifierNameRegEx.Match(node.Identifier.ToString()).Success)
            {
                string message = node.Identifier.ToString();

                if (ShowParameters)
                {
                    message = node.ToString();
                }

                Messages.AppendLine(String.Format("{0} {1}",
                    GetNodeContext(node),
                    message));

                //string nodeValue = message;

                RecordMatch(message);
            }

            base.VisitMethodStatement(node);
        }
    }
}
