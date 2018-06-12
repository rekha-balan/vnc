using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace VNC.CodeAnalysis.SyntaxRewriters.VB
{
    public class RewriteStopOrEndStatement : VNCVBSyntaxRewriterBase
    {
        public string _newInvocationExpression = null;

        public RewriteStopOrEndStatement()
        {
            
        }

        public override Microsoft.CodeAnalysis.SyntaxNode VisitStopOrEndStatement(StopOrEndStatementSyntax node)
        {
            ExpressionStatementSyntax newNode = (ExpressionStatementSyntax)SyntaxFactory.ParseExecutableStatement("System.Environment.Exit(0)");

            
            return newNode.WithTriviaFrom(node);
            //// We are hoping that specifying CellFormat.Font.Color is enough

            //if (_targetPatternRegEx.Match(node.Left.ToString()).Success)
            //{
            //    var leftSide = node.Left;
            //    var rightSide = node.Right;

            //    var leftChildren = leftSide.ChildNodesAndTokens();
            //    var lastIdentifier = leftSide.ChildNodesAndTokens().Last();

            //    //e.Worksheet.Rows(intCurrentRowIdx).Cells(0).CellFormat.Font.ColorInfo = New ColorInfo(Color.FromArgb(0, 64, 128))
            //    //e.Worksheet.Rows(intCurrentRowIdx).Cells(0).CellFormat.Font.ColorInfo = New Infragistics.Documents.Excel.WorkbookColorInfo(Color.FromArgb(0, 64, 128))

            //    var newLeftSideSimpleMemberAccessExpression = SyntaxFactory.ParseExpression(
            //        string.Format("{0}.{1}",
            //            ((MemberAccessExpressionSyntax)leftSide).Expression.ToString(),
            //            "ColorInfo "));

            //    var newRightSideInvocationExpression = SyntaxFactory.ParseExpression(
            //        string.Format("New Infragistics.Documents.Excel.WorkbookColorInfo({0})",
            //        rightSide.ToString()
            //        ));


            //    newNode = node.WithLeft(newLeftSideSimpleMemberAccessExpression);

            //    newNode = newNode.WithRight(newRightSideInvocationExpression).WithTriviaFrom(node);

            //    RecordReplacementAndContext(node, node.ToString(), newNode.ToString());

            //    //newNode = node;
            //    PerformedReplacement = true;
            //}
            //else
            //{
            //    newNode = node;
            //}

            //// Call base to replace children

            //return base.VisitAssignmentStatement(newNode);
        }
    }
}
