using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Background Threads *****\n");

            Printer p = new Printer();

            Thread bgroundThread = new Thread(new ThreadStart(p.PrintNumbers));

            // This is now a background thread.

            // If you comment this out the app will keep running until
            // the new foreground thread completes

            //bgroundThread.IsBackground = true;
            bgroundThread.Start();

            Console.WriteLine("***** Background Thread Started *****\n");
        }
    }
}
