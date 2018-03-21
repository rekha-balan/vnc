using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamDiagnostics
{
   public static class Helpers
   {
      public static bool ClassDerivesFrom(ITypeSymbol typeSymbol, Type type)
      {
         return ClassDerivesFrom(typeSymbol, type.FullName);
      }

      public static bool ClassDerivesFrom(ITypeSymbol typeSymbol, string qualifiedName)
      {
         var baseType = typeSymbol.BaseType;
         if (baseType is IErrorTypeSymbol) { return false; }
         if (baseType.SpecialType == SpecialType.System_Object) { return false; }
         var displayString = baseType.ToDisplayString();
         if (baseType.ToDisplayString() == qualifiedName) { return true; }
         return ClassDerivesFrom(baseType, qualifiedName);
      }

      public static T FirstAncestorNodeFromToken<T>(SyntaxToken token)
      {
         var foundNode = token.Parent.AncestorsAndSelf().OfType<T>().FirstOrDefault();
         return foundNode;
      }

      public static string FormatList<T>(IEnumerable<T> list)
      {
         var count = list.Count();
         if (count == 0) { return ""; }
         if (count == 1) { return list.First().ToString(); }

         var ret = string.Join(", ", list.Take(count - 1))
                     + " and " + list.ElementAt(count - 1);
         return ret;
      }

      public static async Task<ITypeSymbol> GetTypeSymbol(
               Document document,
               TypeSyntax typeSyntax,
               CancellationToken cancellationToken)
      {
         var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
         var aliasInfo = semanticModel.GetAliasInfo(typeSyntax);
         ITypeSymbol typeSymbol = aliasInfo != null
                           ? aliasInfo.Target as ITypeSymbol
                           : semanticModel.GetTypeInfo(typeSyntax).ConvertedType;
         return typeSymbol;
      }


   }
}
