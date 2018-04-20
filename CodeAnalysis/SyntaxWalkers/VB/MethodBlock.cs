using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

using Crc32C;

namespace VNC.CodeAnalysis.SyntaxWalkers.VB
{
    public class MethodBlock : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitMethodBlock(MethodBlockSyntax node)
        {
            if (_targetPatternRegEx.Match(node.SubOrFunctionStatement.Identifier.ToString()).Success)
            {
                RecordMatchAndContext(node, BlockType.MethodBlock);                    
            }

            base.VisitMethodBlock(node);
        }
    }
}
