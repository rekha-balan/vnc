using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TeamDiagnostics.Test
{
   [TestClass]
   public class UseExpressionBodyMemberTest : CustomCodeFixVerifier
   {
      protected override CodeFixProvider GetCSharpCodeFixProvider()
         => new UseExpressionBodyMemberCodeFix();
      protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
         => new UseExpressionBodyMemberAnalyzer();
      protected override string GetDiagnosticId()
         => UseExpressionBodyMemberAnalyzer.DiagnosticId;
      protected override DiagnosticSeverity GetSeverity()
         => DiagnosticSeverity.Hidden;
      public override int IndentSpaces => 3;

      protected override string GetBeforeDirectory()
         => @"..\..\..\TeamDiagnostics.TestSource.Before\Style\UseExpressionBodyMember";
      protected override string GetAfterDirectory()
         => @"..\..\..\TeamDiagnostics.TestSource.After\Style\UseExpressionBodyMember";

      protected override string CreateMessage(SyntaxToken token)
         => String.Format(UseExpressionBodyMemberAnalyzer.MessageFormat.ToString(), "Foo");

      protected override bool TokenShouldTrigger(SyntaxToken token)
         => token.Kind() == SyntaxKind.GetKeyword 
           || (token.Kind() == SyntaxKind.IdentifierToken
                  && token.Text == "Foo"
                  && token.Parent.Kind() == SyntaxKind.MethodDeclaration);

      public override Location GetSquiggleLocation(SyntaxToken token)
         => token.Kind() == SyntaxKind.GetKeyword
            ? token.Parent.FirstAncestorOrSelf<PropertyDeclarationSyntax>().Identifier.GetLocation()
            : token.GetLocation();

      [TestMethod]
      public void UseExpressionBodyMember_Empty_string_should_not_give_diagnostic()
      { VerifyCSharpDiagnostic(""); }

      [TestMethod]
      public void UseExpressionBodyMember_NonTriggering_code_should_not_give_diagnostic()
      { VerifyNoDiagnosticForFiles(); }

      [TestMethod]
      public void UseExpressionBodyMember_Triggering_code_should_give_diagnostics()
      { VerifyDiagnosticForFiles(); }

      [TestMethod]
      public void UseExpressionBodyMember_Triggering_code_should_give_code_fix()
      { VerifyCodeFixesForDirectories(); }

      [TestMethod]
      public void UseExpressionBodyMember_Triggering_code_for_specified_files_should_give_code_fix()
      {
         // Use this test during development/debugging to trigger just one file 
         // (add files to list just as needed, and delete later to avoid dupe running)
         var files = new List<string>()
         {
         };
         VerifyCodeFixForSpecificFiles(files, 0);
      }
   }
}