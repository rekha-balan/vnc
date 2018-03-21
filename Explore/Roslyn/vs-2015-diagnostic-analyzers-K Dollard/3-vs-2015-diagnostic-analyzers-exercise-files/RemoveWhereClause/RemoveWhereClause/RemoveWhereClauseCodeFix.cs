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

namespace RemoveWhereClause
{
   [ExportCodeFixProvider(LanguageNames.CSharp, Name =
      nameof(RemoveWhereClauseCodeFix)), Shared]
   public class RemoveWhereClauseCodeFix : CodeFixProvider
   {
      public sealed override ImmutableArray<string> FixableDiagnosticIds
      {
         get { return ImmutableArray.Create(RemoveWhereClauseAnalyzer.DiagnosticId); }
      }

      public sealed override FixAllProvider GetFixAllProvider()
      {
         return WellKnownFixAllProviders.BatchFixer;
      }

      public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         var diagnostic = context.Diagnostics.First();
         var diagnosticSpan = diagnostic.Location.SourceSpan;
         var whereInvocation = root
                           .FindToken(diagnosticSpan.Start)
                           .Parent
                           .FirstAncestorOrSelf<InvocationExpressionSyntax>();
         var nextInvocation = whereInvocation
                                 .Parent
                                 .FirstAncestorOrSelf<InvocationExpressionSyntax>();
         var methodName = Utilities.InvokedMethod(nextInvocation).ToString();
         var message = string.Format("Move filtering predicate to {0} and remove Where method call",
                           methodName);
         var fix = CodeAction.Create(message,
                           c => RemoveWhereCall(context.Document,
                                                whereInvocation, nextInvocation, c));
         context.RegisterCodeFix(fix, diagnostic);
      }

      private async Task<Document> RemoveWhereCall(Document document, 
               InvocationExpressionSyntax whereInvocation, 
               InvocationExpressionSyntax entireInvocation, 
               CancellationToken c)
      {
         var root = await document.GetSyntaxRootAsync(c);
         var whereMethod = whereInvocation.ChildNodes()
                           .OfType<MemberAccessExpressionSyntax>()
                           .FirstOrDefault();
         var nextMethod = entireInvocation.ChildNodes()
                           .OfType<MemberAccessExpressionSyntax>()
                           .FirstOrDefault();

         if (whereMethod == null || nextMethod == null) { return document; }
         var newNextMethod = SyntaxFactory.MemberAccessExpression(
                                 SyntaxKind.SimpleMemberAccessExpression,
                                 whereMethod.Expression,
                                 nextMethod.Name);
         var newInvocation = SyntaxFactory.InvocationExpression(
                                 newNextMethod, whereInvocation.ArgumentList);
         var newRoot = root.ReplaceNode(entireInvocation, newInvocation);
         var newDocument = document.WithSyntaxRoot(newRoot);
         return newDocument;
      }
   }
}