using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Runtime.InteropServices;


namespace AppDomains
{
    class Program
    {
        static void Main(string[] args)
        {
            Marshalling();

            Console.WriteLine("Return to Continue");
            Console.ReadLine();

            FieldAccessTiming();
            AppDomainResourceMonitoring();
            UnloadTimeout.Go();
        }

        private static void Marshalling()
        {
            // Get a reference to the AppDomain that the calling thread is executing in

            AppDomain callingThreadDomain = Thread.GetDomain();

            // Every AppDomain is assigned a friendly name (helpful for debugging)
            // Get this AppDomain's friendly name and display it

            String callingDomainName = callingThreadDomain.FriendlyName;

            Console.WriteLine("Default AppDomain's friendly name={0}", callingDomainName);

            // Get and display the assembly in our AppDomain that contains the "main" method

            String exeAssembly = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("Main assembly ={0}", exeAssembly);

            // Define a local variable that can refer to an AppDomain

            AppDomain ad2 = null;

            // DEMO 1: Cross-AppDomain communication using Marshal-by-Reference

            Console.WriteLine("{0}Demo #1", Environment.NewLine);

            // Create new AppDomain (security and configuration match current AppDomain

            ad2 = AppDomain.CreateDomain("AD #2", null, null);
            MarshalByRefType mbrt = null;

            // Load our assembly into the new AppDomain, construct an object,
            // marshall it back to our AppDomain (we really get a reference to a proxy)

            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, "MarshalByRefType");

            Console.WriteLine("Type={0}", mbrt.GetType());  // The CLR lies about the type

            // Prove tht we got a reference to a proxy object

            Console.WriteLine("Is Proxy={0}", RemotingServices.IsTransparentProxy(mbrt));

            // This looks like we're calling a method on a MarshallByRefType but we're not
            // We're calling a method on the proxy type.  THe proxy transitions the thread
            // to the AppDomain owing the object and calls this method on the real object.

            mbrt.SomeMethod();

            // Unload the AppDomain

            AppDomain.Unload(ad2);

            // mbrt refers to a valid proxy object; the proxy object refers to an invalid AppDomain

            try
            {
                // Were calling a method on the proxy type.  The AppDomain is invalid, exception thrown

                mbrt.SomeMethod();
                Console.WriteLine("Successful Call");
            }
            catch (Exception AppDomainUnloadException)
            {
                Console.WriteLine("Failed Call");
            }

            // Demo 2.  Cross-AppDomain Communication Using Marshall-by-Value

            Console.WriteLine("{0}Demo #2", Environment.NewLine);

            // Create new AppDomain (security and configuration match current AppDomain)

            ad2 = AppDomain.CreateDomain("AD #2", null, null);

            // Load our assembly into the new AppDomain, construct an object,
            // marshall it back to our AppDomain (we really get a reference to a proxy)

            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, "MarshalByRefType");

            // The object's method returns a copy of the returned object
            // The object is marshalled by value (not by reference)

            MarshalByValType mbvt = mbrt.MethodWithReturn();

            // Prove tht we did not get a reference to a proxy object

            Console.WriteLine("Is Proxy={0}", RemotingServices.IsTransparentProxy(mbvt));

            // This looks like we're calling a method on a MarshallByValType and we are

            Console.WriteLine("Returned object created" + mbvt.ToString());

            // Unload the AppDomain

            AppDomain.Unload(ad2);

            // mbvt refers to a valid object; unloading the AppDomain has no effect

            try
            {
                // We're calling a method on an object; no exception is thrown.

                Console.WriteLine("Returned object created" + mbvt.ToString());
                Console.WriteLine("Successful Call");
            }
            catch (Exception AppDomainUnloadException)
            {
                Console.WriteLine("Failed Call");
            }

            // Demo 2.  Cross-AppDomain Communication Using non-marshalable type

            Console.WriteLine("{0}Demo #3", Environment.NewLine);

            // Create new AppDomain (security and configuration match current AppDomain)

            ad2 = AppDomain.CreateDomain("AD #2", null, null);

