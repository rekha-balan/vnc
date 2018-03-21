using System.Diagnostics;

namespace EmptyCatchBlock.TestSource.Before
{
   public class EmptyCatchClauseInIfNoBraces
   {
      public void Foo()
      {
         if (true)
            try { Debug.WriteLine("EmptyCatchClauseInIfNoBraces"); }
            catch
            {
               // Oh ick! please don't write code with if without braces!
            }
      }
   }
}