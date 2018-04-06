using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTestCases.Quality
{
    public class MagicNumbersInMath
    {
        public float fun(int g)
        {
            int size = 10000;
            g+=23456;//bad code. magic number 23456 is used.
            g+=size;
            return g/10;
        }

        public decimal updateRate(decimal rate)
        {
            return rate / 0.2345M;
        }

        public decimal updateRateM(decimal rateM)
        {
            decimal basis = 0.2345M;
            return rateM/basis;
        }
    }
}
