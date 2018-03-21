//// Unit test template
//// 
//// - Copy to appropriate location
//// - Rename the file, usually [diagnostic name (w/o "Analyzer")]Test
//// - Uncomment entire file (Ctl-A, Ctl-K-U)
//// - Find/Replace xName with diagnostic name (w/o "Analyzer")
////       - If using non-standard naming, you may need more work here
//// - Change the TokenShouldTrigger override
//// - Review the severity and indent
//// - Delete these comment

//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CodeFixes;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.Diagnostics;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;

//namespace TeamDiagnostics.Test
//{
//   [TestClass]
//   public class xNameTest : CustomCodeFixVerifier
//   {
//      protected override CodeFixProvider GetCSharpCodeFixProvider()
//         => new xNameCodeFix();
//      protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
//         => new xNameAnalyzer();
//      protected override string GetDiagnosticId()
//         => xNameAnalyzer.DiagnosticId;
//      protected override DiagnosticSeverity GetSeverity()
//         => DiagnosticSeverity.Warning;
//      public override int IndentSpaces => 3;

//      protected override string GetBeforeDirectory()
//         => @"..\..\..\TeamDiagnostics.TestSource.Before\Design\xName";
//      protected override string GetAfterDirectory()
//         => @"..\..\..\TeamDiagnostics.TestSource.After\Design\xName";

//      protected override string CreateMessage(SyntaxToken token)
//         => String.Format(xNameAnalyzer.MessageFormat.ToString());

//      protected override bool TokenShouldTrigger(SyntaxToken token)
//         => token.Kind() == SyntaxKind.CatchKeyword;

//      [TestMethod]
//      public void xName_Empty_string_should_not_give_diagnostic()
//      { VerifyCSharpDiagnostic(""); }

//      [TestMethod]
//      public void xName_NonTriggering_code_should_not_give_diagnostic()
//      { VerifyNoDiagnosticForFiles(); }

//      [TestMethod]
//      public void xName_Triggering_code_should_give_diagnostics()
//      { VerifyDiagnosticForFiles(); }

//      [TestMethod]
//      public void xName_Triggering_code_should_give_code_fix()
//      { VerifyCodeFixesForDirectories(); }

//      [TestMethod]
//      public void xName_Triggering_code_for_specified_files_should_give_code_fix()
//      {
//         // Use this test during development/debugging to trigger just one file 
//         // (add files to list just as needed, and delete later to avoid dupe running)
//         var files = new List<string>()
//         {
//         };
//         VerifyCodeFixForSpecificFiles(files, 0);
//      }

//      [TestMethod]
//      public void xName_NonTriggering_code_for_specified_files_should_not_give_daignostic()
//      {
//         // Use this test during development/debugging to trigger just one file 
//         // (add files to list just as needed, and delete later to avoid dupe running)
//         var files = new List<string>()
//         {
//         };
//         VerifyNoDiagnoistcForSpecificFiles(files);
//      }
//   }
//}