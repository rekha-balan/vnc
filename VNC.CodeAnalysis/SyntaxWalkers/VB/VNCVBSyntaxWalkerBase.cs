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
        static int tabs = 0;

        public ConfigurationOptions Display = new ConfigurationOptions();
        //public Boolean DisplayClassOrModuleName;
        //public Boolean DisplayMethodName;

        public string IdentifierNames;
        internal Regex identifierNameRegEx;

        public Dictionary<string, Int32> Matches;

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

            if (Display.ClassOrModuleName)
            {
                messageContext = Helpers.VB.GetContainingContext(node, Display);
            }

            if (Display.MethodName)
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
                Helpers.VB.GetContainingContext(node, Display),
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

        //public override void Visit(Microsoft.CodeAnalysis.SyntaxNode node)
        //{
        //    tabs++;
        //    var indents = new String(' ', tabs * 3);
        //    Messages.AppendLine(string.Format("{0}{1,6}{2}:>{3}<", indents, "Node:", node.Kind(), node.ToString()));

        //    // Call base to visit children

        //    base.Visit(node);
        //    tabs--;
        //}

        //public override void VisitToken(Microsoft.CodeAnalysis.SyntaxToken token)
        //{
        //    var indents = new String(' ', tabs * 3);
        //    Messages.AppendLine(string.Format("{0}{1,6}{2}:>{3}<", indents, "Token:", token.Kind(), token));

        //    // Call base to visit children

        //    base.VisitToken(token);
        //}

        //public override void VisitTrivia(Microsoft.CodeAnalysis.SyntaxTrivia trivia)
        //{
        //    var indents = new String(' ', tabs * 3);
        //    Messages.AppendLine(string.Format("{0}{1,6}{2}:>{3}<", indents, "Trivia:", trivia.Kind(), trivia));

        //    // Call base to visit children

        //    base.VisitTrivia(trivia);
        //}
    }
}
