using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace EmptyCatchBlock.TestSource.After3
{
   public class EmptyCatchClauseInAwait
   {
      public async void Foo()
      {
         var x = 42;
         await Task.Delay(100);
         try { Debug.WriteLine("EmptyCatchClauseInAwait"); }
         catch (Exception ex)
         {
            throw;
         }
      }
   }
}