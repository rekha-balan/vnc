using System.Diagnostics;
using System;

namespace EmptyCatchBlock.TestSource.After3
{
   public class EmptyCatchClauseInIfNoBraces
   {
      public void Foo()
      {
         if (true)
            try { Debug.WriteLine("EmptyCatchClauseInIfNoBraces"); }
            catch (Exception ex)
            {
               throw;
               // Oh ick! please don't write code with if without braces!
            }
      }
   }
}