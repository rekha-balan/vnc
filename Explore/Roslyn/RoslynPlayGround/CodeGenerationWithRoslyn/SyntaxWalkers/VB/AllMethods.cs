﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;

namespace CodeGenerationWithRoslyn.SyntaxWalkers.VB
{
    class AllMethods : VisualBasicSyntaxWalker
    {
        public AllMethods() : base(SyntaxWalkerDepth.StructuredTrivia)
        {
            
        }

        static int tabs = 0;

        public override void Visit(SyntaxNode node)
        {
            tabs++;
            var indents = new String(' ', tabs * 3);
            Console.WriteLine(indents + node.Kind());
            base.Visit(node);
            tabs--;
        }

        public override void VisitToken(SyntaxToken token)
        {
            var indents = new String(' ', tabs * 3);
            Console.WriteLine(string.Format("{0}{1}:\t{2}", indents, token.Kind(), token));
            base.VisitToken(token);
        }

        
    }
}
