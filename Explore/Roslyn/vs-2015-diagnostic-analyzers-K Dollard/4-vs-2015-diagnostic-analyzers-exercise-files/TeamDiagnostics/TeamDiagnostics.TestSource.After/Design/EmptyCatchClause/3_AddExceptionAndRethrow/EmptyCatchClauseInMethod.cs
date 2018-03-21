using System;

namespace EmptyCatchBlock.TestSource.After3
{
   public class EmptyCatchClauseInMethod
   {
      public void Foo()
      {
         try
         {
            // code here
         }
         catch (Exception ex)
         {
            throw;
         }
      }
   }
}