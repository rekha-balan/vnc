using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace TeamDiagnostics
{
   [ExportCodeRefactoringProvider(LanguageNames.CSharp, 
            Name = nameof(LocalDeclarationTypeStyleRefactoring))]
   public class LocalDeclarationTypeStyleRefactoring : CodeRefactoringProvider
   {
      public async sealed override Task ComputeRefactoringsAsync(CodeRefactoringContext context)
      {
         var localDeclaration = await NodeOfTypeFromContext<LocalDeclarationStatementSyntax>(context);
         if (localDeclaration?.Declaration?.Type == null) { return; }
         var typeSyntax = localDeclaration.Declaration.Type;
         var semanticModel = await context.Document.GetSemanticModelAsync(context.CancellationToken);
         if (!VarHasMeaningAtLocation(semanticModel, localDeclaration))
         {
            if (typeSyntax.IsVar)
            {
               context.RegisterRefactoring(CodeAction.Create("Change var to named alias {0}",
                        c => ChangeToExplicitType(localDeclaration, context.Document, 
                              context.CancellationToken)));
            }
            else if (localDeclaration.Declaration.Variables.Count == 1)
            {
               var variable = localDeclaration.Declaration.Variables[0];
               if (variable.Initializer != null)
               {
                  var typeSymbol = semanticModel.GetTypeInfo(typeSyntax).Type;
                  if (typeSymbol == null) { return; }
                  if (!ConversionDone(semanticModel, typeSymbol, variable)
                        && !IsInitializedToNull(variable))
                  {
                     context.RegisterRefactoring(CodeAction.Create("Change {0} to var",
                              c => ChangeToVar(localDeclaration, context.Document, 
                                       context.CancellationToken)));
                  }
               }
            }
         }
      }

      private async Task<Document> ChangeToExplicitType(
               LocalDeclarationStatementSyntax localDeclaration, 
               Document document, CancellationToken cancellationToken)
      {
         var oldType = localDeclaration.Declaration.Type;
         var semanticModel = await document.GetSemanticModelAsync();
         var typeSymbol = semanticModel.GetTypeInfo(oldType).Type;
         var displayString = typeSymbol.ToMinimalDisplayString(semanticModel,
                                 localDeclaration.GetLocation().SourceSpan.Start);
         var newType = SyntaxFactory.IdentifierName(displayString);
         return await CodeFixHelpers.ReplaceNode(document, oldType, newType, cancellationToken, moveTrivia: true);
      }

      private async Task<Document> ChangeToVar(LocalDeclarationStatementSyntax localDeclaration, Document document, CancellationToken cancellationToken)
      {
         var oldType = localDeclaration.Declaration.Type;
         var newType = SyntaxFactory.IdentifierName("var");
         return await CodeFixHelpers.ReplaceNode(document, oldType, newType, cancellationToken);
      }

      private bool IsInitializedToNull(VariableDeclaratorSyntax variable)
               => (variable.Initializer.Value.Kind() == SyntaxKind.NullLiteralExpression);

      private bool ConversionDone(SemanticModel semanticModel, ITypeSymbol typeSymbol, 
               VariableDeclaratorSyntax variable)
      {
         var conversion = semanticModel.ClassifyConversion(variable.Initializer.Value, typeSymbol);
         return (!conversion.Exists || !conversion.IsIdentity);
      }

      private bool VarHasMeaningAtLocation(SemanticModel semanticModel, LocalDeclarationStatementSyntax localDeclaration)
      {
         var position = localDeclaration.GetLocation().SourceSpan.Start;
         var available = semanticModel.LookupNamespacesAndTypes(position, name: "var");
         return (available.Any());
      }

      private async static Task<T> NodeOfTypeFromContext<T>(CodeRefactoringContext context)
         where T : SyntaxNode
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);
         var node = root.FindNode(context.Span);

         return node.FirstAncestorOrSelf<T>();
      }
   }
}