using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamAnalyzers.TestSource.After.Style.UseExpressionBodyMember.Triggering
{
   public class SingleLineVoidMethod
   {
      public void Foo() => Bar();

      public void Bar()
      {
         var x = 42;
         x++;
      }
   }
}
