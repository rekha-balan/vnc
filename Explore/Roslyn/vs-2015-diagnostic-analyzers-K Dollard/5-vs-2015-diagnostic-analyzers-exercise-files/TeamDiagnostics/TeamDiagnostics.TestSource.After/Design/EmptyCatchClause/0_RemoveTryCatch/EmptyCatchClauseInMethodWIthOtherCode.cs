using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.After0
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
      }
   }
}