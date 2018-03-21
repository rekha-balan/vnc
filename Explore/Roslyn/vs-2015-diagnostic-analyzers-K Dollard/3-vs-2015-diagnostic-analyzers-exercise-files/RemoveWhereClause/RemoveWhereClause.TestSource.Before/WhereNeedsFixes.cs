using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveWhereClause.TestSource.Before
{
   public class WhereNeedsFixes
   {
      public void Foo()
      {
         var list = new[] { 0, 1, 2, 3, 4, 5 };
         bool hasBig;
         int result;
         result = list.Where(x => x > 3).First();
         result = list.Where(x => x > 3).FirstOrDefault();
         result = list.Where(x => x > 3).Last();
         result = list.Where(x => x > 3).LastOrDefault();
         result = list.Where(x => x > 3).Single();
         result = list.Where(x => x > 3).SingleOrDefault();
         result = list.Where(x => x > 3).Count();
         hasBig = list.Where(x => x > 3).Any();
      }
   }
}
