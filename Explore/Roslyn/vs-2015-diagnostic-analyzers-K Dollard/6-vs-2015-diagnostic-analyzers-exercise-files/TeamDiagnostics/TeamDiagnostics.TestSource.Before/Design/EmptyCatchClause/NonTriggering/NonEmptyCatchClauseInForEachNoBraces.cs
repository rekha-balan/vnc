using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.Before
{
   public class NonEmptyCatchClauseInForEachNoBraces
   {
    
      public void Foo()
      {
         foreach (var i in new int[] { 0, 1, 2, 3 })
            try { Debug.WriteLine("EmptyCatchClauseInForEachNoBraces"); }
            catch
            {
               Debug.WriteLine("Not empty");
               // Oh ick! please don't write code with for without braces!
            }
      }

   }
}