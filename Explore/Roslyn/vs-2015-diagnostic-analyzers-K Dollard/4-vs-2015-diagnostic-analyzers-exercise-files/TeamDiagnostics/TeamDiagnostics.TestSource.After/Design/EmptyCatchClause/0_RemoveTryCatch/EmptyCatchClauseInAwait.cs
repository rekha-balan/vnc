using System.Diagnostics;
using System.Threading.Tasks;

namespace EmptyCatchBlock.TestSource.After0
{
   public class EmptyCatchClauseInAwait
   {
      public async void Foo()
      {
         var x = 42;
         await Task.Delay(100);
         { Debug.WriteLine("EmptyCatchClauseInAwait"); }
      }
   }
}