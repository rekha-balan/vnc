using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTestCases.Quality
{
    public class FragmentedConditions
    {
        //Find if statements in each functions where
        //they can be clubbed.
        public Int32 maybe_do_something(int something, int somethingelse, int etc)
        {
            if (something != -1)
                return 0;
            if (somethingelse != -1)
                return 0;
            if (etc != -1)
                return 0;
            //do_something();
            return 1;
        }

        public Int32 otherFun(int bailIfIEqualZero, string shouldNeverBeEmpty)
        {
            if (bailIfIEqualZero == 0)
                return 0;
            if (string.IsNullOrEmpty(shouldNeverBeEmpty))
                return 0;
            //if (betterNotBeNull == null || betterNotBeNull.RunAwayIfTrue)
            //    return 0;
            return 1;
        }
    }
}
