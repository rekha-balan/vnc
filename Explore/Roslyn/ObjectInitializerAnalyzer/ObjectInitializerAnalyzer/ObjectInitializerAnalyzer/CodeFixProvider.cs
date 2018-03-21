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

namespace ObjectInitializerAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ObjectInitializerAnalyzerCodeFixProvider)), Shared]
    public class ObjectInitializerAnalyzerCodeFixProvider : CodeFixProvider
    {
        private const string title = "Fix OI";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(ObjectInitializerAnalyzerAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;
            var declarations = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<LocalDeclarationStatementSyntax>();

            if (declarations == null)
            {
                return;
            }

            var declaration = declarations.First();

            context.RegisterCodeFix(
                CodeAction.Create("Rewrite Setters", c => RewriteSetters(context.Document, declaration, c)),
                diagnostic);
            //var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            //// TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
            //var diagnostic = context.Diagnostics.First();
            //var diagnosticSpan = diagnostic.Location.SourceSpan;

            //// Find the type declaration identified by the diagnostic.
            //var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<TypeDeclarationSyntax>().First();

            //// Register a code action that will invoke the fix.
            //context.RegisterCodeFix(
            //    CodeAction.Create(
            //        title: title,
            //        createChangedSolution: c => MakeUppercaseAsync(context.Document, declaration, c),
            //        equivalenceKey: title),
            //    diagnostic);
        }

        private BlockSyntax GetContainingBlock(SyntaxNode node)
        {
            var block = node.Parent as BlockSyntax;
            if (block != null)
            {
                return block;
            }

            return GetContainingBlock(node.Parent);
        }

        //private async Task<Solution> MakeUppercaseAsync(Document document, TypeDeclarationSyntax typeDecl, CancellationToken cancellationToken)
        //{
        //    // Compute new uppercase name.
        //    var identifierToken = typeDecl.Identifier;
        //    var newName = identifierToken.Text.ToUpperInvariant();

        //    // Get the symbol representing the type to be renamed.
        //    var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
        //    var typeSymbol = semanticModel.GetDeclaredSymbol(typeDecl, cancellationToken);

        //    // Produce a new solution that has all references to that type renamed, including the declaration.
        //    var originalSolution = document.Project.Solution;
        //    var optionSet = originalSolution.Workspace.Options;
        //    var newSolution = await Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);

        //    // Return the new solution with the now-uppercase type name.
        //    return newSolution;
        //}

        private async Task<Document> RewriteSetters(Document document, LocalDeclarationStatementSyntax declarationWithInitializer, CancellationToken c)
        {
            var root = await document.GetSyntaxRootAsync(c).ConfigureAwait(false);
            var variableDeclarator = declarationWithInitializer.DescendantNodes().OfType<VariableDeclaratorSyntax>().First();
            string variableName = variableDeclarator.Identifier.ToString();

            var objectInitializer = declarationWithInitializer.DescendantNodes().OfType<InitializerExpressionSyntax>().First();
            var initializedProperties = new List<SyntaxNode>();
            foreach (var propInitialization in objectInitializer.Expressions)
            {
                var separatePropInitialization = SyntaxFactory.ParseStatement(variableName + "." + propInitialization + ";");
                separatePropInitialization = separatePropInitialization.WithTrailingTrivia(SyntaxFactory.Whitespace(Environment.NewLine));
                initializedProperties.Add(separatePropInitialization);
            }

            var declarationWithoutInitializer = declarationWithInitializer.RemoveNode(objectInitializer, SyntaxRemoveOptions.KeepNoTrivia);
            declarationWithoutInitializer = declarationWithoutInitializer.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));

            var objCreationExpr = declarationWithoutInitializer.DescendantNodes().OfType<ObjectCreationExpressionSyntax>().ToList()[0];
            if (objCreationExpr.ArgumentList == null)
            {
                ObjectCreationExpressionSyntax objCreationExprWithEmptyArgList = objCreationExpr.WithArgumentList(SyntaxFactory.ArgumentList());
                declarationWithoutInitializer = declarationWithoutInitializer.ReplaceNode(objCreationExpr, objCreationExprWithEmptyArgList);
            }

            var block = GetContainingBlock(declarationWithInitializer);
            var newBlock = block.TrackNodes(declarationWithInitializer);
            var refreshedObjectInitializer = newBlock.GetCurrentNode(declarationWithInitializer);
            newBlock = newBlock.InsertNodesAfter(refreshedObjectInitializer, initializedProperties).WithAdditionalAnnotations(Microsoft.CodeAnalysis.Formatting.Formatter.Annotation);
            refreshedObjectInitializer = newBlock.GetCurrentNode(declarationWithInitializer);
            newBlock = newBlock.ReplaceNode(refreshedObjectInitializer, declarationWithoutInitializer);

            var newroot = root.ReplaceNode(block, newBlock).WithAdditionalAnnotations(Microsoft.CodeAnalysis.Formatting.Formatter.Annotation);
            return document.WithSyntaxRoot(newroot);
        }
    }
}