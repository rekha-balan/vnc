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
    public class SimpleAsClause : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitSimpleAsClause(SimpleAsClauseSyntax node)
        {
            var n = node;

            var s = node.ToString();

            if (identifierNameRegEx.Match(node.ToString()).Success)
            {
                //if (FilterByType(node.Declarators.First().AsClause))
                //{
                RecordMatchAndContext(node, node.ToString());
                //Messages.AppendLine(String.Format("{0} {1}",
                //        GetNodeContext(node),
                //        node.ToString()));
                //}
            }

            base.VisitSimpleAsClause(node);
        }
    }
}
