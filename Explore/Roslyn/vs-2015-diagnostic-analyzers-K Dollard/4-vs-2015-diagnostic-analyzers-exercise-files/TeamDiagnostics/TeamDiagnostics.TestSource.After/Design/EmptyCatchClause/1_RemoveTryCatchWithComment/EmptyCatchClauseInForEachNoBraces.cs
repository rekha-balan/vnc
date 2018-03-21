using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After1
{
   public class EmptyCatchClauseInForEachNoBraces
   {

      public void Foo()
      {
         foreach (var i in new int[] { 0, 1, 2, 3 })
         { Debug.WriteLine("EmptyCatchClauseInForEachNoBraces"); }
         //TODO: Consider reading MSDN Documentation about how to use Try...Catch => http://msdn.microsoft.com/en-us/library/0yd65esw.aspx
      }

   }
}