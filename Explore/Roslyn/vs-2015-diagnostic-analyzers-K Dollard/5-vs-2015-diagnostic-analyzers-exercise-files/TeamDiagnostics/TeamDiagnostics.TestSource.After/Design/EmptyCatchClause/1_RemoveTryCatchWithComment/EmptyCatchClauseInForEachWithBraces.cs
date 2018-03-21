using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After1
{
   public class EmptyCatchClauseInForEachWithBraces
   {
      public void Foo()
      {
         foreach (var i in new int[] { 0, 1, 2, 3 })
         {
            { Debug.WriteLine("EmptyCatchClauseInForEachWithBraces"); }
            //TODO: Consider reading MSDN Documentation about how to use Try...Catch => http://msdn.microsoft.com/en-us/library/0yd65esw.aspx
         }
      }
   }
}