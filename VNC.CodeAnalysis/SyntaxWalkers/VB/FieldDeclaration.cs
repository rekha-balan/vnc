﻿using System;
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
        public FieldDeclaration(VNC.CodeAnalysis.SyntaxNode.FieldDeclarationLocation declarationLocation)
        {
            _declarationLocation = declarationLocation;
        }


        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            // Verify we have the correct context for the Field Declaration

            var parent = node.Parent;

            switch (_declarationLocation)
            {
                case VNC.CodeAnalysis.SyntaxNode.FieldDeclarationLocation.Class:
                    if (parent.Kind() != SyntaxKind.ClassBlock)
                    {
                        return;
                    }
                    break;

                case VNC.CodeAnalysis.SyntaxNode.FieldDeclarationLocation.Module:
                    if (parent.Kind() != SyntaxKind.ModuleBlock)
                    {
                        return;
                    }
                    break;

                case VNC.CodeAnalysis.SyntaxNode.FieldDeclarationLocation.Structure:
                    if (parent.Kind() != SyntaxKind.StructureBlock)
                    {
                        return;
                    }
                    break;
            }

            //if ( ! node.Parent.IsKind(SyntaxKind.StructureBlock))
            //{
                if (_targetPatternRegEx.Match(node.Declarators.First().Names.First().Identifier.ToString()).Success)
                {
                    // TODO(crhodes)
                    // Handle multiple declarations on each line

                    if (_targetPatternRegEx.Match(node.Declarators.First().Names.First().ToString()).Success)
                    {
                        if (FilterByType(node.Declarators.First().AsClause))
                        {
                            RecordMatchAndContext(node, BlockType.None);
                        }
                    }
                }
            //}

            base.VisitFieldDeclaration(node);
        }
    }
}
