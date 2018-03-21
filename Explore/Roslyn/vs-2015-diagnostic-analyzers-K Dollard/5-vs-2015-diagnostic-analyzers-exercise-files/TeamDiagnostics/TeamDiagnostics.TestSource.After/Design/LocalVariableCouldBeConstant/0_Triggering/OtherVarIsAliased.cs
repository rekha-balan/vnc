using System;

namespace TeamAnalyzers.TestSource.After.Design.LocalVariableCouldBeConstant.Triggering
{
   namespace VarAliasObject
   {
      using var = System.Object;

      public class OtherTriggering
      {

         public void Foo()
         {
            const object b = null;

            object b1; b1 = b;

         }
      }

      namespace VarAliasInt
      {
         using var = Int32;

         public class OtherTriggering
         {

            public void Foo()
            {
               const int b = default(var);

               var b1; b1 = b;

            }
         }
      }
   }
}
