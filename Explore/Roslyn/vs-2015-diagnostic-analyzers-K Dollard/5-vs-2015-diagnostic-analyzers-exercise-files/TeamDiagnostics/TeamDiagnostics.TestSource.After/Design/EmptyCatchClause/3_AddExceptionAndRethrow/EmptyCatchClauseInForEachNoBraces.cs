using System.Diagnostics;
using System;

namespace EmptyCatchBlock.TestSource.After3
{
   public class EmptyCatchClauseInForEachNoBraces
   {

      public void Foo()
      {
         foreach (var i in new int[] { 0, 1, 2, 3 })
            try { Debug.WriteLine("EmptyCatchClauseInForEachNoBraces"); }
            catch (Exception ex)
            {
               throw;
               // Oh ick! please don't write code with for without braces!
            }
      }

   }
}