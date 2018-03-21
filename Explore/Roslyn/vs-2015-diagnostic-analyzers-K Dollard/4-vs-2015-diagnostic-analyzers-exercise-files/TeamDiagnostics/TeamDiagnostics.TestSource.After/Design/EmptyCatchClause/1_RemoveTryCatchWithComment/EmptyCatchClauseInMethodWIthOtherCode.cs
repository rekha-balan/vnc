using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After1
{
   public class EmptyCatchClauseInMethodWIthOtherCode
   {
      public void Foo()
      {
         var x = 42;
         {
            x++;
            Debug.WriteLine("EmptyCatchClauseInMethodWIthOtherCode: " + x);
         }
         //TODO: Consider reading MSDN Documentation about how to use Try...Catch => http://msdn.microsoft.com/en-us/library/0yd65esw.aspx
      }
   }
}