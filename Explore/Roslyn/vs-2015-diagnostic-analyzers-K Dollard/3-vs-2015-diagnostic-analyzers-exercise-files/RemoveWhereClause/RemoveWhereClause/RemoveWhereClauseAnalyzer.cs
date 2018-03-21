using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RemoveWhereClause
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public class RemoveWhereClauseAnalyzer : DiagnosticAnalyzer
   {
      public const string DiagnosticId = "KADGEN005";
      internal const string Title = "Remove Where clause where possible";
      public const string MessageFormat = "Move the filter to the '{0}' method";
      internal const string Category = "Performance";

      private string[] LinqFilteringMethods = new[]
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

      internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(
         DiagnosticId, Title, MessageFormat, Category,
         DiagnosticSeverity.Warning, isEnabledByDefault: true);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
      { get { return ImmutableArray.Create(Rule); } }

      public override void Initialize(AnalysisContext context)
      {
         context.RegisterSyntaxNodeAction(CheckWhereClause, SyntaxKind.InvocationExpression);
      }

      private void CheckWhereClause(SyntaxNodeAnalysisContext context)
      {
         var invocation = context.Node as InvocationExpressionSyntax;
         if (invocation == null) { return; }
         var whereSyntax = Utilities.InvokedMethod(invocation);
         if (whereSyntax == null || whereSyntax.ToFullString() != "Where") { return; }
         var parentInvocation = invocation.Parent.Parent as InvocationExpressionSyntax;
         if (parentInvocation == null) { return; }
         var nextMethod = Utilities.InvokedMethod(parentInvocation).ToFullString();
         if (LinqFilteringMethods.Contains(nextMethod))
         {
            var diagnostic = Diagnostic.Create(Rule, whereSyntax.GetLocation(),
                     nextMethod);
            context.ReportDiagnostic(diagnostic);
         }
      }
   }
}
