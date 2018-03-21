using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;
using RemoveWhereClause;
using System.IO;

namespace RemoveWhereClause.Test
{
   [TestClass]
   public class RemoveWhereClauseTest : CodeFixVerifier
   {

      //No diagnostics expected to show up
      [TestMethod]
      public void RemoveWhereClause_Empty_string_should_not_give_diagnostic()
      {
         var test = @"";

         VerifyCSharpDiagnostic(test);
      }

      [TestMethod]
      public void RemoveWhereClause_should_give_many_diagnostics()
      {
         var fileName = "WhereNeedsFixes.cs";
         var test = File.ReadAllText(TestUtilities.GetBeforeSource(fileName));

         var methods = new[]
         {
            "First",
            "FirstOrDefault",
            "Last",
            "LastOrDefault",
            "Single",
            "SingleOrDefault",
            "Count",
            "Any"
         };

         var expectedResults = new DiagnosticResult[methods.Length];
         var column = 24;
         var firstLine = 16;

         for (int i = 0; i < methods.Length; i++)
         {

            var expected = new DiagnosticResult
            {
               Id = RemoveWhereClauseAnalyzer.DiagnosticId,
               Message = String.Format(RemoveWhereClauseAnalyzer.MessageFormat.ToString(),
                               methods[i]),
               Severity = DiagnosticSeverity.Warning,
               Locations =
                  new[] {
                            new DiagnosticResultLocation("Test0.cs", firstLine+i,column )
                        }
            };
            expectedResults[i] = expected;
         }

         VerifyCSharpDiagnostic(test, expectedResults);

         var fixTest = File.ReadAllText(TestUtilities.GetAfterSource(fileName));
         VerifyCSharpFix(test, fixTest);
      }

      [TestMethod]
      public void RemoveWhereClause_non_triggering_code_should_not_trigger()
      {
         var fileName = "WhereEtcOkUse.cs";
         var test = File.ReadAllText(TestUtilities.GetBeforeSource(fileName));

         VerifyCSharpDiagnostic(test);

      }
      protected override CodeFixProvider GetCSharpCodeFixProvider()
      {
         return new RemoveWhereClauseCodeFix();
      }

      protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
      {
         return new RemoveWhereClauseAnalyzer();
      }
   }
}