﻿using System;
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
    public class AddImportsStatement : VNCVBSyntaxRewriterBase
    {
        string importsName = "";

        public AddImportsStatement(string name)
        {
            importsName = name;
        }

        public override Microsoft.CodeAnalysis.SyntaxNode VisitImportsStatement(ImportsStatementSyntax node)
        {
            var newImportsStatement = SyntaxFactory.ImportsStatement();

            newImportsStatement = newImportsStatement.AddImportsClauses(SyntaxFactory.SimpleImportsClause(SyntaxFactory.ParseName(importsName)));

            CompilationUnitSyntax cu = (CompilationUnitSyntax)node.Parent;
            cu = cu.AddImports(newImportsStatement);

            ImportsStatementSyntax newNode = null;

            if (_targetPatternRegEx.Match(node.ImportsClauses.ToString()).Success)
            {
                var existingImports = node.Parent.DescendantNodes().OfType<ImportsStatementSyntax>();

                //newNode = node.AddImportsClauses(new)
                //RecordMatchAndContext(node, BlockType.None);
            }

            return newNode;
        }

        //public override Microsoft.CodeAnalysis.SyntaxNode VisitMethodBlock(MethodBlockSyntax node)
        //{
        //    MethodBlockSyntax newNode = null;
        //    EmptyStatementSyntax emptyNode = SyntaxFactory.EmptyStatement();

        //    if (_targetPatternRegEx.Match(node.SubOrFunctionStatement.Identifier.ToString()).Success)
        //    {
        //        try
        //        {
        //            if (_commentOutOnly)
        //            {
        //                List<SyntaxTrivia> newTrivia = new List<SyntaxTrivia>();
        //                string existingLeadingTrivia = node.GetLeadingTrivia().ToString();
        //                string existingLeadingTriviaFull = node.GetLeadingTrivia().ToFullString();

        //                string existingTrailingTrivia = node.GetTrailingTrivia().ToString();
        //                string existingTrailingTriviaFull = node.GetTrailingTrivia().ToFullString();

        //                string startOfLineWhiteSpace = existingLeadingTrivia.Replace(System.Environment.NewLine, "");

        //                newTrivia.Add(SyntaxFactory.CommentTrivia(existingLeadingTriviaFull));
        //                newTrivia.Add(SyntaxFactory.CommentTrivia(VNCCA.Helpers.VB.MultiLineComment(_comment, startOfLineWhiteSpace)));
        //                newTrivia.Add(SyntaxFactory.CommentTrivia("' "));

        //                // Comment out the whole method.

        //                newTrivia.Add(SyntaxFactory.CommentTrivia(VNCCA.Helpers.VB.BlockComment(node.ToFullString())));

        //                return emptyNode.WithLeadingTrivia(newTrivia);

        //                // We don't need the existing node as it has been completely commented out.
        //                //newNode = node.WithLeadingTrivia(newTrivia);
        //            }
        //            else
        //            {
        //                // This removes the node

        //                newNode = null;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Messages.AppendLine(string.Format("Ex:{0} InnerEx:{1}", ex.ToString(), ex.InnerException.ToString()));
        //        }
        //    }
        //    else
        //    {
        //        newNode = node;
        //    }

        //    return newNode;
        //}
    }
}
