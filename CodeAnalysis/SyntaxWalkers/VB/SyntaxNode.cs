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
    public class SyntaxNode : VNCVBSyntaxWalkerBase
    {
        public override void Visit(Microsoft.CodeAnalysis.SyntaxNode node)
        {
            if (_targetPatternRegEx.Match(node.ToString()).Success)
            {
                Messages.AppendLine(String.Format("{0} >{1}<",
                    GetNodeContext((VisualBasicSyntaxNode)node),
                    node.ToString()));
            }

            base.Visit(node);
        }
    }
}
