using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TeamDiagnostics.Test
{
   [TestClass]
   public class NonSealedAttributeTest : CustomCodeFixVerifier
   {
      protected override CodeFixProvider GetCSharpCodeFixProvider()
         => new NonSealedAttributeCodeFix();
      protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
         => new NonSealedAttributeAnalyzer();
      protected override string GetDiagnosticId()
         => NonSealedAttributeAnalyzer.DiagnosticId;
      protected override DiagnosticSeverity GetSeverity()
         => DiagnosticSeverity.Warning;
      public override int IndentSpaces => 3;

      protected override string GetBeforeDirectory()
         => @"..\..\..\TeamDiagnostics.TestSource.Before\Design\NonSealedAttribute";
      protected override string GetAfterDirectory()
         => @"..\..\..\TeamDiagnostics.TestSource.After\Design\NonSealedAttribute";

      protected override string CreateMessage(SyntaxToken token)
         => String.Format(NonSealedAttributeAnalyzer.MessageFormat.ToString(), token);

      protected override bool TokenShouldTrigger(SyntaxToken token)
         => token.Kind() == SyntaxKind.IdentifierToken
                        && token.Parent is ClassDeclarationSyntax
                        && token.Text.Contains("Attribute")
                        && !((ClassDeclarationSyntax)(token.Parent))
                                 .Modifiers
                                 .Any(y => y.Kind() == SyntaxKind.AbstractKeyword);   

      [TestMethod]
      public void NonSealedAttribute_Empty_string_should_not_give_diagnostic()
      { VerifyCSharpDiagnostic(""); }

      [TestMethod]
      public void NonSealedAttribute_NonTriggering_code_should_not_give_diagnostic()
      { VerifyNoDiagnosticForFiles(); }

      [TestMethod]
      public void NonSealedAttribute_Triggering_code_should_give_diagnostics()
      { VerifyDiagnosticForFiles(); }

      [TestMethod]
      public void NonSealedAttribute_Triggering_code_should_give_code_fix()
      { VerifyCodeFixesForDirectories(); }

      [TestMethod]
      public void NonSealedAttribute_Triggering_code_for_specified_files_should_give_code_fix()
      {
         // Use this test during development/debugging to trigger just one file 
         // (add files to list just as needed, and delete later to avoid dupe running)
         var files = new List<string>()
         {
         };
         VerifyCodeFixForSpecificFiles(files, 0);
      }

      [TestMethod]
      public void NonSealedAttribute_NonTriggering_code_for_specified_files_should_not_give_daignostic()
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