using System;

namespace TeamDiagnostics.TestSource.Before.Refactoring
{
   public class Changes
   {
      public class Class1 : I1 { }
      public interface I1 { }

      public void Foo()
      {
         int a = 4;
         double b = 4;
         string c = "";
         Class1 d = new Class1();
         Changes.Class1 e = new Class1();
         Class1 f = Bar1();
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
      }

      public Class1 Bar1() { return null; }
      public I1 Bar2() { return null; }
      public object Bar3() { return null; }
   }
}
