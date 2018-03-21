using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After2
{
   public class EmptyCatchClauseInProperty
   {
      public int Foo
      {
         get
         {
            var x = 42;
            x++;
            Debug.WriteLine("Foo2: " + x);
            return x;
         }
      }
   }
}