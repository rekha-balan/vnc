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
    public class InvocationExpression : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (identifierNameRegEx.Match(node.Expression.ToString()).Success)
            {
                RecordMatchAndContext(node, node.ToString());
                //Messages.AppendLine(String.Format("{0} {1}",
                //    GetNodeContext(node),
                //    node.ToString()));
            }

            // Call base to visit children

            base.VisitInvocationExpression(node);
        }
    }
}
