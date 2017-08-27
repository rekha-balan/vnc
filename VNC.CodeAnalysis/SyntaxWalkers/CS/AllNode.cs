﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace VNC.CodeAnalysis.SyntaxWalkers.CS
{
    public class AllNode : CSharpSyntaxWalker
    {
        public StringBuilder StringBuilder;

        public AllNode() : base(SyntaxWalkerDepth.Node)
        {
            
        }

        static int tabs = 0;

        public override void Visit(Microsoft.CodeAnalysis.SyntaxNode node)
        {
            tabs++;
            var indents = new String(' ', tabs * 3);
            StringBuilder.AppendLine(indents + node.Kind());

            // Call base to visit children

            base.Visit(node);
            tabs--;
        }

        public override void VisitToken(SyntaxToken token)
        {
            var indents = new String(' ', tabs * 3);
            StringBuilder.AppendLine(string.Format("{0}{1}:>{2}<", indents, token.Kind(), token));

            // Call base to visit children

            base.VisitToken(token);
        }

        public override void VisitTrivia(SyntaxTrivia trivia)
        {
            var indents = new String(' ', tabs * 3);
            StringBuilder.AppendLine(string.Format("{0}{1}:>{2}<", indents, trivia.Kind(), trivia));

            // Call base to visit children

            base.VisitTrivia(trivia);
        }
    }
}
