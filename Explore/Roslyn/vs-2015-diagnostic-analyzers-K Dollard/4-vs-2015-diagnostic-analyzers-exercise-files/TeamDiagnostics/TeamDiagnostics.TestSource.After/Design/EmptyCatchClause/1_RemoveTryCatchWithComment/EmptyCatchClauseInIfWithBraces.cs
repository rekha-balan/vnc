using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After1
{
   public class EmptyCatchClauseInIfWithBraces
   {
      public void Foo3()
      {
         if (true)
         {
            { Debug.WriteLine("Foo3"); }
            //TODO: Consider reading MSDN Documentation about how to use Try...Catch => http://msdn.microsoft.com/en-us/library/0yd65esw.aspx
         }
      }
   }
}