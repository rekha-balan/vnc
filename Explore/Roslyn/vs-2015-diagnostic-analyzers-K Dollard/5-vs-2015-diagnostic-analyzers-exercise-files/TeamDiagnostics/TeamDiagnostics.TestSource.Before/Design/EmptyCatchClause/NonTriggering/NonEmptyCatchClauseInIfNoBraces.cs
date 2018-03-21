using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.Before
{
   public class NonEmptyCatchClauseInIfNoBraces
   {
      public void Foo()
      {
         if (true)
            try { Debug.WriteLine("EmptyCatchClauseInIfNoBraces"); }
            catch
            {
               Debug.WriteLine("Not empty");
               // Oh ick! please don't write code with if without braces!
            }
      }
   }
}