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
    public class PropertyBlock : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitPropertyBlock(PropertyBlockSyntax node)
        {
            if (identifierNameRegEx.Match(node.PropertyStatement.Identifier.ToString()).Success)
            {
                if (FilterByType(node.PropertyStatement.AsClause))
                {
                    Messages.AppendLine(String.Format("{0} {1}",
                        GetNodeContext(node),
                        node.ToString()));
                }
            }

            base.VisitPropertyBlock(node);
        }
    }
}
