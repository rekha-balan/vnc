using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTestCases.Quality
{
    public class LotsOfLocalVariables
    {
        public int fun(int x)
        {
            int y = 0;
            x++;
            return x + 1;
        }
        public double funny(double x)
        {
            return x / 2.13;
        }
    }
}
