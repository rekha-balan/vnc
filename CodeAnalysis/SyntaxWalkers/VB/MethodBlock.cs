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
            if (_targetPatternRegEx.Match(node.SubOrFunctionStatement.Identifier.ToString()).Success)
            {
                RecordMatchAndContext(node, BlockType.MethodBlock);

                // Everything below here is an attempt to determine if a method 
                // is Syntactically the same as another method.
                //
                // Some challenges.
                // If only nodes kinds are evaluated things will show as the same even if different names or trivia
                //
                // If NodeValues are considered, at least two problems occur.
                // 1. If we evaluate the body of the method, any trivia in the body is evaluated.
                // 2. If we we don't evaluate the body, we have to iterate across the descendants

                var walkerNode = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Node);
                var walkerToken = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Token);
                var walkerTrivia = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Trivia);
                var walkerStructuredTrivia = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.StructuredTrivia);

                // Get the contained MethodStatement

                var co = _configurationOptions.AdditionalNodeAnalysis;
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
