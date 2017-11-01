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
    public class VNCVBSyntaxWalkerBase : VisualBasicSyntaxWalker
    {
        public StringBuilder Messages;
        public Boolean DisplayClassOrModuleName;
        public Boolean DisplayMethodName;

        public string IdentifierNames;
        internal Regex identifierNameRegEx;

        public Dictionary<string, Int32> Matches;

        public VNCVBSyntaxWalkerBase() : base(SyntaxWalkerDepth.StructuredTrivia)
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

            if (DisplayClassOrModuleName)
            {
                messageContext = Helpers.VB.GetContainingType(node);
            }

            if (DisplayMethodName)
            {
                messageContext += string.Format(" Method:({0, -35})", Helpers.VB.GetContainingMethod(node));
            }

            return messageContext;
        }

        public void RecordMatch(string nodeValue)
        {
            Messages.AppendLine(String.Format("{0}", 
                nodeValue));

            if (Matches.ContainsKey(nodeValue))
            {
                Matches[nodeValue] += 1;
            }
            else
            {
                Matches.Add(nodeValue, 1);
            }
        }

        public void RecordMatchAndContext(VisualBasicSyntaxNode node, string nodeValue)
        {
            Messages.AppendLine(String.Format("{0} {1}",
                GetNodeContext(node),
                nodeValue));

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
