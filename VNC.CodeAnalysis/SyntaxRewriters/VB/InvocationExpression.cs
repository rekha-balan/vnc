using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace VNC.CodeAnalysis.SyntaxRewriters.VB
{
    public class InvocationExpression : VNCVBSyntaxRewriterBase
    {
        public StringBuilder Messages;
        public string _targetInvocationExpression = null;
        public string _newInvocationExpression = null;
        public Boolean PerformedReplacement = false;

        //public InvocationExpression(bool visitIntoStructuredTrivia = false) : base(visitIntoStructuredTrivia)
        //{

        //}

        public InvocationExpression(string TargetInvocationExpression, string NewInvocationExpression)
        {
            _targetInvocationExpression = TargetInvocationExpression;
            _newInvocationExpression = NewInvocationExpression;
        }

        public override Microsoft.CodeAnalysis.SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (_targetInvocationExpression == null || _newInvocationExpression == null)
            {
                return node;
            }

            //Microsoft.CodeAnalysis.SyntaxNode newInvocation = null;
            var expression = node.Expression;
            InvocationExpressionSyntax newInvocationExpression;

            var newExpression = SyntaxFactory.IdentifierName(_newInvocationExpression);

            if (expression.ToString() == _targetInvocationExpression)
            {
                newInvocationExpression = node.WithExpression(newExpression);
                Messages.AppendLine(string.Format("From: >{0}< To: >{1}<", expression.ToString(), newInvocationExpression.ToString()));
                PerformedReplacement = true;
            }
            else
            {
                newInvocationExpression = node;
            }

            //return newInvocationExpression;
            return base.VisitInvocationExpression(newInvocationExpression);
        }
    }
}
