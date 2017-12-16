using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Crc32C;

namespace VNC.CodeAnalysis.SyntaxWalkers.VB
{
    public class VisitAll : VisualBasicSyntaxWalker
    {
        public StringBuilder Messages;

        public StringBuilder WalkerNode = new StringBuilder();
        public StringBuilder WalkerToken = new StringBuilder();
        public StringBuilder WalkerTrivia = new StringBuilder();
        public StringBuilder WalkerStructureTrivia = new StringBuilder();

        ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();

        public VisitAll(SyntaxWalkerDepth depth = SyntaxWalkerDepth.StructuredTrivia) : base(depth)
        {      
        }

        public string GetCRC32()
        {
            byte[] messagesToBytes = asciiEncoding.GetBytes(Messages.ToString());

            return Crc32CAlgorithm.Compute(messagesToBytes).ToString();
        }

        static int tabs = 0;
        const int tabWidth = 2;

        public override void Visit(Microsoft.CodeAnalysis.SyntaxNode node)
        {
            tabs++;
            var indents = new String(' ', tabs * tabWidth);
            Messages.AppendLine(string.Format("Node:{0}{1}:>{2}<", indents, node.Kind(), node.ToString()));

            // Call base to visit children

            base.Visit(node);
            tabs--;
        }

        public override void VisitToken(Microsoft.CodeAnalysis.SyntaxToken token)
        {
            var indents = new String(' ', tabs * tabWidth);
            Messages.AppendLine(string.Format("Token:{0}{1}:>{2}<", indents, token.Kind(), token));

            // Call base to visit children

            base.VisitToken(token);
        }

        public override void VisitTrivia(Microsoft.CodeAnalysis.SyntaxTrivia trivia)
        {
            var indents = new String(' ', tabs * tabWidth);
            Messages.AppendLine(string.Format("Trivia:{0}{1}:>{2}<", indents, trivia.Kind(), trivia));

            // Call base to visit children

            base.VisitTrivia(trivia);
        }
    }
}
