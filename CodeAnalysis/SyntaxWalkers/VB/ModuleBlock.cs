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
    public class ModuleBlock : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitModuleBlock(ModuleBlockSyntax node)
        {
            if (_targetPatternRegEx.Match(node.ModuleStatement.Identifier.ToString()).Success)
            {
                RecordMatch(node, BlockType.ModuleBlock);
            }

            base.VisitModuleBlock(node);
        }
    }
}
