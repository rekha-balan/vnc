using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.Before
{
   public class NonEmptyCatchClauseInForEachWithBraces
   {
      public void Foo()
      {
         foreach (var i in new int[] { 0, 1, 2, 3 })
         {
            try { Debug.WriteLine("EmptyCatchClauseInForEachWithBraces"); }
            catch
            {
               Debug.WriteLine("Not empty");
            }
         }
      }
   }
}