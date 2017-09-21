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
    public class VariableDeclarator : VisualBasicSyntaxWalker
    {
        public StringBuilder Messages;

        private string _pattern;

        public VariableDeclarator(string pattern) : base(SyntaxWalkerDepth.StructuredTrivia)
        {
            _pattern = pattern;
        }

        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            var nodeInitializer = node.Initializer;
            var nodeAsClause = node.AsClause;
            var nodeNames = node.Names;
            var asType = node.AsClause.Type().ToString();
            

            //if (node.Expression.ToString() == _pattern)
            //{
            //    Messages.AppendLine(node.ToString());
            //}

            base.VisitVariableDeclarator(node);
        }
    }
}
