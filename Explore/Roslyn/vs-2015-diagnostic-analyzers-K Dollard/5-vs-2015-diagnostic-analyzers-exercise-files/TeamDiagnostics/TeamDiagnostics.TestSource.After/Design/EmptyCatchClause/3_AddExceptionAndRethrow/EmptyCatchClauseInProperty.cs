using System.Diagnostics;
using System;

namespace EmptyCatchBlock.TestSource.After3
{
   public class EmptyCatchClauseInProperty
   {
      public int Foo
      {
         get
         {
            var x = 42;
            try
            {
               x++;
               Debug.WriteLine("Foo2: " + x);
            }
            catch (Exception ex)
            {
               throw;
            }
            return x;
         }
      }
   }
}