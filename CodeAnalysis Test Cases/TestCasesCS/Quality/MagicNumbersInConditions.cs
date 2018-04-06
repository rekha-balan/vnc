using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTestCases.Quality
{
    public class MagicNumbersInConditions
    {
        int x = 8;

        public bool IsGood(string password)
        {
            if (password.Length < 5)
                return false;

            return password.Length >= 7;
        }

        public int fun()
        {
            int[] g = null;

            return g[x];
        }

        public bool zun()
        {
            float z = 0.0F;

            if (z > 3.4)
                return false;
            else
                return true;
        }
    }
}
