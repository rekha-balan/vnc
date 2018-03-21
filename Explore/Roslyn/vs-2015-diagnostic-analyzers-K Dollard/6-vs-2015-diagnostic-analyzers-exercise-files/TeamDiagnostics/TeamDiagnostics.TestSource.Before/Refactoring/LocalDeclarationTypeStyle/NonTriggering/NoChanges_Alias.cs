using System;
using var = System.Object;

namespace TeamDiagnostics.TestSource.Before.Refactoring
{
   public class NoChanges_Alias
   {
      public class Class1 : I1 { }
      public interface I1 { }

      public void Foo()
      {
         int a = 4;
         double b = 4.2;
         string c = "";
         Class1 d = new Class1();
         Class1 e = Bar1();
         I1 g = Bar2();
         object h = Bar3();
         Int32 i = 4;
         System.Int32 j = 42;

         var aVar = 4;
         var bVar = 4;
         var cVar = "";
         var dVar = new Class1();
         var eVar = Bar1();
         var gVar = Bar2();
         var hVar = Bar3();

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
