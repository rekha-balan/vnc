using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After2
{
   public class EmptyCatchClauseInIfWithBraces
   {
      public void Foo3()
      {
         if (true)
         {
            Debug.WriteLine("Foo3");
         }
      }
   }
}