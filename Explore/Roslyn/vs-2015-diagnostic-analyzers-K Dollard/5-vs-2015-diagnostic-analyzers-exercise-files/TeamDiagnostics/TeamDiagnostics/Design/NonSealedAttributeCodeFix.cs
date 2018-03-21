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
   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(NonSealedAttributeCodeFix)), Shared]
   public class NonSealedAttributeCodeFix : CodeFixProvider
   {
      public sealed override ImmutableArray<string> FixableDiagnosticIds
         => ImmutableArray.Create(NonSealedAttributeAnalyzer.DiagnosticId);

      public sealed override FixAllProvider GetFixAllProvider()
         => WellKnownFixAllProviders.BatchFixer;

      internal static readonly LocalizableString FixMessage =
         new LocalizableResourceString(nameof(Resources.NonSealedAttributeCodeFixMessage),
                                       Resources.ResourceManager, typeof(Resources));

      public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         var diagnostic = context.Diagnostics.First();
         var declaration =
               await CodeFixHelpers.FirstAncestorNodeFromCodeFixContext<ClassDeclarationSyntax>(
                  context, diagnostic).ConfigureAwait(false);

         var message = string.Format(FixMessage.ToString()); // pass params as needed

         context.RegisterCodeFix(
             CodeAction.Create(message, c => AddSealedKeyword(context.Document, declaration, c)),
             diagnostic);
      }

      private async Task<Document> AddSealedKeyword(Document document, 
            ClassDeclarationSyntax declaration, CancellationToken cancellationToken)
      {
         var newDeclaration = declaration.AddModifiers(
                  SyntaxFactory.Token(SyntaxKind.SealedKeyword));
         return await CodeFixHelpers.ReplaceNode(document,
                                          declaration, newDeclaration,
                                          cancellationToken)
                                    .ConfigureAwait(false);
      }
   }
}