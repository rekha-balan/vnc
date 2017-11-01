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
    public class PropertyStatement : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitPropertyStatement(PropertyStatementSyntax node)
        {
            if (identifierNameRegEx.Match(node.Identifier.ToString()).Success)
            {
                if (FilterByType(node.AsClause))
                {
                    RecordMatchAndContext(node, node.ToString());
                    //Messages.AppendLine(String.Format("{0} {1}",
                    //    GetNodeContext(node),
                    //    node.ToString()));
                }
            }

            base.VisitPropertyStatement(node);
        }
    }
}
