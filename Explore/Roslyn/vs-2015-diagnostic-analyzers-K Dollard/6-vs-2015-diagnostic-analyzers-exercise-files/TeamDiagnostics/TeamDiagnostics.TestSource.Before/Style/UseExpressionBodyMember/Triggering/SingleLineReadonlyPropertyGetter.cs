using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamAnalyzers.TestSource.Before.Style.UseExpressionBodyMember.Triggering
{
   public class SingleLineReadonlyPropertyGetter
   {
      private int i = 42;

      public string Foo
      {
         get
         {
            return "Hello World" + i;
         }
      }
   }
}
