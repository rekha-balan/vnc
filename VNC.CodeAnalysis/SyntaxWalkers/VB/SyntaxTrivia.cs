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
    public class SyntaxTrivia : VNCVBSyntaxWalkerBase
    {
        public override void VisitTrivia(Microsoft.CodeAnalysis.SyntaxTrivia trivia)
        {
            var st = trivia.SyntaxTree;
            string s = trivia.ToString();
            var p = trivia.GetLocation();

            if (! String.IsNullOrWhiteSpace(trivia.ToString()))
            {
                if (_targetPatternRegEx.Match(trivia.ToString()).Success)
                {
                    Messages.AppendLine(String.Format("{0} >{1}<",
                        //GetNodeContext(trivia.SyntaxTree),
                        "",
                        trivia.ToString()));
                }                
            }


            //base.VisitAssignmentStatement(node);

            //var indents = new String(' ', tabs * 3);
            //StringBuilder.AppendLine(string.Format("{0}{1}:>{2}<", indents, trivia.Kind(), trivia));

            // Call base to visit children

            base.VisitTrivia(trivia);
        }
    }
}
