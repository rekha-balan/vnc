using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace VNC.CodeAnalysis.SyntaxRewriters.VB
{
    public class ReplaceConvertToInt16 : VisualBasicSyntaxRewriter
    {
        public override Microsoft.CodeAnalysis.SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            Microsoft.CodeAnalysis.SyntaxNode newInvocation = null;
            var expression = node.Expression;
            InvocationExpressionSyntax newInvocationExpression;

            var dojie = SyntaxFactory.IdentifierName("Doje");

            if (expression.ToString() == "ConvertToInt16" || expression.ToString() == "Ec.GeneralFunc.ConvertToInt16")
            {
                newInvocationExpression = node.WithExpression(dojie);
            }
            else
            {
                newInvocationExpression = node;
            }

            return newInvocationExpression;
        }
    }
}
