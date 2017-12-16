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
    public class RemoveFieldDeclaration : VNCVBSyntaxRewriterBase
    {

        public RemoveFieldDeclaration(string targetInvocationExpression, SyntaxNode.FieldDeclarationLocation declarationLocation, 
            Boolean commentOutOnly, string comment)
        {
            TargetPattern = targetInvocationExpression;
            _declarationLocation = declarationLocation;
            _comment = comment;
            _commentOutOnly = commentOutOnly;
        }

        public override Microsoft.CodeAnalysis.SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            FieldDeclarationSyntax newNode = null;
            var parent = node.Parent;

            // Verify we have the correct context for the Field Declaration

            switch (_declarationLocation)
            {
                case SyntaxNode.FieldDeclarationLocation.Class:
                    if (parent.Kind() != SyntaxKind.ClassBlock)
                    {
                        return node;
                    }
                    break;

                case SyntaxNode.FieldDeclarationLocation.Module:
                    if (parent.Kind() != SyntaxKind.ModuleBlock)
                    {
                        return node;
                    }
                    break;

                case SyntaxNode.FieldDeclarationLocation.Structure:
                    if (parent.Kind() != SyntaxKind.StructureBlock)
                    {
                        return node;
                    }
                    break;
            }

            if (_targetPatternRegEx.Match(node.Declarators.First().Names.First().Identifier.ToString()).Success)
            {
                try
                {
                    // Verify this expression is on line by itself.  May not need to do this.
                    // See if can have multiple FieldDeclarations on same line.

                    if (VNCCA.Helpers.VB.IsOnLineByItself(node))
                    {
                        // HACK(crhodes)
                        // Figure out how to get Helpers to work here.
                        Messages.AppendLine(String.Format("Removing {0} {1}",
                            VNCCA.Helpers.VB.GetContainingContext(node, _configurationOptions),
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
