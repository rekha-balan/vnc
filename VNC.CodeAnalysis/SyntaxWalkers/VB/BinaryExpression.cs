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
                if (_targetPatternRegEx.Match(faos.ToString()).Success)
                {
                    RecordMatchAndContext(faos, BlockType.None);
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
                if (_targetPatternRegEx.Match(node.ToString()).Success)
                {
                    RecordMatchAndContext(node, BlockType.None);
                }
            }

            base.VisitBinaryExpression(node);
        }
    }
}
