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
    public class ModuleBlock : VNCVBSyntaxWalkerBase
    {
        public override void VisitModuleBlock(ModuleBlockSyntax node)
        {
            if (identifierNameRegEx.Match(node.ModuleStatement.Identifier.ToString()).Success)
            {
                Messages.AppendLine(String.Format("{0}", node.ToString()));
            }

            base.VisitModuleBlock(node);
        }
    }
}
