using System;

namespace Test
{
   public class C { }
}

namespace TeamAnalyzers.TestSource.After.Design.LocalVariableCouldBeConstant.Triggering
{

   public class OtherQualifiedNames
   {

      public void Foo()
      {
         const System.Int32 a = 42;
         const System.Object b = null;
         const Test.C c = null;

      }
   }
}