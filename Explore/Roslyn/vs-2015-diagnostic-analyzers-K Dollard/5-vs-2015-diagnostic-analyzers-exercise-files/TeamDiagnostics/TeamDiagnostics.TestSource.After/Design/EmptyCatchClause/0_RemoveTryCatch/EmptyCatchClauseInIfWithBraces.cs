using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After0
{
   public class EmptyCatchClauseInIfWithBraces
   {
      public void Foo3()
      {
         if (true)
         {
            { Debug.WriteLine("Foo3"); }
         }
      }
   }
}