using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamAnalyzers.TestSource.Before.Style.UseExpressionBodyMember.Triggering
{
   public  class MultiLineMethod
   {
      public string Foo(int i)
      {
         i++;
         return i.ToString();
      }
   }
}
