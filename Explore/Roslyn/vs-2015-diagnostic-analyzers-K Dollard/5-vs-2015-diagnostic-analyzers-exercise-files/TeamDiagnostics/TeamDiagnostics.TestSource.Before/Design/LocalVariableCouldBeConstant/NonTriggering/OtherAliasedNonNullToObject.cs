using System;
using var = System.Object;

namespace TeamAnalyzers.TestSource.Before.Design.LocalVariableCouldBeConstant.NonTriggering
{
   public class OtherAliasedNonNullToObject
   {
      public char A() { return 'A'; }

      public void Foo()
      {
         var a = "42";

         var a1 = a;

      }
   }
}
