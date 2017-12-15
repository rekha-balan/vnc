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
    public class LocalDeclarationStatement : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            if (_targetPatternRegEx.Match(node.Declarators.First().Names.First().Identifier.ToString()).Success)
            {
                // TODO(crhodes)
                // Need to handle multiple declarations.  For now, just pay attention to first

                if (_targetPatternRegEx.Match(node.Declarators.First().ToString()).Success)
                {
                    if (FilterByType(node.Declarators.First().AsClause))
                    {
                        RecordMatchAndContext(node, BlockType.None);
                    }
                }
            }

            base.VisitLocalDeclarationStatement(node);
        }
    }
}
