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
            if (_targetPatternRegEx.Match(node.Identifier.ToString()).Success)
            {
                if (FilterByType(node.AsClause))
                {
                    RecordMatchAndContext(node, BlockType.None);
                }
            }

            base.VisitPropertyStatement(node);
        }
    }
}
