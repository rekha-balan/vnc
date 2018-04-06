using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTestCases.Quality
{
    public class LadderIfStatements
    {
        public Int32 funnyBad()
        {
            //Call a function only once
            if (c1() == 1)
                return 1;
            if (c1() == 2)
                return 2;
            if (c1() == 3)
                return 3;
            if (c1() == 4)
                return 4;
            if (c2() == 23)
                return 23;
            if (c3() == 24)
                return 24;

            return 9;
        }

        public void funnyGood()
        {
            if (c1() == 3)
                f1();

            if (c2() == 34)
                f1();
        }

        Int32 c1()
        {
            return 1;
        }

        Int32 c2()
        {
            return 1;
        }

        Int32 c3()
        {
            return 1;
        }

        void f1()
        {
            
        }
    }
}
