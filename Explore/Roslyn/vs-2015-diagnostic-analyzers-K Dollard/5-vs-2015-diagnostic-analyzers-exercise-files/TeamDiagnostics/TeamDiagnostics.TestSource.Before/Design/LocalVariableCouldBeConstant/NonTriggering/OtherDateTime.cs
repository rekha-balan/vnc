using System;

namespace TeamAnalyzers.TestSource.Before.Design.LocalVariableCouldBeConstant.NonTriggering
{
   public class OtherDateTime
   {
      public char A() { return 'A'; }

      public void Foo()
      {
         const char c_E = 'E';

         var f = new DateTime(2015, 1, 1);
         DateTime g = default(DateTime);
         DateTime h = new DateTime();

         var f1 = f;
         var g1 = g;
         var h1 = h;

      }
   }
}
