using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveWhereClause.TestSource.Before
{
   public class WhereEtcOkUse
   {
      public void Foo()
      {
         var list = new[] { 0, 1, 2, 3, 4, 5 };
         bool hasBig;
         int result;
         result = list.First(x => x > 3);
         result = list.FirstOrDefault(x => x > 3);
         result = list.Last(x => x > 3);
         result = list.LastOrDefault(x => x > 3);
         result = list.Single(x => x > 3);
         result = list.SingleOrDefault(x => x > 3);
         result = list.Count(x => x > 3);
         hasBig = list.Any(x => x > 3);
         var list2 = list.Where(x => x > 3);
      }
   }
}
