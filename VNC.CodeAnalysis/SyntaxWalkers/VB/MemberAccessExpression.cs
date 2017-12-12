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
    public class MemberAccessExpression : VNCVBSyntaxWalkerBase
    {
        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            if (identifierNameRegEx.Match(node.ToString()).Success)
            {
                RecordMatchAndContext(node, BlockType.None);
            }

            base.VisitMemberAccessExpression(node);
        }
    }
}
