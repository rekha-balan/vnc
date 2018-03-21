using System;

namespace TeamAnalyzers.TestSource.Before.Design.LocalVariableCouldBeConstant.Triggering
{
   namespace VarAliasObject
   {
      using var = System.Object;

      public class OtherTriggering
      {

         public void Foo()
         {
            var b = null;

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
               var b = default(var);

               var b1; b1 = b;

            }
         }
      }
   }
}
