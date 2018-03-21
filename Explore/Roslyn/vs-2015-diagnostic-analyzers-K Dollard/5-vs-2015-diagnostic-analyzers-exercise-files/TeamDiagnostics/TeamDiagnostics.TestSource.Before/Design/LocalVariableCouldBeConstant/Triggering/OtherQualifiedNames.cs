using System;

namespace Test
{
   public class C { }
}

namespace TeamAnalyzers.TestSource.Before.Design.LocalVariableCouldBeConstant.Triggering
{

   public class OtherQualifiedNames
   {

      public void Foo()
      {
         System.Int32 a = 42;
         System.Object b = null;
         Test.C c = null;

      }
   }
}