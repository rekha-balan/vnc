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
    public class ObjectCreationExpression : VNCVBSyntaxWalkerBase
    {
        //public Boolean MatchLeft = true;

        //public Boolean MatchRight = true;

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            var n = node;

            if (identifierNameRegEx.Match(node.ToString()).Success)
            {
                RecordMatchAndContext(node, node.ToString());
            }

            //if (MatchLeft)
            //{
            //    if (identifierNameRegEx.Match(node.Left.ToString()).Success)
            //    {
            //        Messages.AppendLine(String.Format("{0} {1}",
            //            GetNodeContext(node),
            //            node.ToString()));
            //    }
            //}

            //if (MatchRight)
            //{
            //    if (identifierNameRegEx.Match(node.Right.ToString()).Success)
            //    {
            //        Messages.AppendLine(String.Format("{0} {1}",
            //            GetNodeContext(node),
            //            node.ToString()));
            //    }                
            //}

            base.VisitObjectCreationExpression(node);
        }
    }
}
