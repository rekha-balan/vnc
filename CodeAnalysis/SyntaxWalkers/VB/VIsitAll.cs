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

        bool displayNodeKind = false;
        bool displayNodeValue = false;
        bool displayFormattedOutput = false;

        public VisitAll(SyntaxWalkerDepth depth = SyntaxWalkerDepth.StructuredTrivia,
            bool display_NodeKind = true, bool display_NodeValue = false, bool display_FormattedOutput = false) : base(depth)
        {
            displayNodeKind = display_NodeKind;
            displayNodeValue = display_NodeValue;
            displayFormattedOutput = display_FormattedOutput;
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

            if (node.Kind() != SyntaxKind.CompilationUnit)
            {
                var indents = new String(' ', tabs * tabWidth);

                if (displayFormattedOutput)
                {
                    Messages.AppendLine(string.Format("Node:{0}{1}:>{2}<", indents, 
                        displayNodeKind ? node.Kind().ToString() : "",
                        displayNodeValue ? node.ToString() : ""));
                }
                else
                {
                    Messages.AppendLine(string.Format("Node:{0}:>{1}<",
                        displayNodeKind ? node.Kind().ToString() : "",
                        displayNodeValue ? node.ToString() : ""));
                }
            }

            // Call base to visit children

            base.Visit(node);
            tabs--;
        }

        public override void VisitToken(Microsoft.CodeAnalysis.SyntaxToken token)
        {
            var indents = new String(' ', tabs * tabWidth);

            if (displayFormattedOutput)
            {
                Messages.AppendLine(string.Format("Token:{0}{1}:>{2}<", indents,
                        displayNodeKind ? token.Kind().ToString() : "",
                        displayNodeValue ? token.ToString() : ""));
            }
            else
            {
                Messages.AppendLine(string.Format("Token:{0}:>{1}<",
                        displayNodeKind ? token.Kind().ToString() : "",
                        displayNodeValue ? token.ToString() : ""));
            }

            // Call base to visit children

            base.VisitToken(token);
        }

        public override void VisitTrivia(Microsoft.CodeAnalysis.SyntaxTrivia trivia)
        {
            var indents = new String(' ', tabs * tabWidth);

            if (displayFormattedOutput)
            {
                Messages.AppendLine(string.Format("Trivia:{0}{1}:>{2}<", indents,
                        displayNodeKind ? trivia.Kind().ToString() : "",
                        displayNodeValue ? trivia.ToString() : ""));
            }
            else
            {
                Messages.AppendLine(string.Format("Trivia:{0}:>{1}<",
                        displayNodeKind ? trivia.Kind().ToString() : "",
                        displayNodeValue ? trivia.ToString() : ""));
            }

            // Call base to visit children

            base.VisitTrivia(trivia);
        }
    }
}
