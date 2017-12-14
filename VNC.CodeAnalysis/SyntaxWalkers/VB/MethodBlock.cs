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
        public override void VisitMethodBlock(MethodBlockSyntax node)
        {
            if (identifierNameRegEx.Match(node.SubOrFunctionStatement.Identifier.ToString()).Success)
            {
                RecordMatchAndContext(node, BlockType.MethodBlock);

                var walkerNode = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Node);
                var walkerToken = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Token);
                var walkerTrivia = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Trivia);
                var walkerStructuredTrivia = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.StructuredTrivia);

                // Get the contained MethodStatement

                var methodStatement = node.DescendantNodes().First();

                //var descendantNodesAndSelf = node.DescendantNodesAndSelf();
                //var descendantNodesAndTokens = node.DescendantNodesAndTokens();
                //var descendantNodesAndTokensAndSelf = node.DescendantNodesAndTokensAndSelf();
                //var descendantTokens = node.DescendantTokens();
                //var descendantTrivia = node.DescendantTrivia();

                 StringBuilder internalMessages = new StringBuilder();

                //walkerNode.Messages = Messages;
                //walkerToken.Messages = Messages;
                //walkerTrivia.Messages = Messages;
                //walkerStructuredTrivia.Messages = Messages;

                walkerNode.Messages = internalMessages;
                walkerToken.Messages = internalMessages;
                walkerTrivia.Messages = internalMessages;
                walkerStructuredTrivia.Messages = internalMessages;

                // Nodes

                walkerNode.Visit(methodStatement);
                CRC32Node = walkerNode.GetCRC32();
                WalkerNode.AppendLine(walkerNode.Messages.ToString());

                internalMessages.Clear();

                // Tokens

                walkerToken.Visit(methodStatement);
                CRC32Token = walkerToken.GetCRC32();
                WalkerToken.AppendLine(walkerNode.Messages.ToString());

                internalMessages.Clear();

                // Trivia

                walkerTrivia.Visit(methodStatement);
                CRC32Trivia = walkerTrivia.GetCRC32();
                WalkerTrivia.AppendLine(internalMessages.ToString());

                internalMessages.Clear();

                // Structured Trivia

                walkerStructuredTrivia.Visit(methodStatement);
                CRC32StructuredTrivia = walkerStructuredTrivia.GetCRC32();
                WalkerStructuredTrivia.AppendLine(internalMessages.ToString());
            }

            base.VisitMethodBlock(node);
        }
    }
}
