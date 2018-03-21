using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.Before
{
   public class NonEmptyCatchClauseInMethod
   {
      public void Foo()
      {
         try { }
         catch
         {
            Debug.WriteLine("Not empty");
         }
      }
   }
}