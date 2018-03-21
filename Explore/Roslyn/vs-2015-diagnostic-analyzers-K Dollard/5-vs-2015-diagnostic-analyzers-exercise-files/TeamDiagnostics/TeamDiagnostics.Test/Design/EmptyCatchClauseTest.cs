// Unit test template
// 
// - Copy to appropriate location
// - Rename the file, usually [diagnostic name (w/o "Analyzer")]Test
// - Uncomment entire file (Ctl-A, Ctl-K-U)
// - Find/Replace xName with diagnostic name (w/o "Analyzer")
//       - If using non-standard namng, you may need more work here
// - Change the TokenShouldTrigger override
// - Review the severity and indent

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TeamDiagnostics.Test
{
   [TestClass]
   public class EmptyCatchClauseTest : CustomCodeFixVerifier
   {
      protected override CodeFixProvider GetCSharpCodeFixProvider()
         => new EmptyCatchClauseCodeFix();
      protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
         => new EmptyCatchClauseAnalyzer();
      protected override string GetDiagnosticId()
         => EmptyCatchClauseAnalyzer.DiagnosticId;
      protected override DiagnosticSeverity GetSeverity()
         => DiagnosticSeverity.Warning;
      public override int IndentSpaces => 3;

      protected override string GetBeforeDirectory()
         => @"..\..\..\TeamDiagnostics.TestSource.Before\Design\EmptyCatchClause";
      protected override string GetAfterDirectory()
         => @"..\..\..\TeamDiagnostics.TestSource.After\Design\EmptyCatchClause";

      protected override string CreateMessage(SyntaxToken token)
         => String.Format(EmptyCatchClauseAnalyzer.MessageFormat.ToString());

      protected override bool TokenShouldTrigger(SyntaxToken token)
         => token.Kind() == SyntaxKind.CatchKeyword;



      [TestMethod]
      public void EmptyCatchClause_Empty_string_should_not_give_diagnostic()
      { VerifyCSharpDiagnostic(""); }

      [TestMethod]
      public void EmptyCatchClause_NonTriggering_code_should_not_give_diagnostic()
      { VerifyNoDiagnosticForFiles(); }

      [TestMethod]
      public void EmptyCatchClause_Triggering_code_should_give_diagnostics()
      { VerifyDiagnosticForFiles(); }

      [TestMethod]
      public void EmptyCatchClause_Triggering_code_should_give_code_fix()
      { VerifyCodeFixesForDirectories(); }

      [TestMethod]
      public void EmptyCatchClause_Triggering_code_for_specified_files_should_give_code_fix()
      {
         // Use this test during development/debugging to trigger just one file 
         // (add files to list just as needed, and delete later to avoid dupe running)
         var files = new List<string>()
         {
         };
         VerifyCodeFixForSpecificFiles(files, 0);
      }

      [TestMethod]
      public void EmptyCatchClause_NonTriggering_code_for_specified_files_should_not_give_daignostic()
      {
         // Use this test during development/debugging to trigger just one file 
         // (add files to list just as needed, and delete later to avoid dupe running)
         var files = new List<string>()
         {
         };
         VerifyNoDiagnoistcForSpecificFiles(files);
      }
   }
}