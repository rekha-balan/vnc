using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace TeamDiagnostics
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public class EmptyCatchClauseAnalyzer : DiagnosticAnalyzer
   {
      public readonly static string DiagnosticId =
                     KadGenDiagnosticId.EmptyCatchClause.ToDiagnosticId();

      // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
      internal static readonly LocalizableString Title =       new LocalizableResourceString(nameof(Resources.EmptyCatchClauseTitle), Resources.ResourceManager, typeof(Resources));
      public static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.EmptyCatchClauseMessageFormat), Resources.ResourceManager, typeof(Resources));
      internal static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.EmptyCatchClauseDescription), Resources.ResourceManager, typeof(Resources));

      internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(
                                                   DiagnosticId,
                                                   Title, 
                                                   MessageFormat,
                                                   AnalyzerCategories.Design,
                                                   DiagnosticSeverity.Warning,
                                                   isEnabledByDefault: true);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

      public override void Initialize(AnalysisContext context)
      {
         context.RegisterSyntaxNodeAction(CheckCatchClause, SyntaxKind.CatchClause);
      }

      private void CheckCatchClause(SyntaxNodeAnalysisContext context)
      {
         var catchStatement = context.Node as CatchClauseSyntax;
         if (catchStatement == null) return;
         if (catchStatement.Block == null) return; // can happen during incomplete typing
         if (catchStatement.Block.Statements.Count() == 0)
         {
            var diagnostic = Diagnostic.Create(Rule, catchStatement.GetLocation(),
                     Description);
            context.ReportDiagnostic(diagnostic);
         }
      }
   }
}
