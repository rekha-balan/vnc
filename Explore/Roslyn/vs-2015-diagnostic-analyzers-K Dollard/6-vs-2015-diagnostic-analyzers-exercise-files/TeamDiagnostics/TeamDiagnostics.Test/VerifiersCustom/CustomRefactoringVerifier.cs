using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TestHelper;

namespace TeamDiagnostics.Test
{
   public abstract class CustomRefactoringVerifier : DiagnosticVerifier
   {
      protected abstract string GetBeforeDirectory();
      protected abstract string GetAfterDirectory();
      protected abstract IEnumerable<Location> GetLocations(SyntaxNode root);
      protected abstract CodeRefactoringProvider GetCSharpRefactoringProvider();

      public virtual int IndentSpaces { get; } = 0;

      protected string NonTriggeringBeforeDir => Path.Combine(GetBeforeDirectory(), "NonTriggering");
      protected string TriggeringBeforeDir => Path.Combine(GetBeforeDirectory(), "Triggering");

      protected void VerifyNoRefactoringForFiles()
      {
         var files = Directory.GetFiles(NonTriggeringBeforeDir);
         foreach (var fileName in files)
         {
            var document = DocumentFromFileName(fileName);
            var root = document.GetSyntaxRootAsync().Result;
            var triggeredCount = GetTriggeredCount(root, document);
            Assert.AreEqual(0, triggeredCount, $"Expected no triggers in file {fileName}; {triggeredCount} triggers found");
         }
      }

      protected void VerifyAllOfTypeSingleRefactoring<T>()
            where T : SyntaxNode
      {
         var files = Directory.GetFiles(TriggeringBeforeDir);
         foreach (var fileName in files)
         {
            var document = DocumentFromFileName(fileName);
            var root = document.GetSyntaxRootAsync().Result;
            var expectedCount = root.DescendantNodes()
                                 .OfType<T>()
                                 .Count();
            var triggeredCount = GetTriggeredCount(root, document);
            Assert.AreEqual(expectedCount, triggeredCount,
                   $"Expected {expectedCount} triggers in file {fileName}; {triggeredCount} triggers found");
         }
      }

      protected void VerifyRefactoringResults()
      {
         var files = Directory.GetFiles(TriggeringBeforeDir);
         foreach (var filePath in files)
         {
            var fileName = Path.GetFileName(filePath);
            fileName = Path.Combine(GetAfterDirectory(), fileName);
            var expectedSource = File.ReadAllText(fileName);

            var document = DocumentFromFileName(filePath);
            var newDocument = MakeAllFixes(document);

            var actual = GetStringFromDocument(newDocument);

            CustomHelpers.MakeCommonNamespaces(ref actual, ref expectedSource);

            Assert.AreEqual(expectedSource, actual);
         }
      }

      private Document MakeAllFixes(Document document)
      {
         var root = document.GetSyntaxRootAsync().Result;
         var locations = GetLocations(root);
         var provider = GetCSharpRefactoringProvider();
         foreach (var location in locations.Reverse())
         {
            CodeAction codeAction = null;
            var context = new CodeRefactoringContext(document, location.SourceSpan,
               x => codeAction = x,
               default(CancellationToken));
            provider.ComputeRefactoringsAsync(context).Wait();
            document = ApplyChanges(codeAction, document);
         }
         return document;
      }

      private Document ApplyChanges(CodeAction codeAction, Document document)
      {
         if (codeAction == null) { return document; }
         var operations = codeAction.GetOperationsAsync(CancellationToken.None).Result;
         foreach (var operation in operations.OfType<ApplyChangesOperation>())
         {
            var solution = operation.ChangedSolution;
            document = solution.GetDocument(document.Id);
         }
         return document;
      }

      private int GetTriggeredCount(SyntaxNode root, Document document)
      {
         var locations = GetLocations(root);
         var triggeredCount = 0;
         var provider = GetCSharpRefactoringProvider();
         foreach (var location in locations)
         {
            var context = new CodeRefactoringContext(document, location.SourceSpan,
               x => triggeredCount++,
               default(CancellationToken));
            provider.ComputeRefactoringsAsync(context);
         }
         return triggeredCount;
      }

      protected Document DocumentFromFileName(string fileName)
      {
         var source = File.ReadAllText(fileName);
         var document = CreateDocument(source);
         AddWorkspaceOptions()?.Invoke(document.Project.Solution.Workspace);
         return document;
      }

      protected virtual Action<Workspace> AddWorkspaceOptions()
      {
         return IndentSpaces == 4 || IndentSpaces == 0
                                         ? (Action<Workspace>)null
                                         : x => x.Options = x.Options.WithChangedOption(
                                                   FormattingOptions.IndentationSize,
                                                   LanguageNames.CSharp,
                                                   IndentSpaces);
      }

      /// <summary>
      /// Same as the one in CodeFixVerifier.Helper but avoids changing the scope 
      /// in that file.
      /// </summary>
      /// <param name="document"></param>
      /// <returns></returns>
      protected static string GetStringFromDocument(Document document)
      {
         var simplifiedDoc = Simplifier.ReduceAsync(document, Simplifier.Annotation).Result;
         var root = simplifiedDoc.GetSyntaxRootAsync().Result;
         root = Formatter.Format(root, Formatter.Annotation, simplifiedDoc.Project.Solution.Workspace);
         return root.GetText().ToString();
      }


   }
}
