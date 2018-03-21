using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamAnalyzers.TestSource.Before.Style.UseExpressionBodyMember.Triggering
{
   public class MultiLineReadWritePropertyGetter
   {
      private int i = 42;
      private string _foo;

      public string Foo
      {
         get
         {
            if (string.IsNullOrWhiteSpace(_foo))
            { return "Hello World" + i; }
            return _foo;
         }
         set
         {
            _foo = value;
         }
      }
   }
}
