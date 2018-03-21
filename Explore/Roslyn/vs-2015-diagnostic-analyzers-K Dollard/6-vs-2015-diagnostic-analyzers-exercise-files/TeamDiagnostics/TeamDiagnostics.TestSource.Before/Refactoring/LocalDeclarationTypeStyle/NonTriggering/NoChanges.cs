using System;

namespace TeamDiagnostics.TestSource.Before.Refactoring
{
   public class NoChanges
   {
      public class Class1 : I1 { }
      public interface I1 { }

      public void Foo()
      {
         double k = 4;
         string m = null;
         Class1 n = null;
         Class1 o = null;
         I1 p = new Class1();
         object q = null;
         object r = "";
         I1 s = Bar1();

         int w = 4, x = 3;
         Class1 y = new Class1(), z = Bar1();
      }

      public Class1 Bar1() { return null; }
      public I1 Bar2() { return null; }
      public object Bar3() { return null; }
   }
}
