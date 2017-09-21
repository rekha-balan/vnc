using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTestCases.Quality
{
    public class MultipleReturnStatements
    {
        public int fun(int x)
        {
            x++;

            if (x == 0)
                return x;
            else
                return x + 2;
        }
        public double funny3(int x)
        {
            return x / 12;
        }
    }
}
