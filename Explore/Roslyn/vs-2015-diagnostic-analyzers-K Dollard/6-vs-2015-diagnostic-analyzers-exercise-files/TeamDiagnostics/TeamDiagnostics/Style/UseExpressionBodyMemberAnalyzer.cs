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
   public class UseExpressionBodyMemberAnalyzer : DiagnosticAnalyzer
   {
      public readonly static string DiagnosticId =
                     KadGenDiagnosticId.UseExpressionBodyMember.ToDiagnosticId();

      // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
      internal static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.UseExpressionBodyMemberTitle), Resources.ResourceManager, typeof(Resources));
      public static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.UseExpressionBodyMemberMessageFormat), Resources.ResourceManager, typeof(Resources));
      internal static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.UseExpressionBodyMemberDescription), Resources.ResourceManager, typeof(Resources));

      internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(
                                                   DiagnosticId,
                                                   Title,
                                                   MessageFormat,
                                                   AnalyzerCategories.Style,
                                                   DiagnosticSeverity.Hidden,
                                                   isEnabledByDefault: true);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

      public override void Initialize(AnalysisContext context)
      {
         context.RegisterCompilationStartAction(AnalyzeBodyIfCSharp6);
      }

      private void AnalyzeBodyIfCSharp6(CompilationStartAnalysisContext context)
      {
         if (Helpers.IsCSharp6OrHigher(context))
         {
            context.RegisterSyntaxNodeAction(AnalyzeMethodBody,
                     SyntaxKind.MethodDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzePropertyBody,
                     SyntaxKind.PropertyDeclaration);
         }
      }

      private void AnalyzePropertyBody(SyntaxNodeAnalysisContext context)
      {
         var property = context.Node as PropertyDeclarationSyntax;
         var readOnlyGetter = ReadOnlyGetter(property);
         if (readOnlyGetter == null) { return; }
         if (readOnlyGetter.Body.Statements.Count() == 1)
         {
            if (readOnlyGetter.Body.Statements[0] is ReturnStatementSyntax)
            {
               var identifier = property.Identifier;
               context.ReportDiagnostic(Diagnostic.Create(Rule, identifier.GetLocation(), identifier));
            }
         }
      }

      private AccessorDeclarationSyntax ReadOnlyGetter(PropertyDeclarationSyntax property)
      {
         if (property != null
               && property.AccessorList != null
               && property.AccessorList.Accessors.Count == 1)
         {
            var accessor = property.AccessorList.Accessors.Single();
            if (accessor.IsKind(SyntaxKind.GetAccessorDeclaration))
            { return accessor; }
         }
         return null;
      }

      private void AnalyzeMethodBody(SyntaxNodeAnalysisContext context)
      {
         var method = context.Node as MethodDeclarationSyntax;
         if (method == null || method.Body == null) { return; }
         if (method.Body.Statements.Count() == 1)
         {
            StatementSyntax statement;
            if (IsVoidReturning(context.SemanticModel, method))
            { statement = method.Body.Statements[0] as ExpressionStatementSyntax; }
            else
            { statement = method.Body.Statements[0] as ReturnStatementSyntax; }

            if (statement != null)
            {
               var identifier = method.Identifier;
               context.ReportDiagnostic(Diagnostic.Create(Rule, identifier.GetLocation(), identifier));
            }
         }
      }

      private bool IsVoidReturning(SemanticModel semanticModel, MethodDeclarationSyntax method)
      {
         var returnType = semanticModel.GetTypeInfo(method.ReturnType).ConvertedType;
         return (returnType.SpecialType == SpecialType.System_Void);
      }
   }
}
