using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.CodeAnalysis;
using VNCCA = VNC.CodeAnalysis;

namespace VNC.CodeAnalysis.SyntaxRewriters.VB
{
    public class WrapSQLFillCallsInDALHelper : VNCVBSyntaxRewriterBase
    {

        public WrapSQLFillCallsInDALHelper(string TargetInvocationExpression)
        {
            TargetPattern = TargetInvocationExpression;
        }

        public override Microsoft.CodeAnalysis.SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var expression = node.Expression;
            InvocationExpressionSyntax newInvocationExpression = node;

            // We are looking for Invocations like this
            //
            // <something>.Fill(<argument list>)
            //
            // e.g.
            //  objDA.Fill(objDS, strTableName)
            // objDataAdaptor.Fill(objDataTable)

            if (_targetPatternRegEx.Match(node.Expression.ToString()).Success)
            {
                var simpleMemberAccessExpression = node.ChildNodes().First();
                var firstIdentifier = simpleMemberAccessExpression.ChildNodes().First();
                var lastIdentifer = simpleMemberAccessExpression.ChildNodes().Last();
                var argumentList = node.ArgumentList.Arguments.ToString();

                List<SyntaxTrivia> newTrivia = new List<SyntaxTrivia>();

                string existingLeadingTrivia = node.GetLeadingTrivia().ToString();
                string existingLeadingTriviaFull = node.GetLeadingTrivia().ToFullString();

                string existingTrailingTrivia = node.GetTrailingTrivia().ToString();
                string existingTrailingTriviaFull = node.GetTrailingTrivia().ToFullString();

                if (VNCCA.Helpers.VB.IsOnLineByItself(node))
                {
                    string startOfLineWhiteSpace = existingLeadingTrivia.Replace(System.Environment.NewLine, "");

                    newTrivia.Add(SyntaxFactory.CommentTrivia(existingLeadingTriviaFull));

                    // TODO(crhodes)
                    // NB. lastIdentifier should always be Fill.  May want to put a check in.

                    var newExpression = SyntaxFactory.ParseExpression(string.Format("DAL.Helpers.{0}({1}{2})",                       
                        lastIdentifer, firstIdentifier,
                        argumentList.Length > 0 ? ", " + argumentList : ""));

                    newInvocationExpression = (InvocationExpressionSyntax)newExpression.WithTriviaFrom(node);

                    RecordReplacementAndContext(node, node.ToString(), newInvocationExpression.ToString());
                }
                else
                {
                    Messages.AppendLine(String.Format("node: >{0}< >{1}< Is NOT OnLineByItself()", node.ToString(), node.ToFullString()));
                    newInvocationExpression = node;
                }
            }
            else
            {
                newInvocationExpression = node;
            }

            return base.VisitInvocationExpression(newInvocationExpression);
        }       
    }
}
