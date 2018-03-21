using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamDiagnostics.Test
{
   [TestClass]
   public class LocalDeclarationTypeStyleTest : CustomRefactoringVerifier
   {
      protected override string GetBeforeDirectory()
         => @"..\..\..\TeamDiagnostics.TestSource.Before\Refactoring\LocalDeclarationTypeStyle";
      protected override string GetAfterDirectory()
         => @"..\..\..\TeamDiagnostics.TestSource.After\Refactoring\LocalDeclarationTypeStyle";
      protected override CodeRefactoringProvider GetCSharpRefactoringProvider()
         => new LocalDeclarationTypeStyleRefactoring();
      public override int IndentSpaces => 3;
   
      protected override IEnumerable<Location> GetLocations(SyntaxNode root)
      {
         var declarations = root.DescendantNodes()  
                              .OfType<LocalDeclarationStatementSyntax>()
                              .Select(x=>x.GetLocation());
         return declarations;
      }

      [TestMethod]
      public void LocalDeclarationTypeStyleTest_nontriggering_files_do_not_trigger()
      {
         VerifyNoRefactoringForFiles();
      }

      [TestMethod]
      public void LocalDeclarationTypeStyleTest_triggering_count_correct_for_triggering_files()
      {
         VerifyAllOfTypeSingleRefactoring<LocalDeclarationStatementSyntax>();
      }

      //[TestMethod]
      //public void LocalDeclarationTypeStyleTest_triggering_creates_correct_output()
      //{
      //   VerifyRefactoringResults();
      //}
   }
}