using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.Before
{
   public class EmptyCatchClauseInMethodWIthOtherCode
   {
      public void Foo()
      {
         var x = 42;
         try
         {
            x++;
            Debug.WriteLine("EmptyCatchClauseInMethodWIthOtherCode: " + x);
         }
         catch
         {
         }
      }
   }
}