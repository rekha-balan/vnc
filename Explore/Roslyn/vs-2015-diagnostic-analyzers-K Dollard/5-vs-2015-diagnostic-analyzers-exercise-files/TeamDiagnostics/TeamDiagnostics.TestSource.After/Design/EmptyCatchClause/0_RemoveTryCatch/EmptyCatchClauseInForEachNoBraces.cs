using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After0
{
   public class EmptyCatchClauseInForEachNoBraces
   {

      public void Foo()
      {
         foreach (var i in new int[] { 0, 1, 2, 3 })
         { Debug.WriteLine("EmptyCatchClauseInForEachNoBraces"); }
      }

   }
}