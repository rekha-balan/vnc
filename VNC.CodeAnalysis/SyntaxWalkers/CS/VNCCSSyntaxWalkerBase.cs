using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace VNC.CodeAnalysis.SyntaxWalkers.CS
{
    public class VNCCSSyntaxWalkerBase : CSharpSyntaxWalker
    {
        public StringBuilder Messages;
        static int tabs = 0;

        public VNCCSSyntaxWalkerBase(SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node) : base(depth)
        {
        }

        public override void Visit(Microsoft.CodeAnalysis.SyntaxNode node)
        {
            tabs++;
            var indents = new String(' ', tabs * 3);
            Messages.AppendLine(string.Format("{0}{1,6}{2}:>{3}<", indents, "Node:", node.Kind(), node.ToString()));

            // Call base to visit children

            base.Visit(node);
            tabs--;
        }

        public override void VisitToken(Microsoft.CodeAnalysis.SyntaxToken token)
        {
            var indents = new String(' ', tabs * 3);
            Messages.AppendLine(string.Format("{0}{1,6}{2}:>{3}<", indents, "Token:", token.Kind(), token));

            // Call base to visit children

            base.VisitToken(token);
        }

        public override void VisitTrivia(Microsoft.CodeAnalysis.SyntaxTrivia trivia)
        {
            var indents = new String(' ', tabs * 3);
            Messages.AppendLine(string.Format("{0}{1,6}{2}:>{3}<", indents, "Trivia:", trivia.Kind(), trivia));

            // Call base to visit children

            base.VisitTrivia(trivia);
        }
    }
}
