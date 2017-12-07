using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace VNC.CodeAnalysis.Helpers
{
    public static class CS
    {
        //Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax
        public static BlockSyntax GetContainingBlock(Microsoft.CodeAnalysis.SyntaxNode node)
        {
            var block = node.Parent as BlockSyntax;

            if (block != null)
            {
                return block;
            }

            return GetContainingBlock(node.Parent);
        }
    }
}
