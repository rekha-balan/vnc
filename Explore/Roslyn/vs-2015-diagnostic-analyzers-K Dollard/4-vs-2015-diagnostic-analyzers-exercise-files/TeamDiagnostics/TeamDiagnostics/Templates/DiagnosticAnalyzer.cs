//// Template for new code analyzers, updated from the default
////
//// Copy this file and a code fix file if desired into the appropriate category directory 
//// and make the following changes:
////
//// - The class name
//// - The KadGenId enum value, adding a new enum value for your analyzer
//// - The resource names, replacing the word "Analyzer" with the analyzer name
////     - Add these resources to the resources file
//// - The Category, DiagnostidSeverity and isEnabledByDefault
//// - Register an analyzer using one of the registration methods (often RegisterSyntaxNodeAction)
//// - Create the method that will do the analysis - the signature depends on which registration method

//using System;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using System.Linq;
//using System.Threading;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Microsoft.CodeAnalysis.Diagnostics;

//namespace TeamDiagnostics
//{
//   [DiagnosticAnalyzer(LanguageNames.CSharp)]
//   public class TeamDiagnosticsAnalyzer : DiagnosticAnalyzer
//   {
//      public readonly static string DiagnosticId =
//                     KadGenDiagnosticId.NotYetSet.ToDiagnosticId();

//      // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
//      internal static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
//      public static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
//      internal static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));

//      internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(
//                                                   DiagnosticId,
//                                                   Title,
//                                                   MessageFormat,
//                                                   AnalyzerCategories.Design,
//                                                   DiagnosticSeverity.Warning,
//                                                   isEnabledByDefault: true);

//      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

//      public override void Initialize(AnalysisContext context)
//      {
//         // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
//         context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
//      }

//      private static void AnalyzeSymbol(SymbolAnalysisContext context)
//      {
//      }
//   }
//}
