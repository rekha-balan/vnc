using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.Before
{
   public class EmptyCatchClauseInIfWithBraces
   {
      public void Foo3()
      {
         if (true)
         {
            try { Debug.WriteLine("Foo3"); }
            catch { }
         }
      }
   }
}