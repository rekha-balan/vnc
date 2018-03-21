using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After0
{
   public class EmptyCatchClauseInForEachWithBraces
   {
      public void Foo()
      {
         foreach (var i in new int[] { 0, 1, 2, 3 })
         {
            { Debug.WriteLine("EmptyCatchClauseInForEachWithBraces"); }
         }
      }
   }
}