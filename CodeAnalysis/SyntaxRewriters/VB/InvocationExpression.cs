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
        public string _newInvocationExpression = null;

        public InvocationExpression(string TargetInvocationExpression, string NewInvocationExpression)
        {
            TargetPattern = TargetInvocationExpression;
            _newInvocationExpression = NewInvocationExpression;
        }

        public override Microsoft.CodeAnalysis.SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (TargetPattern == null || _newInvocationExpression == null)
            {
                return node;
            }

            var expression = node.Expression;
            InvocationExpressionSyntax newInvocationExpression;

            var newExpression = SyntaxFactory.IdentifierName(_newInvocationExpression);

            // We could relax this to use regular expressions but that seems like more risk.

            //if (identifierNameRegEx.Match(node.Expression.ToString()).Success)

            if (expression.ToString() == TargetPattern)
            {
                newInvocationExpression = node.WithExpression(newExpression);

                //Messages.AppendLine(string.Format("From: >{0}< To: >{1}<", expression.ToString(), newInvocationExpression.ToString()));
                RecordReplacementAndContext(node, expression.ToString(), newInvocationExpression.ToString());
                PerformedReplacement = true;
            }
            else
            {
                newInvocationExpression = node;
            }

            // Call base to replace children

            return base.VisitInvocationExpression(newInvocationExpression);
        }
    }
}
