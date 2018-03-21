using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.Before
{
   public class EmptyCatchClauseInProperty
   {
      public int Foo
      {
         get
         {
            var x = 42;
            try
            {
               x++;
               Debug.WriteLine("Foo2: " + x);
            }
            catch
            {
            }
            return x;
         }
      }
   }
}