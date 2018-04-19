using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

using Crc32C;

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

                //var walkerNode = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Node);
                //var walkerToken = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Token);
                //var walkerTrivia = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.Trivia);
                //var walkerStructuredTrivia = new VNC.CodeAnalysis.SyntaxWalkers.VB.VisitAll(SyntaxWalkerDepth.StructuredTrivia);

                // Get the contained MethodStatement

                //IEnumerable<Microsoft.CodeAnalysis.SyntaxNode> analysisNodes = null;
                //IEnumerable<Microsoft.CodeAnalysis.SyntaxToken> analysisTokens = null;
                //IEnumerable<Microsoft.CodeAnalysis.SyntaxTrivia> analysisTrivia = null;
                //IEnumerable<Microsoft.CodeAnalysis.SyntaxNodeOrToken> analysisNodesOrTokens = null;
                //ChildSyntaxList childAnalysisNodes;

                //switch (_configurationOptions.AdditionalNodeAnalysis)
                //{
                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.Ancestors:
                //        analysisNodes = node.Ancestors();
                //        break;

                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.AncestorsAndSelf:
                //        analysisNodes = node.AncestorsAndSelf();
                //        break;

                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.ChildNodes:
                //        analysisNodes = node.ChildNodes();
                //        break;

                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.ChildNodesAndTokens:
                //        childAnalysisNodes = node.ChildNodesAndTokens();
                //        break;

                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.ChildTokens:
                //        analysisTokens = node.ChildTokens();
                //        break;

                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.DescendantNodes:
                //        analysisNodes = node.DescendantNodes();
                //        break;

                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.DescendantNodesAndSelf:
                //        analysisNodes = node.DescendantNodesAndSelf();
                //        break;

                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.DescendantNodesAndTokens:
                //        analysisNodesOrTokens = node.DescendantNodesAndTokens();
                //        break;

                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.DescendantNodesAndTokensAndSelf:
                //        analysisNodesOrTokens = node.DescendantNodesAndTokensAndSelf();
                //        break;

                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.DescendantTokens:
                //        analysisTokens = node.DescendantTokens();
                //        break;

                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.DescendantTrivia:
                //        analysisTrivia = node.DescendantTrivia();
                //        break;

                //    case CodeAnalysis.SyntaxNode.AdditionalNodes.None:
                //        break;

                //    default:
                //        // ??
                //        break;
                //}

                //StringBuilder analysisNodeKind = new StringBuilder();
                //StringBuilder analysisNodeValue = new StringBuilder();

                //bool displayAnalysisOutput = _configurationOptions.DisplayNodeKind || _configurationOptions.DisplayNodeValue;

                //if (analysisNodes != null)
                //{
                //    if (displayAnalysisOutput)
                //    {
                //        Messages.AppendLine(">>> AnalysisNodes >>>");
                //    }

                //    foreach (var item in analysisNodes)
                //    {
                //        var isBlock = item.Kind().ToString().Contains("Block");

                //        //if (isBlock)
                //        //{
                //        //    //continue;   // Can have trivia.
                //        //}

                //        //if (_configurationOptions.DisplayFormattedOutput)
                //        //{

                //        analysisNodeKind.AppendLine(string.Format("{0}", item.Kind().ToString()));

                //        analysisNodeValue.AppendLine(string.Format("{0}", isBlock ? "BLOCK" : item.ToString()));

                //        if (displayAnalysisOutput)
                //        {
                //            Messages.AppendLine(string.Format("Node:{0}:>{1}<",
                //                _configurationOptions.DisplayNodeKind ? item.Kind().ToString() : "",
                //                _configurationOptions.DisplayNodeValue ? isBlock ? "BLOCK" : item.ToString() : ""));
                //        }
                //        //}
                //        //else
                //        //{
                //        //    Messages.AppendLine(string.Format("Node:{0}:>{1}<",
                //        //        _configurationOptions.DisplayNodeKind ? item.Kind().ToString() : "",
                //        //        // Don't display contents of block.
                //        //        _configurationOptions.DisplayNodeValue ? isBlock ? "BLOCK" : item.ToString() : ""));
                //        //}

                //        //Messages.AppendFormat(String.Format("{0}\n", item.ToString()));

                //    }

                //    if (displayAnalysisOutput)
                //    {
                //        Messages.AppendLine("<<< AnalysisNodes <<<");
                //    }
                //}

                //if (analysisTokens != null)
                //{
                //    Messages.AppendLine(">>> AnalysisTokens >>>");

                //    foreach (var item in analysisTokens)
                //    {
                //        Messages.AppendFormat(String.Format("{0}\n", item.ToString()));
                //    }

                //    Messages.AppendLine("<<< AnalysisTokens <<<");
                //}

                //if (analysisTrivia != null)
                //{
                //    Messages.AppendLine(">>> AnalysisTrivia >>>");

                //    foreach (var item in analysisTrivia)
                //    {
                //        Messages.AppendFormat(String.Format("{0}\n", item.ToString()));
                //    }

                //    Messages.AppendLine("<<< AnalysisTrivia <<<");
                //}

                //if (analysisNodesOrTokens != null)
                //{
                //    Messages.AppendFormat(">>> AnalysisNodesOrTokens >>>");

                //    foreach (var item in analysisNodesOrTokens)
                //    {
                //        Messages.AppendFormat(String.Format("{0}\n", item.ToString()));
                //    }

                //    Messages.AppendFormat("<<< AnalysisNodesOrTokens <<<");
                //}

                //byte[] analysisToBytes = asciiEncoding.GetBytes(analysisNodeKind.ToString());
                //CRC32Node = Crc32CAlgorithm.Compute(analysisToBytes).ToString();

                //analysisToBytes = asciiEncoding.GetBytes(analysisNodeValue.ToString());
                //CRC32NodeKind = Crc32CAlgorithm.Compute(analysisToBytes).ToString();

                //if (childAnalysisNodes != null)
                //{
                //    Messages.AppendFormat("ChildAnalysisNodes");
                //    foreach (var item in childAnalysisNodes)
                //    {
                //        Messages.AppendFormat("{0}\n", item.ToString());
                //    }
                //}

                //var co = _configurationOptions.AdditionalNodeAnalysis;


                //var methodStatement = node.DescendantNodes().First();

                //var descendantNodes = node.DescendantNodes();
                //var descendantNodesAndSelf = node.DescendantNodesAndSelf();

                //var descendantNodesAndTokens = node.DescendantNodesAndTokens();
                //var descendantNodesAndTokensAndSelf = node.DescendantNodesAndTokensAndSelf();

                //var descendantTokens = node.DescendantTokens();
                
                //var descendantTrivia = node.DescendantTrivia();

                //StringBuilder internalMessages = new StringBuilder();

                ////walkerNode.Messages = Messages;
                ////walkerToken.Messages = Messages;
                ////walkerTrivia.Messages = Messages;
                ////walkerStructuredTrivia.Messages = Messages;

                //walkerNode.Messages = internalMessages;
                //walkerToken.Messages = internalMessages;
                //walkerTrivia.Messages = internalMessages;
                //walkerStructuredTrivia.Messages = internalMessages;

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

                //// Nodes

                //walkerNode.Visit(node);
                //CRC32Node = walkerNode.GetCRC32();
                //WalkerNode.AppendLine(walkerNode.Messages.ToString());

                //internalMessages.Clear();

                //// Tokens

                //walkerToken.Visit(node);
                //CRC32Token = walkerToken.GetCRC32();
                //WalkerToken.AppendLine(walkerNode.Messages.ToString());

                //internalMessages.Clear();

                //// Trivia

                //walkerTrivia.Visit(node);
                //CRC32Trivia = walkerTrivia.GetCRC32();
                //WalkerTrivia.AppendLine(internalMessages.ToString());

                //internalMessages.Clear();

                //// Structured Trivia

                //walkerStructuredTrivia.Visit(node);
                //CRC32StructuredTrivia = walkerStructuredTrivia.GetCRC32();
                //WalkerStructuredTrivia.AppendLine(internalMessages.ToString());

            }

            base.VisitMethodBlock(node);
        }
    }
}
