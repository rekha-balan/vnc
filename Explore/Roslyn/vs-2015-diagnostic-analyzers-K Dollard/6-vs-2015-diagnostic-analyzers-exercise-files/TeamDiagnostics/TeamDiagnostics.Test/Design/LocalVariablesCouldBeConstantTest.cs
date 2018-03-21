using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TeamDiagnostics.Test
{
   [TestClass]
   public class LocalVariablesCouldBeConstantTest : CustomCodeFixVerifier
   {
      protected override CodeFixProvider GetCSharpCodeFixProvider()
         => new LocalVariableCouldBeConstantCodeFix();
      protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
         => new LocalVariableCouldBeConstantAnalyzer();
      protected override string GetDiagnosticId()
         => LocalVariableCouldBeConstantAnalyzer.DiagnosticId;
      protected override DiagnosticSeverity GetSeverity()
         => DiagnosticSeverity.Warning;
      public override int IndentSpaces => 3;

      protected override string GetBeforeDirectory()
         => @"..\..\..\TeamDiagnostics.TestSource.Before\Design\LocalVariableCouldBeConstant";
      protected override string GetAfterDirectory()
         => @"..\..\..\TeamDiagnostics.TestSource.After\Design\LocalVariableCouldBeConstant";

      protected override string CreateMessage(SyntaxToken token)
            => String.Format(LocalVariableCouldBeConstantAnalyzer.MessageFormat.ToString(),
                  Helpers.FormatList((token.Parent.Parent as VariableDeclarationSyntax).Variables));

      public override Location GetSquiggleLocation(SyntaxToken token)
      {
         var statement = Helpers.FirstAncestorNodeFromToken
                              <LocalDeclarationStatementSyntax>(token);
         return statement.GetLocation();
      }

      protected override bool TokenShouldTrigger(SyntaxToken token)
      {
         var varSyntax = token.Parent.Parent as VariableDeclarationSyntax;
         if (varSyntax != null)
         {
            var localDeclaration = varSyntax.Parent as LocalDeclarationStatementSyntax;
            if (localDeclaration != null && !localDeclaration.IsConst)
            {
               if (varSyntax.Type == token.Parent)
               {
                  return (varSyntax.Variables.First().Initializer != null);
               }
            }
         }
         return false;
      }

      [TestMethod]
      public void LocalVariableCouldBeConstant_Empty_string_should_not_give_diagnostic()
      {
         var test = @"";
         VerifyCSharpDiagnostic(test);
      }

      [TestMethod]
      public void LocalVariableCouldBeConstant_NonTriggering_code_should_not_give_diagnostic()
      {
         VerifyNoDiagnosticForFiles();
      }

      [TestMethod]
      public void LocalVariableCouldBeConstant_Triggering_code_should_give_diagnostics()
      {
         VerifyDiagnosticForFiles();
      }

      [TestMethod]
      public void LocalVariableCouldBeConstant_Triggering_code_should_give_code_fix()
      {
         VerifyCodeFixesForDirectories();
      }

      [TestMethod]
      public void LocalVariableCouldBeConstant_Triggering_code_for_specified_files_should_give_code_fix()
      {
         // Use this test during development/debugging to trigger just one file 
         // (add files to list just as needed, and delete later to avoid dupe running)
         var files = new List<string>()
         {
            "OtherQualifiedNames.cs"
         };
         VerifyCodeFixForSpecificFiles(files, 0);
      }

      [TestMethod]
      public void LocalVariableCouldBeConstant_NonTriggering_code_for_specified_files_should_not_give_daignostic()
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