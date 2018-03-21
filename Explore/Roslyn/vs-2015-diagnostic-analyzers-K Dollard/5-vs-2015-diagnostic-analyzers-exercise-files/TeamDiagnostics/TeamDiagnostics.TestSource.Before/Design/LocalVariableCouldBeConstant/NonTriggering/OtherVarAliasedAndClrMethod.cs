using System;
using var = System.Double;

namespace TeamAnalyzers.TestSource.Before.Design.LocalVariableCouldBeConstant.NonTriggering
{
   public class OtherVarAliasedAndClrMethod
   {
      public char A() { return 'A'; }

      public void Foo()
      {
         var a = Math.Pow(2, 2);

         var a1 = a;

      }
   }
}
