using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After1
{
   public class EmptyCatchClauseInIfNoBraces
   {
      public void Foo()
      {
         if (true)
         { Debug.WriteLine("EmptyCatchClauseInIfNoBraces"); }
         //TODO: Consider reading MSDN Documentation about how to use Try...Catch => http://msdn.microsoft.com/en-us/library/0yd65esw.aspx
      }
   }
}