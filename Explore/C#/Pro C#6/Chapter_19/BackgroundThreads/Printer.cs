﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BackgroundThreads
{
    public class Printer
    {
        public void PrintNumbers()
        {
            // Display Thread info.
            Console.WriteLine("-> {0} is executing PrintNumbers()",
              Thread.CurrentThread.Name);

            // Print out numbers.
            Console.Write("Your numbers: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0}, ", i);
                Thread.Sleep(2000);
            }
            Console.WriteLine();
        }
    }
}
