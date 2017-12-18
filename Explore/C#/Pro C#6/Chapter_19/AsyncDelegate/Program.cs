using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AsyncDelegate
{
    public delegate int BinaryOp(int x, int y);

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Async Delegate Invocation *****");

            // Print out the ID of the executing thread.
            Console.WriteLine("Main() invoked on thread {0}.",
              Thread.CurrentThread.ManagedThreadId);

            BinaryOp b = new BinaryOp(Add);
            IAsyncResult iftAR = b.BeginInvoke(10, 10, null, null);

            // If this check iftaR.IsCompleted is removed, the code will block
            // on b.EndInvoke(iftAR);



            //while (!iftAR.IsCompleted)
            //{
            //  This message will keep printing until
            //  the Add() method is finished.

            //    Console.WriteLine("Doing more work in Main()!");
            //    Thread.Sleep(1000); 
            //    // Sleep to avoid thousands of messages
            //}

            // Alternatively you can wait for a specified amount of time (ms)
            // Note the Sleep has been removed

            while (!iftAR.AsyncWaitHandle.WaitOne(2000, true))
            {
                Console.WriteLine("Doing more work in Main()!");
            }

            // Now we know the Add() method is complete.
            // This call will block if Add is not finished.

            int answer = b.EndInvoke(iftAR);

            Console.WriteLine("10 + 10 is {0}.", answer);
            Console.ReadLine();
        }

        static int Add(int x, int y)
        {
            // Print out the ID of the executing thread.
            Console.WriteLine("Add() invoked on thread {0}.",
              Thread.CurrentThread.ManagedThreadId);

            // Pause to simulate a lengthy operation.
            Thread.Sleep(5000);
            return x + y;
        }
    }
}

