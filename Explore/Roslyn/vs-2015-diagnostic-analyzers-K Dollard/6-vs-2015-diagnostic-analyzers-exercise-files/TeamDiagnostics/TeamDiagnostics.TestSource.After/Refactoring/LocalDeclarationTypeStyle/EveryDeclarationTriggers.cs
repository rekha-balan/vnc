using System;

namespace TeamAnalyzers.TestSource.After.Refactoring
{
   public class Changes
   {
      public class Class1 : I1 { }
      public interface I1 { }

      public void Foo()
      {
         var a = 4;
         var b = 4;
         var c = "";
         var d = new Class1();
         var e = new Class1();
         var f = Bar1();
         var g = Bar2();
         var h = Bar3();
         var i = 4;
         var j = 42;

         int aVar = 4;
         double bVar = 4;
         string cVar = "";
         Class1 dVar = new Class1();
         Class1 eVar = Bar1();
         I1 gVar = Bar2();
         object hVar = Bar3();
      }

      public Class1 Bar1() { return null; }
      public I1 Bar2() { return null; }
      public object Bar3() { return null; }
   }
}
