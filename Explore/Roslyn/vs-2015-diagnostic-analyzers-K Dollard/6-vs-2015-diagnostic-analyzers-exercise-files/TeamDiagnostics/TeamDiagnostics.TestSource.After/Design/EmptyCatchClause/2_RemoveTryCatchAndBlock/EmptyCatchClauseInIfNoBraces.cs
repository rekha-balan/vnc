using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After2
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