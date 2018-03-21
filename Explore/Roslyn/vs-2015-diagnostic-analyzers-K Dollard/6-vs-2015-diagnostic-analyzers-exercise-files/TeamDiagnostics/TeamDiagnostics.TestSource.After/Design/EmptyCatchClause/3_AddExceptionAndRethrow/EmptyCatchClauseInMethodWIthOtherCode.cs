using System.Diagnostics;
using System;

namespace EmptyCatchBlock.TestSource.After3
{
   public class EmptyCatchClauseInMethodWIthOtherCode
   {
      public void Foo()
      {
         var x = 42;
         try
         {
            x++;
            Debug.WriteLine("EmptyCatchClauseInMethodWIthOtherCode: " + x);
         }
         catch (Exception ex)
         {
            throw;
         }
      }
   }
}