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
   public class LocalVariableCouldBeConstantAnalyzer : DiagnosticAnalyzer
   {
      public readonly static string DiagnosticId =
                     KadGenDiagnosticId.LocalVariableCouldBeConstant.ToDiagnosticId();

      // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
      internal static readonly LocalizableString Title =       new LocalizableResourceString(nameof(Resources.LocalVariableCouldBeConstantTitle), Resources.ResourceManager, typeof(Resources));
      public static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.LocalVariableCouldBeConstantMessageFormat), Resources.ResourceManager, typeof(Resources));
      internal static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.LocalVariableCouldBeConstantDescription), Resources.ResourceManager, typeof(Resources));

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
         context.RegisterSyntaxNodeAction(AnalyzeVariableAsConstant, 
                  SyntaxKind.LocalDeclarationStatement);
      }

      private void AnalyzeVariableAsConstant(SyntaxNodeAnalysisContext context)
      {
         var localDeclaration = context.Node as LocalDeclarationStatementSyntax;
         if (localDeclaration == null) { return; }
         if (localDeclaration.IsConst) { return; }

         var model = context.SemanticModel;
         foreach (var variable in localDeclaration.Declaration.Variables)
         {
            if (!CanBeConst(variable, localDeclaration.Declaration, model)) { return; }
         }

         if (!AreAnyVariablesChanged(localDeclaration, model))
         {
            var messageParam = Helpers.FormatList( localDeclaration.Declaration.Variables);
            var location = localDeclaration.GetLocation();
            context.ReportDiagnostic(Diagnostic.Create(Rule, location, messageParam));
         }
      }

      private bool AreAnyVariablesChanged(
               LocalDeclarationStatementSyntax localDeclaration, 
               SemanticModel model)
      {
         var analysis = model.AnalyzeDataFlow(localDeclaration);
         if (!analysis.Succeeded) return true;
         var writtenOutside = analysis.WrittenOutside;
         var ret = new List<VariableDeclaratorSyntax>();
         foreach (var variable in localDeclaration.Declaration.Variables)
         {
            var symbol = model.GetDeclaredSymbol(variable);
            if (writtenOutside.Contains(symbol)) { return true; }
         }
         return false;
      }

      private bool CanBeConst(VariableDeclaratorSyntax variable, VariableDeclarationSyntax declaration, SemanticModel model)
      {
         var initializer = variable.Initializer;
         if (initializer != null)
         {
            if (!(initializer.Value is InterpolatedStringExpressionSyntax))
            {
               var constant = model.GetConstantValue(initializer.Value);
               if (constant.HasValue)
               {
                  var typeSymbol = GetTypeSymbol(declaration, model);

                  return typeSymbol.OriginalDefinition
                                 ?.SpecialType != SpecialType.System_Nullable_T
                         && IsLegalReferenceType(typeSymbol, constant)
                         && IsLegalConversion(initializer, typeSymbol, model);
               }
            }
         }
         return false;
      }

      private bool IsLegalConversion(EqualsValueClauseSyntax initializer, 
               ITypeSymbol typeSymbol, SemanticModel model)
      {
         var conversion = model.ClassifyConversion(initializer.Value, typeSymbol);
         return (conversion.Exists && !conversion.IsUserDefined);
      }

      private bool IsLegalReferenceType(ITypeSymbol typeSymbol, Optional<object> constant)
      {
         if (typeSymbol.SpecialType == SpecialType.System_String) { return true; }
         if (typeSymbol.IsReferenceType)
         {
            if (constant.Value != null) { return false; }
         }
         return true;
      }

      private ITypeSymbol GetTypeSymbol(VariableDeclarationSyntax declaration, 
               SemanticModel model)
      {
         var typeSyntax = declaration.Type;
         var type = model.GetTypeInfo(typeSyntax).ConvertedType;
         return type;
      }
   }
}
