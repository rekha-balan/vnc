using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.CodeAnalysis;
using VNCCA = VNC.CodeAnalysis;

namespace VNC.CodeAnalysis.SyntaxRewriters.VB
{
    public class RewriterIdeas : VNCVBSyntaxRewriterBase
    {

        //public InvocationExpression(bool visitIntoStructuredTrivia = false) : base(visitIntoStructuredTrivia)
        //{

        //}

        public RewriterIdeas(string TargetInvocationExpression)
        {
            TargetPattern = TargetInvocationExpression;
            //_newInvocationExpression = NewInvocationExpression;
        }

        //public WrapSQLCallsInDebug2(string TargetInvocationExpression, string NewInvocationExpression)
        //{
        //    _targetInvocationExpression = TargetInvocationExpression;
        //    _newInvocationExpression = NewInvocationExpression;
        //}

        public override Microsoft.CodeAnalysis.SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (TargetPattern == null)
            {
                return node;
            }

            //Microsoft.CodeAnalysis.SyntaxNode newInvocation = null;
            var expression = node.Expression;
            //InvocationExpressionSyntax newInvocationExpression = null;
            InvocationExpressionSyntax newInvocationExpression = node;

            //var newExpression = SyntaxFactory.IdentifierName(_newInvocationExpression);

            // Decide if want to use RegEx here.

            if (expression.ToString() == TargetPattern)
            {
                var location = node.GetLocation();
                var startLine = location.GetLineSpan().StartLinePosition.Line;
                var endLine = location.GetLineSpan().EndLinePosition.Line;

                var simpleMemberAccessExpression = node.ChildNodes().First();
                var firstIdentifier = simpleMemberAccessExpression.ChildNodes().First();
                var lastIdentifer = simpleMemberAccessExpression.ChildNodes().Last();

                //Dim dbTicks as Long = Log.Debug2("Execution Start", LOG_APPNAME)
                //dbReader = dbCmd.ExecuteReader()
                //Log.Trace(String.Format("Execution End: strSQL:{({0})", strSQL), LOG_APPNAME, dbTicks)

                //LocalDeclarationStatementSyntax declareVariable = GetDeclareString("myString");

                LocalDeclarationStatementSyntax logStart = GetLogStartSyntax();

                InvocationExpressionSyntax logEnd = GetLogEndSyntax();

                List<Microsoft.CodeAnalysis.SyntaxNode> newBeforeNodes = new List<Microsoft.CodeAnalysis.SyntaxNode>();
                newBeforeNodes.Add(logStart);

                List<Microsoft.CodeAnalysis.SyntaxNode> newAfterNodes = new List<Microsoft.CodeAnalysis.SyntaxNode>();
                newAfterNodes.Add(logEnd);

                List<SyntaxTrivia> newTrivia = new List<SyntaxTrivia>();

                string existingLeadingTrivia = node.GetLeadingTrivia().ToString();
                string existingLeadingTriviaFull = node.GetLeadingTrivia().ToFullString();

                string existingTrailingTrivia = node.GetTrailingTrivia().ToString();
                string existingTrailingTriviaFull = node.GetTrailingTrivia().ToFullString();

                // Verify this expression is on line by itself

                if (VNCCA.Helpers.VB.IsOnLineByItself(node))
                {
                    string startOfLineWhiteSpace = existingLeadingTrivia.Replace(System.Environment.NewLine, "");

                    newTrivia.Add(SyntaxFactory.CommentTrivia(existingLeadingTriviaFull));
                    //newTrivia.Add(SyntaxFactory.CommentTrivia(VNCCA.Helpers.VB.MultiLineComment(_comment, startOfLineWhiteSpace)));
                    //newTrivia.Add(SyntaxFactory.CommentTrivia("' "));

                    var newExpression = SyntaxFactory.ParseExpression(string.Format("DAL.Helpers.{0}({1})", lastIdentifer, firstIdentifier));
                    newInvocationExpression = (InvocationExpressionSyntax)newExpression.WithTriviaFrom(node);
                    //newInvocationExpression = node.WithLeadingTrivia(newTrivia);

                }
                else
                {
                    Messages.AppendLine(String.Format("node: >{0}< >{1}< Is NOT OnLineByItself", node.ToString(), node.ToFullString()));
                    newInvocationExpression = node;
                }



                //try
                //{
                //    var root = node.SyntaxTree.GetRoot();

                //    var nodeParent = node.Parent;
                //    var nodeParentParent = nodeParent.Parent;
                    
                //    var newSomething = root.InsertNodesAfter(node, newAfterNodes);
                //}
                //catch (Exception ex)
                //{
                    
                //}

                //try
                //{
                //    //var newSomething2 = node.InsertNodesAfter()
                //}
                //catch (Exception ex)
                //{

                //}

                //newInvocationExpression = node.WithExpression(newExpression);

                //newInvocationExpression = node.InsertNodesBefore(node, newBeforeNodes);

                //try
                //{
                //    //newInvocationExpression = node.InsertNodesAfter(node.Parent, newAfterNodes);
                //    node.R
                //    newInvocationExpression = node.WithExpression(logEnd);
                //}
                //catch (Exception ex)
                //{

                //}
                //try
                //{
                //    newBeforeNodes.Add(node);
                //    newBeforeNodes.Add(logEnd);
                //    newInvocationExpression = node.ReplaceNode(node.Parent, newBeforeNodes);

                //    //newInvocationExpression = node.InsertNodesAfter(node, newAfterNodes);
                //}
                //catch (Exception ex)
                //{

                //}

                //newInvocationExpression = newInvocationExpression.InsertNodesAfter(node, newAfterNodes);


                RecordMatchAndContext(node, node.ToString());
                //Messages.AppendLine(string.Format("Found: >{0}<\n>{1}<\nLocation: >{2}< \nstart:({3}) end:({4})", 
                //    expression.ToString(), 
                //    node.ToFullString(),
                //    location.ToString(),
                //    startLine, endLine));
                //PerformedReplacement = true;
            }
            else
            {
                newInvocationExpression = node;
            }

