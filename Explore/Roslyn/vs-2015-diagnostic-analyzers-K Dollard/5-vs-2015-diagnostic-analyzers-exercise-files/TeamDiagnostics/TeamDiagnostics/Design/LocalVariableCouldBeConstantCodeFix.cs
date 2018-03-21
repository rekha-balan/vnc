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
   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(LocalVariableCouldBeConstantCodeFix)), Shared]
   public class LocalVariableCouldBeConstantCodeFix : CodeFixProvider
   {
      public sealed override ImmutableArray<string> FixableDiagnosticIds
         => ImmutableArray.Create(LocalVariableCouldBeConstantAnalyzer.DiagnosticId);

      public sealed override FixAllProvider GetFixAllProvider()
         => WellKnownFixAllProviders.BatchFixer;

      internal static readonly LocalizableString FixMessage =
         new LocalizableResourceString(nameof(Resources.LocalVariableCouldBeConstantCodeFixMessage),
                                       Resources.ResourceManager, typeof(Resources));

      public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         var diagnostic = context.Diagnostics.First();
         var declaration =
               await CodeFixHelpers
                  .FirstAncestorNodeFromCodeFixContext<LocalDeclarationStatementSyntax >(
                        context, diagnostic).ConfigureAwait(false);

         if (declaration?.Declaration?.Variables == null) { return; }
         var variableList = Helpers.FormatList(declaration.Declaration.Variables);
         var message = string.Format(FixMessage.ToString(), variableList ); 

         context.RegisterCodeFix(
             CodeAction.Create(message, c => AddConstKeyword(context.Document, declaration, c)),
             diagnostic);
      }

      private async Task<Document> AddConstKeyword(Document document, 
            LocalDeclarationStatementSyntax localDeclaration, 
            CancellationToken cancellationToken)
      {
         var newLocalDeclaration = CodeFixHelpers.UpdateNode(localDeclaration,
                      x => x.AddModifiers(SyntaxFactory.Token(SyntaxKind.ConstKeyword)));

         var declaration = localDeclaration.Declaration;
         if (declaration.Type.IsVar)
         {
            var typeSymbol = await Helpers.GetTypeSymbol(
                     document, declaration.Type, cancellationToken).ConfigureAwait(false);
            if (typeSymbol.Name != "var")
            {
               var newTypeName = SyntaxFactory.ParseTypeName(typeSymbol.ToDisplayString());
               var newDeclaration = CodeFixHelpers.UpdateNode(declaration,
                                    x => x.WithType(newTypeName));
               newLocalDeclaration = CodeFixHelpers.UpdateNode(newLocalDeclaration,
                                    x => x.WithDeclaration(newDeclaration));
            }
         }
         return await CodeFixHelpers.ReplaceNode(document,
                  localDeclaration, newLocalDeclaration, cancellationToken).ConfigureAwait(false);
      }
   }
}