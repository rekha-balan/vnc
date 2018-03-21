using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RemoveWhereClause
{
   public static class Utilities
   {
      internal static SimpleNameSyntax InvokedMethod(InvocationExpressionSyntax invocation)
      {
         if (invocation == null) return null;
         var memberAccess = invocation.ChildNodes()
                                       .OfType<MemberAccessExpressionSyntax>()
                                       .FirstOrDefault();
         if (memberAccess == null) return null;
         return memberAccess.Name;
      }
   }
}
