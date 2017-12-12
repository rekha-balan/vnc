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
    public class FieldDeclaration : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            if ( ! node.Parent.IsKind(SyntaxKind.StructureBlock))
            {
                if (identifierNameRegEx.Match(node.Declarators.First().Names.First().Identifier.ToString()).Success)
                {
                    // TODO(crhodes)
                    // Handle multiple declarations on each line

                    if (identifierNameRegEx.Match(node.Declarators.First().Names.First().ToString()).Success)
                    {
                        if (FilterByType(node.Declarators.First().AsClause))
                        {
                            RecordMatchAndContext(node, BlockType.None);
                        }
                    }
                }
            }

            base.VisitFieldDeclaration(node);
        }
    }
}
