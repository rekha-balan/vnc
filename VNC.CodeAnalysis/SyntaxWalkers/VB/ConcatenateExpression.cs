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
    public class BinaryExpression : VNCVBSyntaxWalkerBase
    {
        public override void VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            var t = node.GetType().ToString();
            var cn = node.ChildNodes();
            var faos = node.FirstAncestorOrSelf<AssignmentStatementSyntax>();
            var p = node.Parent.ToString();
            var pt = node.Parent.GetType().ToString();

            if (node.Parent.GetType() == typeof(AssignmentStatementSyntax))
            {
                if (identifierNameRegEx.Match(faos.ToString()).Success)
                {
                    Messages.AppendLine(String.Format("{0} {1}",
                        GetNodeContext(faos),
                        faos.ToString()));
                }
            }
            if (faos != null)
            {
                //if (identifierNameRegEx.Match(faos.ToString()).Success)
                //{
                //    Messages.AppendLine(String.Format("{0} {1}",
                //        GetNodeContext(faos),
                //        faos.ToString()));
                //}
            }
            else
            {
                if (identifierNameRegEx.Match(node.ToString()).Success)
                {
                    Messages.AppendLine(String.Format("{0} {1}",
                        GetNodeContext(node),
                        node.ToString()));
                }
            }

            //if (identifierNameRegEx.Match(node.ToString()).Success)


            base.VisitBinaryExpression(node);
        }
    }
}
