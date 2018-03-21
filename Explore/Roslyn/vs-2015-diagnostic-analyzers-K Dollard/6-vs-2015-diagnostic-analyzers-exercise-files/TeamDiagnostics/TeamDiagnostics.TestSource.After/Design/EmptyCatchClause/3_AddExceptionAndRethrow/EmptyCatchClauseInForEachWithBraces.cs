using System.Diagnostics;
using System;

namespace EmptyCatchBlock.TestSource.After3
{
   public class EmptyCatchClauseInForEachWithBraces
   {
      public void Foo()
      {
         foreach (var i in new int[] { 0, 1, 2, 3 })
         {
            try { Debug.WriteLine("EmptyCatchClauseInForEachWithBraces"); }
            catch (Exception ex)
            {
               throw;
            }
         }
      }
   }
}