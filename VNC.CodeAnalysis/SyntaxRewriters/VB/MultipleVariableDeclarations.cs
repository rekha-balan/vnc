using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace VNC.CodeAnalysis.SyntaxRewriters.VB
{
    public class MultipleVariableDeclarations : VNCVBSyntaxRewriterBase
    {
        //public StringBuilder Messages;

        //private string _pattern;
        //private string _comment;

        public MultipleVariableDeclarations(string comment="Rewritten by a button")
        {
            _comment = comment;
        }

        public override Microsoft.CodeAnalysis.SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            LocalDeclarationStatementSyntax newSyntaxNode = null;

            if (node.Declarators.Count() > 1)
            {
                List<LocalDeclarationStatementSyntax> newDeclarations = new List<LocalDeclarationStatementSyntax>();

                foreach (var vds in node.Declarators)
                {

                    //LocalDeclarationStatementSyntax newLDS = new LocalDeclarationStatementSyntax(vds);

                }
            }
            else
            {
                newSyntaxNode = node;
            }

            //Microsoft.CodeAnalysis.SyntaxNode newInvocation = null;
            //var expression = node.Expression;
            //InvocationExpressionSyntax newInvocationExpression = null;

            //if (expression.ToString() == _pattern)
            //{
            //    List<SyntaxTrivia> newTrivia = new List<SyntaxTrivia>();
            //    string existingLeadingTrivia = node.GetLeadingTrivia().ToString();
            //    string existingLeadingTriviaFull = node.GetLeadingTrivia().ToFullString();

            //    string existingTrailingTrivia = node.GetTrailingTrivia().ToString();
            //    string existingTrailingTriviaFull = node.GetTrailingTrivia().ToFullString();

            //    // Verify this expression is on line by itself

            //    if (Helpers.VB.IsOnLineByItself(node))
            //    {
            //        // HACK(crhodes)
            //        // Figure out how to get Helpers to work here.
            //        Messages.AppendLine(String.Format("Commenting out {0} Method:({1,-35}) {2}",
            //            Helpers.VB.GetContainingType(node),
            //            Helpers.VB.GetContainingMethod(node),
            //            node.ToString()));

            //        string startOfLineWhiteSpace = existingLeadingTrivia.Replace(System.Environment.NewLine, "");

            //        newTrivia.Add(SyntaxFactory.CommentTrivia(existingLeadingTriviaFull));
            //        newTrivia.Add(SyntaxFactory.CommentTrivia(Helpers.VB.MultiLineComment(_comment, startOfLineWhiteSpace)));
            //        newTrivia.Add(SyntaxFactory.CommentTrivia("' "));

            //        newInvocationExpression = node.WithLeadingTrivia(newTrivia);
            //    }
            //    else
            //    {
            //        Messages.AppendLine(String.Format("node: >{0}< Is NOT OnLineByItself", node.ToString()));
            //    }
            //}
            //else
            //{
            //    newInvocationExpression = node;
            //}

            return newSyntaxNode;
        }
    }
}
