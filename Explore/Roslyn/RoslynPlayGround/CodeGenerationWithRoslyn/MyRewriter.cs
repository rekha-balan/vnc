using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGenerationWithRoslyn
{
    public class MyRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitIfStatement(IfStatementSyntax node)
        {
            // remove node entirely
            // return null;

            // return what we got
            // return node;

            // Return entirely new syntax tree

            // Call base to visit more deeply into node.
            //return base.VisitIfStatement(node);

            // Wrap body with a block

            var body = node.Statement;
            var block = SyntaxFactory.Block(body);

            var newIfStatement = node.WithStatement(block);

            // Can use the formatter class to clean up layout.

            return newIfStatement;
        }
    }
}
