using System;

namespace TeamAnalyzers.TestSource.After.Design.LocalVariableCouldBeConstant.Triggering
{
   namespace VarIsType // must be in separate namespace to avoid breaking other tests
   {
      public class var
      {
         // You are perhaps [insert choice] for naming a class "var"
      }

      public class OtherVarIsType
      {

         public void Foo()
         {
            const var b = null;

            var b1; b1 = b;

         }
      }
   }
}
