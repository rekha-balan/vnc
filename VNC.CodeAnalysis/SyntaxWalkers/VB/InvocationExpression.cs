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
        public StringBuilder StringBuilder;
        static int tabs = 0;
        private string _pattern;

        public InvocationExpression(string pattern) : base(SyntaxWalkerDepth.StructuredTrivia)
        {
            _pattern = pattern;
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            //tabs++;
            //var indents = new String(' ', tabs * 3);

            if (node.Expression.ToString() == _pattern)
            {
                StringBuilder.AppendLine(node.ToString());
            }

            base.VisitInvocationExpression(node);
            //tabs--;
        }
    }
}
