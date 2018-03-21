using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace TeamDiagnostics
{
   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UseExpressionBodyMemberCodeFix)), Shared]
   public class UseExpressionBodyMemberCodeFix : CodeFixProvider
   {
      public sealed override ImmutableArray<string> FixableDiagnosticIds
         => ImmutableArray.Create(UseExpressionBodyMemberAnalyzer.DiagnosticId);

      public sealed override FixAllProvider GetFixAllProvider()
         => WellKnownFixAllProviders.BatchFixer;

      internal static readonly LocalizableString FixMessage =
         new LocalizableResourceString(nameof(Resources.UseExpressionBodyMemberCodeFixMessage),
                                       Resources.ResourceManager, typeof(Resources));

      public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         var diagnostic = context.Diagnostics.First();
         Func<CancellationToken, Task<Document>> action = null;
         var declaration =
               await CodeFixHelpers.FirstAncestorNodeFromCodeFixContext<MethodDeclarationSyntax>(
                  context, diagnostic).ConfigureAwait(false);

         if (declaration != null)
         { action = c => MethodToExpressionBody(context.Document, declaration, c); }
         else
         {
            var propertyDeclaration = await
               CodeFixHelpers.FirstAncestorNodeFromCodeFixContext<PropertyDeclarationSyntax>(context, diagnostic);
            if (propertyDeclaration != null)
            { action = c => PropertyToExpressionBody(context.Document, propertyDeclaration, c); }
         }

         var message = string.Format(FixMessage.ToString()); // pass params as needed

         context.RegisterCodeFix(
             CodeAction.Create(message, action),
             diagnostic);
      }

      private async Task<Document> PropertyToExpressionBody(Document document, 
         PropertyDeclarationSyntax propertyDeclaration, CancellationToken c)
      {
         if (propertyDeclaration == null
            || propertyDeclaration.AccessorList == null
            || propertyDeclaration.AccessorList.Accessors.Count() != 1
            || propertyDeclaration.AccessorList.Accessors[0].Body.Statements.Count() != 1)
         { return document; }

         var accessor = propertyDeclaration.AccessorList.Accessors[0];
         var expression = GetExpression(accessor.Body.Statements[0]);

         if (expression == null) return document;
         var lambda = SyntaxFactory.ArrowExpressionClause(expression);
         var newDeclaration = propertyDeclaration
                                 .WithAccessorList(null)
                                 .WithExpressionBody(lambda)
                                 .WithSemicolon(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
         return await CodeFixHelpers.ReplaceNode(document,
                  propertyDeclaration, newDeclaration, c);
      }

      private async Task<Document> MethodToExpressionBody(Document document, 
         MethodDeclarationSyntax declaration, CancellationToken c)
      {
         if (declaration?.Body?.Statements == null
                  || declaration.Body.Statements.Count() != 1)
         { return document; }
         var expression = GetExpression(declaration.Body.Statements[0]);
         if (expression == null) return document;
         var lambda = SyntaxFactory.ArrowExpressionClause(expression);
         var newDeclaration = declaration
                                 .WithBody(null)
                                 .WithExpressionBody(lambda)
                                 .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
         return await CodeFixHelpers.ReplaceNode(document,
                  declaration, newDeclaration, c);
      }

      private ExpressionSyntax GetExpression(StatementSyntax statementSyntax)
      {
         var exprStatement = statementSyntax as ExpressionStatementSyntax;
         if (exprStatement != null)
         {
            return exprStatement.Expression;
         }
         else
         {
            var returnStatement = statementSyntax as ReturnStatementSyntax;
            if (returnStatement == null) { return null; }
            return returnStatement.Expression;
         }
      }
   }
}