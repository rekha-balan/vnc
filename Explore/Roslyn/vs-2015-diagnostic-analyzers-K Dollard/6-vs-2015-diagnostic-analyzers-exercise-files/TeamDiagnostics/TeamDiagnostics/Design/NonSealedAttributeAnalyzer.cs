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
   public class NonSealedAttributeAnalyzer : DiagnosticAnalyzer
   {
      public readonly static string DiagnosticId =
                     KadGenDiagnosticId.NonSealedAttribute.ToDiagnosticId();

      // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
      internal static readonly LocalizableString Title =       new LocalizableResourceString(nameof(Resources.NonSealedAttributeTitle), Resources.ResourceManager, typeof(Resources));
      public static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.NonSealedAttributeMessageFormat), Resources.ResourceManager, typeof(Resources));
      internal static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.NonSealedAttributeDescription), Resources.ResourceManager, typeof(Resources));

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
         // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
         context.RegisterSymbolAction(CheckForSealedAttribute, SymbolKind.NamedType);
      }

      private void CheckForSealedAttribute(SymbolAnalysisContext context)
      {
         var namedTypeSymbol = context.Symbol as INamedTypeSymbol;
         if (namedTypeSymbol == null) { return; }
         if (namedTypeSymbol.TypeKind != TypeKind.Class) { return; }
         if (namedTypeSymbol.IsSealed) { return; }
         if (namedTypeSymbol.IsAbstract) { return; }
         if (Helpers.ClassDerivesFrom(namedTypeSymbol, typeof(System.Attribute)))
         {
            var location = namedTypeSymbol.Locations.First();
            context.ReportDiagnostic(Diagnostic.Create(Rule, location, namedTypeSymbol.Name));
         }
      }
   }
}
