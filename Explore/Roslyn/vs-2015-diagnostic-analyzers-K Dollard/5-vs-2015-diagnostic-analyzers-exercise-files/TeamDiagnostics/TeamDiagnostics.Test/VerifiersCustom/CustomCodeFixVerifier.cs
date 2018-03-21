using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TestHelper;

namespace TeamDiagnostics.Test
{
   public abstract class CustomCodeFixVerifier : CodeFixVerifier
   {
      protected abstract string GetDiagnosticId();
      protected abstract DiagnosticSeverity GetSeverity();
      protected abstract string GetBeforeDirectory();
      protected abstract string GetAfterDirectory();
      protected abstract bool TokenShouldTrigger(SyntaxToken token);
      protected abstract string CreateMessage(SyntaxToken token);
      public virtual int IndentSpaces { get; } = 0;
      public virtual Location GetSquiggleLocation(SyntaxToken token)
            => token.GetLocation();

      protected string NonTriggeringBeforeDir
            => Path.Combine(GetBeforeDirectory(), "NonTriggering");
      protected string TriggeringBeforeDir
            => Path.Combine(GetBeforeDirectory(), "Triggering");

      protected void VerifyNoDiagnosticForFiles()
      {
         var files = Directory.GetFiles(NonTriggeringBeforeDir);
         foreach (var fileName in files)
         {
            VerifyNoDiagnosticForFile(fileName);
         }
      }

      protected void VerifyNoDiagnosticForFile(string fileName)
      {
         var test = File.ReadAllText(fileName);
         VerifyCSharpDiagnostic(test);
      }

      protected void VerifyDiagnosticForFiles()
      {
         var files = Directory.GetFiles(TriggeringBeforeDir);
         foreach (var fileName in files)
         {
            VerifyDiagnosticsForFile(fileName);
         }
      }

      protected void VerifyDiagnosticsForFile(string fileName)
      {
         var test = File.ReadAllText(fileName);
         var tree = SyntaxFactory.ParseCompilationUnit(test);
         var syntaxTokens = tree.DescendantTokens().Where(x => TokenShouldTrigger(x));
         var expected = GetExpectedResults(syntaxTokens);

         VerifyCSharpDiagnostic(test, expected.ToArray());
      }

      private IEnumerable<DiagnosticResult> GetExpectedResults(IEnumerable<SyntaxToken> syntaxTokens)
      {
         var expected = new List<DiagnosticResult>();
         foreach (var syntaxToken in syntaxTokens)
         {
            var location = GetSquiggleLocation(syntaxToken).GetLineSpan().StartLinePosition;

            var result = new DiagnosticResult
            {
               Id = GetDiagnosticId(),
               Message = CreateMessage(syntaxToken),
               Severity = GetSeverity(),
               Locations =
                    new[] {                            new DiagnosticResultLocation(
                       "Test0.cs",  location.Line + 1, location.Character+1)                        }
            };
            expected.Add(result);
         }
         return expected;
      }

      protected void VerifyCodeFixesForDirectories()
      {
         var codeFixDirs = Directory.GetDirectories(GetAfterDirectory());
         foreach (var codeFixFullDir in codeFixDirs)
         {
            VerifyCodeFixForFiles(codeFixFullDir, codeFixDirs.Skip(1).Any());
         }
      }

      protected void VerifyCodeFixForFiles(
                  string codeFixFullDir,
                  bool multipleFixes = false)
      {
         var codeFixDir = Path.GetFileName(codeFixFullDir);
         int ordinal;
         int.TryParse(codeFixDir.Substring(0, 1), out ordinal);

         var files = Directory.GetFiles(TriggeringBeforeDir);
         foreach (var beforeFullFileName in files)
         {
            VerifyCodeFixForFile(beforeFullFileName, codeFixFullDir, ordinal, multipleFixes);
         }
      }

      protected void VerifyCodeFixForFile(string beforeFullFileName, string codeFixDir, int ordinal, bool multipleFixes)
      {
         var test = File.ReadAllText(beforeFullFileName);
         var fileName = Path.GetFileName(beforeFullFileName);
         var fixTest = File.ReadAllText(Path.Combine(codeFixDir, fileName));

         CustomHelpers.MakeCommonNamespaces(ref test, ref fixTest);

         if (multipleFixes)
         {
            VerifyCSharpFix(test, fixTest, ordinal, addWorkspaceOptions: AddWorkspaceOptions(), allowNewCompilerDiagnostics: true);
         }
         else
         {
            VerifyCSharpFix(test, fixTest, addWorkspaceOptions: AddWorkspaceOptions(), allowNewCompilerDiagnostics: true);
         }
      }

      protected void VerifyCodeFixForSpecificFiles(List<string> files, int ordinal, bool multipleFixes = false)
      {
         if (files == null || !files.Any()) { return; }
         var directory = Directory.GetDirectories(GetAfterDirectory(), ordinal + "*").First();
         foreach (var file in files)
         {
            var fileName = Path.Combine(TriggeringBeforeDir, file);
            VerifyDiagnosticsForFile(fileName);
            VerifyCodeFixForFile(fileName, directory, ordinal, multipleFixes);
         }
      }

      protected void VerifyNoDiagnoistcForSpecificFiles(List<string> files)
      {
         if (files == null || !files.Any()) { return; }
         foreach (var file in files)
         {
            var fileName = Path.Combine(NonTriggeringBeforeDir, file);
            VerifyNoDiagnosticForFile(fileName);
         }
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



   }
}
