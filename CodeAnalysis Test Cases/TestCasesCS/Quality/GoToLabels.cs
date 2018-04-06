using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTestCases.Quality
{
    public class GoToLabels
    {
        public static void message()
        {
        }
        public void gotoFun()
        {
            // Search:
            Int32[,] array = null;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (array[i, j].Equals(3))
                    {
                        goto Found;
                    }
                }
            }

            Found:
            chump();
        }

        static void chump()
        {
            Int32 n = 0;

            int cost = 0;
            switch (n)
            {
                case 1:
                    cost += 25;
                    break;
                case 2:
                    cost += 25;
                    goto case 1;
                case 3:
                    cost += 50;
                    goto case 1;
                default:
                    break;
            }
        }
    }
}
