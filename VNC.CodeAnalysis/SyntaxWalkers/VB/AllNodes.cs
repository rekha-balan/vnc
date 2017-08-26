using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;

namespace VNC.CodeAnalysis.SyntaxWalkers.VB
{
    public class AllNodes : VisualBasicSyntaxWalker
    {
        public StringBuilder StringBuilder;

        public AllNodes() : base(SyntaxWalkerDepth.StructuredTrivia)
        {
            
        }

        static int tabs = 0;

        public override void Visit(SyntaxNode node)
        {
            tabs++;
            var indents = new String(' ', tabs * 3);
            StringBuilder.AppendLine(indents + node.Kind());
            //Console.WriteLine(indents + node.Kind());
            base.Visit(node);
            tabs--;
        }

        public override void VisitToken(SyntaxToken token)
        {
            var indents = new String(' ', tabs * 3);
            StringBuilder.AppendLine(string.Format("{0}{1}:\t{2}", indents, token.Kind(), token));
            //Console.WriteLine(string.Format("{0}{1}:\t{2}", indents, token.Kind(), token));
            base.VisitToken(token);
        }     
    }
}
