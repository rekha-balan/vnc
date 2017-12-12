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
    public class InvocationExpressionInTryCatch : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (identifierNameRegEx.Match(node.Expression.ToString()).Success)
            {
                RecordMatchAndContext(node, BlockType.None);
            }

            // Call base to visit children

            base.VisitInvocationExpression(node);
        }
    }
}
