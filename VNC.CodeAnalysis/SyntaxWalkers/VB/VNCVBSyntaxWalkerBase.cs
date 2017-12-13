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
    public class VNCVBSyntaxWalkerBase : VisualBasicSyntaxWalker
    {
        public StringBuilder Messages;
        public StringBuilder WalkerNode = new StringBuilder();
        public StringBuilder WalkerToken = new StringBuilder();
        public StringBuilder WalkerTrivia = new StringBuilder();
        public StringBuilder WalkerStructuredTrivia = new StringBuilder();
        static int tabs = 0;

        public ConfigurationOptions _configurationOptions = new ConfigurationOptions();
        //public Boolean DisplayClassOrModuleName;
        //public Boolean DisplayMethodName;

        public string IdentifierNames;
        internal Regex identifierNameRegEx;

        public Dictionary<string, Int32> Matches;
        public Dictionary<string, Int32> CRCMatchesToString;
        public Dictionary<string, Int32> CRCMatchesToFullString;

        public string CRC32Node;
        public string CRC32Token;
        public string CRC32Trivia;
        public string CRC32StructuredTrivia;

        ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();

        public VNCVBSyntaxWalkerBase(SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node) : base(depth)
        {

        }

        public virtual void InitializeRegEx()
        {
            try
            {
                identifierNameRegEx = new Regex(IdentifierNames, RegexOptions.IgnoreCase);
            }
            catch (Exception ex)
            {
                Messages.AppendLine(string.Format("Error in IdentifierNames RegEx >{0}< Error:({1}), using >.*<",
                    IdentifierNames, ex.Message));
                identifierNameRegEx = new Regex(".*", RegexOptions.IgnoreCase);
            }
        }

        public string GetNodeContext(VisualBasicSyntaxNode node)
        {
            string messageContext = "";

            if (_configurationOptions.ClassOrModuleName)
            {
                messageContext = Helpers.VB.GetContainingContext(node, _configurationOptions);
            }

            if (_configurationOptions.MethodName)
            {
                messageContext += string.Format(" Method:({0, -35})", Helpers.VB.GetContainingMethod(node));
            }

            return messageContext;
        }

        public enum BlockType : Int16
        {
            None = 0,
            NamespaceBlock = 1,
            ClassBlock = 2,
            ModuleBlock = 3,
            MethodBlock = 4
        }

        public void RecordMatch(VisualBasicSyntaxNode node, BlockType blockType)
        {
            string nodeValue = "";

            // Produce a useful string to use for the Matches dictionary
            // Don't want to use a long string

            switch (blockType)
            {
                case BlockType.None:
                    nodeValue = node.ToString();
                    break;

                case BlockType.NamespaceBlock:
                    nodeValue = ((NamespaceBlockSyntax)node).NamespaceStatement.Name.ToString();
                    break;

                case BlockType.ClassBlock:
                    nodeValue = ((ClassBlockSyntax)node).ClassStatement.Identifier.ToString();
                    break;

                case BlockType.ModuleBlock:
                    nodeValue = ((ModuleBlockSyntax)node).ModuleStatement.Identifier.ToString();
                    break;

                case BlockType.MethodBlock:
                    nodeValue = ((MethodBlockSyntax)node).SubOrFunctionStatement.Identifier.ToString();
                    break;
            }

            if (_configurationOptions.ReplaceCRLF)
            {
                nodeValue = nodeValue.Replace("\r\n", " ");
            }

            if (_configurationOptions.CRC32)
            {
                byte[] nodeToStringBytes = asciiEncoding.GetBytes(node.ToString());
                byte[] nodeToFullStringBytes = asciiEncoding.GetBytes(node.ToFullString());
                string toStringCRC = Crc32CAlgorithm.Compute(nodeToStringBytes).ToString();
                string toFullStringCRC = Crc32CAlgorithm.Compute(nodeToFullStringBytes).ToString();

                Messages.AppendLine(String.Format("{0, -35} CRC32:({1,10}) ({2,10})",
                    nodeValue,
                    toStringCRC,
                    toFullStringCRC));

                string toStringKey = nodeValue + ":" + toStringCRC;
                string toFullStringKey = nodeValue+ ":" + toFullStringCRC;

                // The Node

                if (CRCMatchesToString.ContainsKey(toStringKey))
                {
                    CRCMatchesToString[toStringKey] += 1;
                }
                else
                {
                    CRCMatchesToString.Add(toStringKey, 1);
                }

                // The Node and Trivia

                if (CRCMatchesToFullString.ContainsKey(toFullStringKey))
                {
                    CRCMatchesToFullString[toFullStringKey] += 1;
                }
                else
                {
                    CRCMatchesToFullString.Add(toFullStringKey, 1);
                }
            }
            else
            {
                Messages.AppendLine(String.Format("{0}",
                    nodeValue));
            }

            if (Matches.ContainsKey(nodeValue))
            {
                Matches[nodeValue] += 1;
            }
            else
            {
                Matches.Add(nodeValue, 1);
            }
        }

        public void RecordMatchAndContext(VisualBasicSyntaxNode node, BlockType blockType)
        {
            string nodeValue = "";

            // Produce a useful string to use for the Matches dictionary
            // Don't want to use a long string

            switch (blockType)
            {
                case BlockType.None:
                    nodeValue = node.ToString();
                    break;

                case BlockType.NamespaceBlock:
                    nodeValue = ((NamespaceBlockSyntax)node).NamespaceStatement.Name.ToString();
                    break;

                case BlockType.ClassBlock:
                    nodeValue = ((ClassBlockSyntax)node).ClassStatement.Identifier.ToString();
                    break;

                case BlockType.ModuleBlock:
                    nodeValue = ((ModuleBlockSyntax)node).ModuleStatement.Identifier.ToString();
                    break;

                case BlockType.MethodBlock:
                    nodeValue = ((MethodBlockSyntax)node).SubOrFunctionStatement.Identifier.ToString();
                    break;
            }

            if (_configurationOptions.ReplaceCRLF)
            {
                nodeValue = nodeValue.Replace("\r\n", " ");
            }

            if (_configurationOptions.CRC32)
            {
                byte[] nodeToStringBytes = asciiEncoding.GetBytes(node.ToString());
                byte[] nodeToFullStringBytes = asciiEncoding.GetBytes(node.ToFullString());
                string toStringCRC = Crc32CAlgorithm.Compute(nodeToStringBytes).ToString();
                string toFullStringCRC = Crc32CAlgorithm.Compute(nodeToFullStringBytes).ToString();

                // TODO(crhodes)
                // Decide if more useful to have CRC first or last.

                //Messages.AppendLine(String.Format("{0} {1,-35} CRC32:({2,10}) ({3,10})",
                Messages.AppendLine(String.Format("CRC32:({2,10}) ({3,10}) {0} {1,-35}",
                    Helpers.VB.GetContainingContext(node, _configurationOptions),
                    nodeValue,
                    toStringCRC,
                    toFullStringCRC));

                //string toStringKey = string.Format("{0}:({1,10})", nodeValue, toStringCRC);
                //string toFullStringKey = string.Format("{0}:({1,10})", nodeValue, toFullStringCRC);

                string toStringKey = string.Format("({0,10}):{1}", toStringCRC, nodeValue);
                string toFullStringKey = string.Format("({0,10}):{1}", toFullStringCRC, nodeValue);


                // The Node

                if (CRCMatchesToString.ContainsKey(toStringKey))
                {
                    CRCMatchesToString[toStringKey] += 1;
                }
                else
                {
                    CRCMatchesToString.Add(toStringKey, 1);
                }

                // The Node and Trivia

                if (CRCMatchesToFullString.ContainsKey(toFullStringKey))
                {
                    CRCMatchesToFullString[toFullStringKey] += 1;
                }
                else
                {
                    CRCMatchesToFullString.Add(toFullStringKey, 1);
                }
            }
            else
            {
                Messages.AppendLine(String.Format("{0} {1}",
                    Helpers.VB.GetContainingContext(node, _configurationOptions),
                    nodeValue));
            }

            // TODO(crhodes)
            // May want to just use the CRC stuff

            if (Matches.ContainsKey(nodeValue))
            {
                Matches[nodeValue] += 1;
            }
            else
            {
                Matches.Add(nodeValue, 1);
            }
        }

        /// <summary>
        /// Used by MethodStatement so we can pass in nodeValue
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeValue"></param>
        public void RecordMatchAndContext(VisualBasicSyntaxNode node, string nodeValue)
        {
            if (_configurationOptions.CRC32)
            {
                byte[] nodeToStringBytes = asciiEncoding.GetBytes(node.ToString());
                byte[] nodeToFullStringBytes = asciiEncoding.GetBytes(node.ToFullString());
                string toStringCRC = Crc32CAlgorithm.Compute(nodeToStringBytes).ToString();
                string toFullStringCRC = Crc32CAlgorithm.Compute(nodeToFullStringBytes).ToString();

                // TODO(crhodes)
                // Decide if more useful to have CRC first or last.

                //Messages.AppendLine(String.Format("{0} {1,-35} CRC32:({2,10}) ({3,10})",
                Messages.AppendLine(String.Format("CRC32:({2,10}) ({3,10}) {0} {1,-35}",
                    Helpers.VB.GetContainingContext(node, _configurationOptions),
                    nodeValue,
                    toStringCRC,
                    toFullStringCRC));

                //string toStringKey = string.Format("{0}:({1,10})", nodeValue, toStringCRC);
                //string toFullStringKey = string.Format("{0}:({1,10})", nodeValue, toFullStringCRC);

                string toStringKey = string.Format("({0,10}):{1}", toStringCRC, nodeValue);
                string toFullStringKey = string.Format("({0,10}):{1}", toFullStringCRC, nodeValue);

                // The Node

                if (CRCMatchesToString.ContainsKey(toStringKey))
                {
                    CRCMatchesToString[toStringKey] += 1;
                }
                else
                {
                    CRCMatchesToString.Add(toStringKey, 1);
                }

                // The Node and Trivia

                if (CRCMatchesToFullString.ContainsKey(toFullStringKey))
                {
                    CRCMatchesToFullString[toFullStringKey] += 1;
                }
                else
                {
                    CRCMatchesToFullString.Add(toFullStringKey, 1);
                }
            }
            else
            {
                Messages.AppendLine(String.Format("{0} {1}",
                    Helpers.VB.GetContainingContext(node, _configurationOptions),
                    nodeValue));
            }

            // TODO(crhodes)
            // May want to just use the CRC stuff

            if (Matches.ContainsKey(nodeValue))
            {
                Matches[nodeValue] += 1;
            }
            else
            {
                Matches.Add(nodeValue, 1);
            }
        }
    }
}
