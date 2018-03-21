using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After0
{
   public class EmptyCatchClauseInIfNoBraces
   {
      public void Foo()
      {
         if (true)
         { Debug.WriteLine("EmptyCatchClauseInIfNoBraces"); }
      }
   }
}