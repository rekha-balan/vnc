using System.Diagnostics;
using System.Threading.Tasks;

namespace EmptyCatchBlock.TestSource.After1
{
   public class EmptyCatchClauseInAwait
   {
      public async void Foo()
      {
         var x = 42;
         await Task.Delay(100);
         { Debug.WriteLine("EmptyCatchClauseInAwait"); }
         //TODO: Consider reading MSDN Documentation about how to use Try...Catch => http://msdn.microsoft.com/en-us/library/0yd65esw.aspx
      }
   }
}