            // Load our assembly into the new AppDomain, construct an object,
            // marshall it back to our AppDomain (we really get a reference to a proxy)

            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, "MarshalByRefType");

            // The object's method returns a non-marshalable object

            NonMarshalableType nmt = mbrt.MethodArgAndReturn(callingDomainName);

            // We won't get here
        }


        private sealed class MBRO : MarshalByRefObject { public Int32 x; }
        private sealed class NonMBRO : Object { public Int32 x; }

        private static void FieldAccessTiming()
        {
            const Int32 count = 100000000;
            NonMBRO nonMbro = new NonMBRO();
            MBRO mbro = new MBRO();

            Stopwatch sw = Stopwatch.StartNew();
            for (Int32 c = 0; c < count; c++) nonMbro.x++;
            Console.WriteLine("{0}", sw.Elapsed);

            sw = Stopwatch.StartNew();
            for (Int32 c = 0; c < count; c++) mbro.x++;
            Console.WriteLine("{0}", sw.Elapsed);
        }

        private sealed class AppDomainMonitorDelta : IDisposable
        {
            private AppDomain m_appDomain;
            private TimeSpan m_thisADCpu;
            private Int64 m_thisADMemoryInUse;
            private Int64 m_thisADMemoryAllocated;

            static AppDomainMonitorDelta()
            {
                // Make sure that AppDomain monitoring is turned on
                AppDomain.MonitoringIsEnabled = true;
            }

            public AppDomainMonitorDelta(AppDomain ad)
            {
                m_appDomain = ad ?? AppDomain.CurrentDomain;
                m_thisADCpu = m_appDomain.MonitoringTotalProcessorTime;
                m_thisADMemoryInUse = m_appDomain.MonitoringSurvivedMemorySize;
                m_thisADMemoryAllocated = m_appDomain.MonitoringTotalAllocatedMemorySize;
            }

            public void Dispose()
            {
                GC.Collect();
                Console.WriteLine("FriendlyName={0}, CPU={1}ms",
                   m_appDomain.FriendlyName,
                   (m_appDomain.MonitoringTotalProcessorTime - m_thisADCpu).TotalMilliseconds);

                Console.WriteLine("   Allocated {0:N0} bytes of which {1:N0} survived GCs",
                   m_appDomain.MonitoringTotalAllocatedMemorySize - m_thisADMemoryAllocated,
                   m_appDomain.MonitoringSurvivedMemorySize - m_thisADMemoryInUse);
            }
        }

        private static void AppDomainResourceMonitoring()
        {
            using (new AppDomainMonitorDelta(null))
            {
                // Allocate about 10 million bytes that will survive collections
                var list = new List<Object>();
                for (Int32 x = 0; x < 1000; x++) list.Add(new Byte[10000]);

                // Allocate about 20 million bytes that will NOT survive collections
                for (Int32 x = 0; x < 2000; x++) new Byte[10000].GetType();

                // Spin the CPU for about 5 seconds
                Int64 stop = Environment.TickCount + 5000;
                while (Environment.TickCount < stop) ;
            }
        }

        private static class UnloadTimeout
        {
            private static Int32 s_testCode = 0;   // 0=InfiniteLoop, 1=ManagedSleep, 2=UnmanagedSleep
            private static AppDomain s_ad;

            public static void Go()
            {
                // Create an AppDomain
                s_ad = AppDomain.CreateDomain("AD #2", null, null);

                // Spawn thread to enter the other AppDomain
                Thread t = new Thread((ThreadStart)delegate { s_ad.DoCallBack(Loop); });
                t.Start();
                Thread.Sleep(5000);  // The other thread a chance to run

                Stopwatch sw = null;
                try
                {
                    // Time how long it takes to unload the AppDomain
                    Console.WriteLine("Calling unload");
                    sw = Stopwatch.StartNew();
                    AppDomain.Unload(s_ad);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                Console.WriteLine("Unload returned after {0}", sw.Elapsed);
                Console.WriteLine("Return to Exit");
                Console.ReadLine();
            }

            private static void Loop()
            {
                try
                {
                    switch (s_testCode)
                    {
                        case 0: while (true) ;              // Infinite loop
                        case 1: Thread.Sleep(10000); break; // Managed sleep
                        case 2: Sleep(10000); break;           // Unmanaged sleep
                    }
                }
                catch (ThreadAbortException)
                {
                    Console.WriteLine("Caught ThreadAbortException: Hit return to exit");
                    Console.ReadLine();
                }
            }

            [DllImport("Kernel32")]
            private static extern void Sleep(UInt32 ms);
        }
    }

}
    // Instances can be marshaled-by-reference across AppDomain boundaries

    public sealed class MarshalByRefType : MarshalByRefObject
    {      
        public MarshalByRefType()
        {
            Console.WriteLine("{0} ctor running in {1}", this.GetType().ToString(), Thread.GetDomain().FriendlyName);
        }

        public void SomeMethod()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName);
        }

        public MarshalByValType MethodWithReturn()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName);
            MarshalByValType mbvt = new MarshalByValType();
            return mbvt;
        }

        public NonMarshalableType MethodArgAndReturn(String callingAppDomain)
        {
            // NB. callingAppDomain is [Serializable]

            Console.WriteLine("Calling from '{0}' typeof('{1}'", callingAppDomain,  Thread.GetDomain().FriendlyName);
            NonMarshalableType nmt = new NonMarshalableType();
            return nmt;
        }

        [DebuggerStepThrough]
        public override Object InitializeLifetimeService()
        {
            return null;   // We want an infinite lifetime
        }
    }

    // Instances can be marshaled-by-value across AppDomain boundaries


    [Serializable]
    public sealed class MarshalByValType : Object
    {
        // NB. DateTime is [Serializable]

        private DateTime _creationTime = DateTime.Now;

        public MarshalByValType()
        {
            Console.WriteLine("{0} ctor running in {1}, Created on {2:D}", 
                this.GetType().ToString(), Thread.GetDomain().FriendlyName, _creationTime);
        }

        public override String ToString()
        {
            return _creationTime.ToLongDateString();
        }
    }

    // Instances cannot be marshaled across AppDomain boundaries

    // [Serializable]

    public sealed class NonMarshalableType : Object
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NonMarshalableType"/> class.
        /// </summary>
        public NonMarshalableType()
        {
            Console.WriteLine("{0} ctor running in {1}", this.GetType().ToString(), Thread.GetDomain().FriendlyName);
        }
    }


