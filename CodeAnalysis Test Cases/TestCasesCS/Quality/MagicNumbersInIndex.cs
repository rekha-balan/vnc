using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTestCases.Quality
{
    public class MagicNumbersInIndex
    {
        public string fun()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();

            int x = 323;
            string z = dic[x] + x + dic[323];

            return z;
        }

        public float funny()
        {
            int d = 234;
            Dictionary<int, float> dic = new Dictionary<int, float>();
            float z = dic[d];
            return z;
        }
    }
}
