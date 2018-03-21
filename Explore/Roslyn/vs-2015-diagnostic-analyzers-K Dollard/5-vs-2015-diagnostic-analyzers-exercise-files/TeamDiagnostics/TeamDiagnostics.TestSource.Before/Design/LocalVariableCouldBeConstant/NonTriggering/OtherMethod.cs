using System;

namespace TeamAnalyzers.TestSource.Before.Design.LocalVariableCouldBeConstant.NonTriggering
{
   public class OtherMethod
   {
      public char A() { return 'A'; }

      public void Foo()
      {
         char a = A();
         object b = A();

         var a1 = a;
         var b1 = b;

      }
   }
}
