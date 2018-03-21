using System.Diagnostics;
using System;

namespace EmptyCatchBlock.TestSource.After3
{
   public class EmptyCatchClauseInIfWithBraces
   {
      public void Foo3()
      {
         if (true)
         {
            try { Debug.WriteLine("Foo3"); }
            catch (Exception ex)
            {
               throw;
            }
         }
      }
   }
}