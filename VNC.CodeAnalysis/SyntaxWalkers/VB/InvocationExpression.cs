using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace VNC.CodeAnalysis.SyntaxWalkers.VB
{
    public class InvocationExpression : VisualBasicSyntaxWalker
    {
        public StringBuilder Messages;

        private string _pattern;

        public InvocationExpression(string pattern) : base(SyntaxWalkerDepth.StructuredTrivia)
        {
            _pattern = pattern;
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (node.Expression.ToString() == _pattern)
            {
                Messages.AppendLine(node.ToString());
            }

            // Call base to visit children

            base.VisitInvocationExpression(node);
        }
    }
}
