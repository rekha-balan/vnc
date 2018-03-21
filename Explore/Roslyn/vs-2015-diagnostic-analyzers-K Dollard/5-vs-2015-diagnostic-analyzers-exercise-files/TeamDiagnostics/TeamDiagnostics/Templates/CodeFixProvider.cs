//// Template for new code fixes, updated from the default
//// Copy this file and the analyzer file into the appropriate category directory 
//// and make the following changes:
////
//// - The class name and the class name in the attribute 
//// - The message resource name to the format [CodeFixName][n]Message where n is the number 
////            of the fix when there are multiple fixes
////     - Add this resource to the resources file
//// - Fixable diagnostic id
//// - The generic argument in the call to the FirstAncestorNodeFromCodeFixContext method in RegisterCodeFixesAsync
//// - Add any additional work required to create registration, including formatting the message
//// - Rename the "MakeUpperAsync" method to a name that makes sense for your analyzer
//// - Add your code fix code the "MakeUpperAsync" method

//using System;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using System.Composition;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CodeFixes;
//using Microsoft.CodeAnalysis.CodeActions;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Microsoft.CodeAnalysis.Rename;
//using Microsoft.CodeAnalysis.Text;

//namespace TeamDiagnostics
//{
//   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(xNameCodeFix)), Shared]
//   public class xNameCodeFix : CodeFixProvider
//   {
//      public sealed override ImmutableArray<string> FixableDiagnosticIds
//         => ImmutableArray.Create(xNameCodeFixAnalyzer.DiagnosticId); 

//      public sealed override FixAllProvider GetFixAllProvider()
//         => WellKnownFixAllProviders.BatchFixer;

//      internal static readonly LocalizableString FixMessage =
//         new LocalizableResourceString(nameof(Resources.xNameCodeFixMessage),
//                                       Resources.ResourceManager, typeof(Resources));

//      public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
//      {
//         var diagnostic = context.Diagnostics.First();
//         var declaration =
//               await CodeFixHelpers.FirstAncestorNodeFromCodeFixContext<TypeDeclarationSyntax>(
//                  context, diagnostic).ConfigureAwait(false);

//         var message = string.Format(FixMessage.ToString()); // pass params as needed

//         context.RegisterCodeFix(
//             CodeAction.Create(message, c => MakeUppercaseAsync(context.Document, declaration, c)),
//             diagnostic);
//      }


//      private async Task<Solution> MakeUppercaseAsync(Document document, TypeDeclarationSyntax typeDecl, CancellationToken cancellationToken)
//      {

//      }
//   }
//}