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


                //Messages.AppendLine("\nCall walkerNode.Visit()\n");
                walkerNode.Visit(node.DescendantNodes().First());
                CRC32Node = walkerNode.GetCRC32();
                WalkerNode.AppendLine(walkerNode.Messages.ToString());

                //Messages.AppendLine(internalMessages.ToString());

                internalMessages.Clear();

                //Messages.AppendLine("\nCall walkerToken.Visit()\n");
                walkerToken.Visit(node.DescendantNodes().First());
                CRC32Token = walkerToken.GetCRC32();
                WalkerToken.AppendLine(walkerNode.Messages.ToString());
                //Messages.AppendLine(internalMessages.ToString());

                internalMessages.Clear();

                //Messages.AppendLine("\nCall walkerTrivia.Visit()\n");
                walkerTrivia.Visit(node.DescendantNodes().First());
                CRC32Trivia = walkerTrivia.GetCRC32();
                WalkerTrivia.AppendLine(internalMessages.ToString());
                //Messages.AppendLine(internalMessages.ToString());

                internalMessages.Clear();

                //Messages.AppendLine("\nCall walkerStructuredTrivia.Visit()\n");
                walkerStructuredTrivia.Visit(node.DescendantNodes().First());
                CRC32StructuredTrivia = walkerStructuredTrivia.GetCRC32();
                WalkerStructuredTrivia.AppendLine(internalMessages.ToString());
                //Messages.AppendLine(internalMessages.ToString());
            }

            base.VisitMethodBlock(node);
        }
    }
}
