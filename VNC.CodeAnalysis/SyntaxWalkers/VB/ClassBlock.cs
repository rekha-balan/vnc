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
    public class ClassBlock : VNCVBSyntaxWalkerBase
    {
        public override void VisitClassBlock(ClassBlockSyntax node)
        {
            if (identifierNameRegEx.Match(node.ClassStatement.Identifier.ToString()).Success)
            {
                RecordMatch(node, BlockType.ClassBlock);
            }

            base.VisitClassBlock(node);
        }
    }
}
