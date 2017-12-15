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
    public class AssignmentStatement : VNCVBSyntaxWalkerBase
    {
        public Boolean MatchLeft = true;

        public Boolean MatchRight = true;

        public override void VisitAssignmentStatement(AssignmentStatementSyntax node)
        {
            if (MatchLeft)
            {
                if (_targetPatternRegEx.Match(node.Left.ToString()).Success)
                {
                    RecordMatchAndContext(node, BlockType.None);
                }
            }

            if (MatchRight)
            {
                if (_targetPatternRegEx.Match(node.Right.ToString()).Success)
                {
                    RecordMatchAndContext(node, BlockType.None);
                }
            }

            base.VisitAssignmentStatement(node);
        }
    }
}
