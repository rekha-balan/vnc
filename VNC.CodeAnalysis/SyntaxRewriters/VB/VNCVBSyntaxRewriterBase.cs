using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using VNCCA = VNC.CodeAnalysis;

namespace VNC.CodeAnalysis.SyntaxRewriters.VB
{
    public class VNCVBSyntaxRewriterBase : VisualBasicSyntaxRewriter
    {
        public StringBuilder Messages;

        public ConfigurationOptions Display = new ConfigurationOptions();
        public string _targetInvocationExpression = null;
        //public string _newInvocationExpression = null;
        public Boolean PerformedReplacement = false;

        public string IdentifierNames;
        internal Regex identifierNameRegEx;

        public Dictionary<string, Int32> Replacements;

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

            if (Display.ClassOrModuleName)
            {
                messageContext = VNCCA.Helpers.VB.GetContainingContext(node, Display);
            }

            if (Display.MethodName)
            {
                messageContext += string.Format(" Method:({0, -35})", VNCCA.Helpers.VB.GetContainingMethod(node));
            }

            return messageContext;
        }

        public void RecordMatch(string nodeValue)
        {
            Messages.AppendLine(String.Format("{0}", 
                nodeValue));

            if (Replacements.ContainsKey(nodeValue))
            {
                Replacements[nodeValue] += 1;
            }
            else
            {
                Replacements.Add(nodeValue, 1);
            }
        }

        public void RecordMatchAndContext(VisualBasicSyntaxNode node, string nodeValue)
        {
            Messages.AppendLine(String.Format("{0} {1}",
                VNCCA.Helpers.VB.GetContainingContext(node, Display),
                nodeValue));

            if (Replacements.ContainsKey(nodeValue))
            {
                Replacements[nodeValue] += 1;
            }
            else
            {
                Replacements.Add(nodeValue, 1);
            }
        }

        public void RecordReplacementAndContext(VisualBasicSyntaxNode node, string oldNodeValue, string newNodeValue)
        {
            Messages.AppendLine(String.Format("{0} From:>{1}< To:>{2}<",
                VNCCA.Helpers.VB.GetContainingContext(node, Display),
                oldNodeValue,
                newNodeValue));

            if (Replacements.ContainsKey(oldNodeValue))
            {
                Replacements[oldNodeValue] += 1;
            }
            else
            {
                Replacements.Add(oldNodeValue, 1);
            }
        }
    }
}
