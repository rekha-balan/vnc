using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.Before
{
   public class NonEmptyCatchClauseInProperty
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
               Debug.WriteLine("Not empty");
            }
            return x;
         }
      }
   }
}