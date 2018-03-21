using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After1
{
   public class EmptyCatchClauseInProperty
   {
      public int Foo
      {
         get
         {
            var x = 42;
            {
               x++;
               Debug.WriteLine("Foo2: " + x);
            }
            //TODO: Consider reading MSDN Documentation about how to use Try...Catch => http://msdn.microsoft.com/en-us/library/0yd65esw.aspx
            return x;
         }
      }
   }
}