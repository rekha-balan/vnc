using System;

namespace TeamAnalyzers.TestSource.Before.Design.LocalVariableCouldBeConstant.NonTriggering
{
   public class OtherMixedInitializer
   {
      public char A() { return 'A'; }

      public void Foo()
      {
         const char c_E = 'E';

         char b = 'B', c;
         char d, e = c_E;

         var b1 = b;
         var e1 = e;

      }
   }
}
