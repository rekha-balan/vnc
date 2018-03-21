using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamAnalyzers.TestSource.Before.Style.UseExpressionBodyMember.Triggering
{
   public class SingleLineReadWritePropertyGetter
   {
      private int i = 42;
      private string _foo;

      public string Foo
      {
         get
         {
            return string.IsNullOrWhiteSpace(_foo)     
                           ?"Hello World" + i
                           : _foo;
         }
         set
         {
            _foo = value;
         }
      }

   }
}