            return base.VisitInvocationExpression(newInvocationExpression);
        }

        private LocalDeclarationStatementSyntax GetDeclareString(string variableName)
        {
            try
            {
                SyntaxTokenList dimKeyword = GetDimKeyword2();

                var ptn = SyntaxFactory.ParseTypeName("Dim");

                //SyntaxToken asKeyword = GetAsKeyword();
                //SyntaxList asKeywodList = GetAsKeyword2();

                var stringType = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword));

                var int16Type = SyntaxFactory.IdentifierName("Int16");
                var int16Type2 = SyntaxFactory.IdentifierName("System.Int16");



                var declarators = new Microsoft.CodeAnalysis.SeparatedSyntaxList<VariableDeclaratorSyntax>();

                var modifiedIdentifer = SyntaxFactory.ModifiedIdentifier(variableName);
                var simpleAsClause = SyntaxFactory.SimpleAsClause(stringType);
                //var simpleAsClause2 = SyntaxFactory.SimpleAsClause();

                VariableDeclaratorSyntax declaratorSyntax = SyntaxFactory.VariableDeclarator(modifiedIdentifer);
                return SyntaxFactory.LocalDeclarationStatement(dimKeyword, declarators);
            }
            catch (Exception)
            {
                
            }

            return null;
        }

        LocalDeclarationStatementSyntax GetLogStartSyntax()
        {
            try
            {
                //SyntaxToken dimKeyword = GetDimKeyword();
                //SyntaxTokenList dimKeyword2 = GetDimKeyword2();


                //var simpleName = SyntaxFactory.IdentifierName("foo1");

                //var modifiedIdentifer = SyntaxFactory.ModifiedIdentifier("foo2");

                //var declarators = new Microsoft.CodeAnalysis.SeparatedSyntaxList<VariableDeclaratorSyntax>();
                //var declarators2 = new Microsoft.CodeAnalysis.SeparatedSyntaxList<ModifiedIdentifierSyntax>();


                ////var variable0 = SyntaxFactory.VariableDeclarator(simpleName);

                //var variable1 = SyntaxFactory.VariableDeclarator(modifiedIdentifer);

                //var variable2 = SyntaxFactory.VariableDeclarator(declarators2);


                //declarators = declarators.Add(variable1);

                //declarators = declarators.Add(variable1);

                //var memberAccessEx = SyntaxFactory.SimpleMemberAccessExpression(simpleName);

                string logStatement = @"
    Dim dbTicks as Long = Log.Debug2(""ExecutionStart"", LOG_APPNAME)";

                //LocalDeclarationStatementSyntax logStartSyntax2 = SyntaxFactory.LocalDeclarationStatement(dimKeyword2, declarators);
                var logStartSyntax = SyntaxFactory.ParseExecutableStatement(logStatement);
                var logStartSyntaxA = (LocalDeclarationStatementSyntax)logStartSyntax;
                return logStartSyntaxA;
            }
            catch (Exception)
            {
                
            }

            return null;
        }

        private SyntaxToken GetAsKeyword()
        {
            return SyntaxFactory.Token(SyntaxKind.AsKeyword).WithTrailingTrivia(SyntaxTriviaList.Create(SyntaxFactory.Space));
        }



        private SyntaxToken GetDimKeyword()
        {
            return SyntaxFactory.Token(SyntaxKind.DimKeyword).WithTrailingTrivia(SyntaxTriviaList.Create(SyntaxFactory.Space));
        }

        private SyntaxTokenList GetDimKeyword2()
        {
            return SyntaxFactory.TokenList(GetDimKeyword());
        }

        InvocationExpressionSyntax GetLogEndSyntax()
        {
            //Microsoft.CodeAnalysis.SyntaxTokenList modifiers = new Microsoft.CodeAnalysis.SyntaxTokenList();
            //var openParen = SyntaxFactory.Token(SyntaxKind.OpenParenToken);
            //var closeParen = SyntaxFactory.Token(SyntaxKind.CloseParenToken);
            //var commaToken = SyntaxFactory.Token(SyntaxKind.CommaToken);
            //var formatString = SyntaxFactory.Token(SyntaxKind.StringLiteralToken, "Execution End: strSQL:({0})");

            //var stringLiteral = SyntaxFactory.StringLiteralExpression(formatString);
            //var strSQL = SyntaxFactory.IdentifierName("strSQL");

            //var arg0 = SyntaxFactory.SimpleArgument(stringLiteral);
            //var arg1 = SyntaxFactory.SimpleArgument(strSQL);
            

            //var log_Id = SyntaxFactory.IdentifierName("Log");
            //var debug2_Id = SyntaxFactory.IdentifierName("Debug2");

            //var nothingExpression = GetNothingExpression();
            ////var booleanExpression = GetDefaultTypeExpression(SpecialType.System_Boolean);
            //var fullName = SyntaxFactory.QualifiedName(log_Id, debug2_Id);

            //var stringFormat = SyntaxFactory.QualifiedName(
            //    SyntaxFactory.IdentifierName("String"),
            //    SyntaxFactory.IdentifierName("Format"));

            //var separatedSyntaxList = new SeparatedSyntaxList<ArgumentSyntax>();

            //separatedSyntaxList.Add(arg0);
            //separatedSyntaxList = separatedSyntaxList.Add(arg1);

            //var stringFormatArgs = SyntaxFactory.ArgumentList(separatedSyntaxList);

            //InvocationExpressionSyntax callStringFormat = SyntaxFactory.InvocationExpression(stringFormat, stringFormatArgs);
            //TypeSyntax foo;
            ////var defaultExpression = GetDefaultValueExpression
            ////var expressionSyntax = SyntaxFactory.ExpressionStatement()
            ////var argumentList = SyntaxFactory.SimpleArgument(expressionSyntax);

            ////var dot = SyntaxKind.DotToken;
            ////var memberAccessExpression = SyntaxFactory.MemberAccessExpression(
            ////    SyntaxKind.SimpleMemberAccessExpression);

            ////var memberAccessExpressionC = Microsoft.CodeAnalysis.CSharp.SyntaxFactory.MemberAccessExpression()

            ////Microsoft.CodeAnalysis.SeparatedSyntaxList<VariableDeclaratorSyntax> declarators = new Microsoft.CodeAnalysis.SeparatedSyntaxList<VariableDeclaratorSyntax>();

            //var argumentList = SyntaxFactory.ArgumentList();

            //InvocationExpressionSyntax logEndSyntax2 = SyntaxFactory.InvocationExpression(fullName, argumentList);

            string logStatement = @"
Log.Debug2(String.Format(""Execution End: strSQL:({0})"", strSQL), LOG_APPNAME, dbTicks)
";

            var logEndSyntaxA = SyntaxFactory.ParseExpression(logStatement);
            var logEndSyntax = (InvocationExpressionSyntax)logEndSyntaxA;

            return logEndSyntax;
        }

        ArgumentSyntax GetArgumentExpression(ParameterSyntax parameter)
        {
            var identifier = SyntaxFactory.IdentifierName(parameter.Identifier.Identifier);
            return SyntaxFactory.SimpleArgument(identifier);
        }

        ExpressionSyntax GetDefaultTypeExpression(TypeSyntax type)
        {
            return SyntaxFactory.CTypeExpression(GetNothingExpression(), type);
        }

        ExpressionSyntax GetNothingExpression()
        {
            return SyntaxFactory.NothingLiteralExpression(SyntaxFactory.Token(SyntaxKind.NothingKeyword));
        }
    }
}
