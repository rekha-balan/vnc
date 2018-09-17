using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using VNCCA = VNC.CodeAnalysis;

namespace VNC.CodeAnalysis.SyntaxRewriters.VB
{
    public class RemoveExpressionStatement : VNCVBSyntaxRewriterBase
    {

        public RemoveExpressionStatement(string targetInvocationExpression, 
            Boolean commentOutOnly, string comment)
        {
            TargetPattern = targetInvocationExpression;
            _comment = comment;
            _commentOutOnly = commentOutOnly;
        }

        public override Microsoft.CodeAnalysis.SyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            var expression = node.Expression;
            ExpressionStatementSyntax newNode = null;

            if (_targetPatternRegEx.Match(node.Expression.ToString()).Success)
            {
                try
                {
                    // Verify this expression is on line by itself.  May not need to do this.
                    // See if can have multiple ExpressionStatements on same line.

                    if (VNCCA.Helpers.VB.IsOnLineByItself(node))
                    {
                        // HACK(crhodes)
                        // Figure out how to get Helpers to work here.
                        Messages.AppendLine(String.Format("Removing {0} Method:({1,-35}) {2}",
                            VNCCA.Helpers.VB.GetContainingContext(node, _configurationOptions),
                            VNCCA.Helpers.VB.GetContainingMethodName(node),
                            node.ToString()));

                        if (_commentOutOnly)
                        {
                            List<SyntaxTrivia> newTrivia = new List<SyntaxTrivia>();
                            string existingLeadingTrivia = node.GetLeadingTrivia().ToString();
                            string existingLeadingTriviaFull = node.GetLeadingTrivia().ToFullString();

                            string existingTrailingTrivia = node.GetTrailingTrivia().ToString();
                            string existingTrailingTriviaFull = node.GetTrailingTrivia().ToFullString();

                            string startOfLineWhiteSpace = existingLeadingTrivia.Replace(System.Environment.NewLine, "");

                            newTrivia.Add(SyntaxFactory.CommentTrivia(existingLeadingTriviaFull));
                            newTrivia.Add(SyntaxFactory.CommentTrivia(VNCCA.Helpers.VB.MultiLineComment(_comment, startOfLineWhiteSpace)));
                            newTrivia.Add(SyntaxFactory.CommentTrivia("' "));

                            newNode = node.WithLeadingTrivia(newTrivia);
                        }
                        else
                        {
                            // This removes the node

                            newNode = null;
                        }
                    }
                    else
                    {
                        Messages.AppendLine(String.Format("node: >{0}< >{1}< Is NOT OnLineByItself", node.ToString(), node.ToFullString()));
                        newNode = node;
                    }
                }
                catch (Exception ex)
                {
                    Messages.AppendLine(string.Format("Ex:{0} InnerEx:{1}", ex.ToString(), ex.InnerException.ToString()));
                }
            }
            else
            {
                newNode = node;
            }

            return newNode;
        }
    }
}
