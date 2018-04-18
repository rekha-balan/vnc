using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace VNC.CodeAnalysis.SyntaxWalkers.VB
{
    public class MethodBlock : VNCVBTypedSyntaxWalkerBase
    {
        bool displayNodeKind = false;
        bool displayNodeValue = false;
        bool displayFormattedOutput = false;

        public MethodBlock(SyntaxWalkerDepth depth = SyntaxWalkerDepth.StructuredTrivia,
            bool display_NodeKind = true, bool display_NodeValue = false, bool display_FormattedOutput = false) : base(depth)
        {
            displayNodeKind = display_NodeKind;
            displayNodeValue = display_NodeValue;
            displayFormattedOutput = display_FormattedOutput;
        }

        public override void VisitMethodBlock(MethodBlockSyntax node)
        {
            if (_targetPatternRegEx.Match(node.SubOrFunctionStatement.Identifier.ToString()).Success)
            {
                RecordMatchAndContext(node, BlockType.MethodBlock);


                // Everything below here is an attempt to determine if a method is Syntactically the same
                // as another method.

                var walkerNode = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Node, displayNodeKind, displayNodeValue, displayFormattedOutput);
                var walkerToken = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Token, displayNodeKind, displayNodeValue, displayFormattedOutput);
                var walkerTrivia = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Trivia, displayNodeKind, displayNodeValue, displayFormattedOutput);
                var walkerStructuredTrivia = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.StructuredTrivia, displayNodeKind, displayNodeValue, displayFormattedOutput);

                // Get the contained MethodStatement

                var methodStatement = node.DescendantNodes().First();

                var descendantNodes = node.DescendantNodes();
                var descendantNodesAndSelf = node.DescendantNodesAndSelf();
                var descendantNodesAndTokens = node.DescendantNodesAndTokens();
                var descendantNodesAndTokensAndSelf = node.DescendantNodesAndTokensAndSelf();
                var descendantTokens = node.DescendantTokens();
                var descendantTrivia = node.DescendantTrivia();

                StringBuilder internalMessages = new StringBuilder();

                //walkerNode.Messages = Messages;
                //walkerToken.Messages = Messages;
                //walkerTrivia.Messages = Messages;
                //walkerStructuredTrivia.Messages = Messages;

                walkerNode.Messages = internalMessages;
                walkerToken.Messages = internalMessages;
                walkerTrivia.Messages = internalMessages;
                walkerStructuredTrivia.Messages = internalMessages;

                // HACK(crhodes)
                // The following always returns the same CRC.  I wonder if it is because we are visiting the methodStatement
                // Changing to node (MethodBlock) did not change this.  Need to think through what we are trying to accomplish here.
                // Not sure we are getting value from this.
                //

                //// Nodes

                //walkerNode.Visit(methodStatement);
                //CRC32Node = walkerNode.GetCRC32();
                //WalkerNode.AppendLine(walkerNode.Messages.ToString());

                //internalMessages.Clear();

                //// Tokens

                //walkerToken.Visit(methodStatement);
                //CRC32Token = walkerToken.GetCRC32();
                //WalkerToken.AppendLine(walkerNode.Messages.ToString());

                //internalMessages.Clear();

                //// Trivia

                //walkerTrivia.Visit(methodStatement);
                //CRC32Trivia = walkerTrivia.GetCRC32();
                //WalkerTrivia.AppendLine(internalMessages.ToString());

                //internalMessages.Clear();

                //// Structured Trivia

                //walkerStructuredTrivia.Visit(methodStatement);
                //CRC32StructuredTrivia = walkerStructuredTrivia.GetCRC32();
                //WalkerStructuredTrivia.AppendLine(internalMessages.ToString());

                // Nodes

                walkerNode.Visit(node);
                CRC32Node = walkerNode.GetCRC32();
                WalkerNode.AppendLine(walkerNode.Messages.ToString());

                internalMessages.Clear();

                // Tokens

                walkerToken.Visit(node);
                CRC32Token = walkerToken.GetCRC32();
                WalkerToken.AppendLine(walkerNode.Messages.ToString());

                internalMessages.Clear();

                // Trivia

                walkerTrivia.Visit(node);
                CRC32Trivia = walkerTrivia.GetCRC32();
                WalkerTrivia.AppendLine(internalMessages.ToString());

                internalMessages.Clear();

                // Structured Trivia

                walkerStructuredTrivia.Visit(node);
                CRC32StructuredTrivia = walkerStructuredTrivia.GetCRC32();
                WalkerStructuredTrivia.AppendLine(internalMessages.ToString());

            }

            base.VisitMethodBlock(node);
        }
    }
}
