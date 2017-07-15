using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// From  http://csharpindepth.com/Articles/General/Singleton.aspx
// Implementing the Singleton pattern in C# - Jon Skeet

// Per Mr. Skeet.  His preference is #4 or #6.  
// #5 is elegant but tricker than #2 or #4.
// #1 is broken and #3 has no benefits over #5
// #2 is useful over #4 if need to know if Singleton instantiated 
// or if need to call other static methods without triggering initialization.

// All these implementations share four common characteristics:

// 1. A single constructor, which is private and parameterless.
//      This prevents other classes from instantiating it (which would be a violation of the pattern). 
//      Note that it also prevents subclassing - if a singleton can be subclassed once, 
//      it can be subclassed twice, and if each of those subclasses can create an instance, 
//      the pattern is violated.The factory pattern can be used if you need a single instance of a base type, 
//      but the exact type isn't known until runtime. 

// 2. The class is sealed. 
//      This is unnecessary, strictly speaking, due to the above point, but may help the JIT to optimize things more.

// 3. A static variable which holds a reference to the single created instance, if any.

// 4. A public static means of getting the reference to the single created instance, creating one if necessary.

// Note that all of these implementations also use a public static property Instance as the means of accessing the instance. 
// In all cases, the property could easily be converted to a method, with no impact on thread-safety or performance. 

namespace PatternTemplates
{
    
    // Bad code!  Not thread safe.  Do not use!
    public sealed class Singleton1
    {
        private static Singleton1 instance = null;

        private Singleton1() { }

        public static Singleton1 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton1();
                }

                return instance;
            }
        }
    }

    // Simple thread-safety
    // Good, but poor performance under heavy load because of locking

    public sealed class Singleton2
    {
        private static Singleton2 instance = null;
        private static readonly object padlock = new object();

        private Singleton2() { }

        public static Singleton2 Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton2();
                    }

                    return instance;
                }
            }
        }
    }

    // Attempted thread-safety using double-check locking
    // Avoids getting lock each time but has downsides
    // Don't use

    public sealed class Singleton3
    {
        private static Singleton3 instance = null;
        private static readonly object padlock = new object();

        private Singleton3() { }

        public static Singleton3 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton3();
                        }
                    }
                }

                return instance;
            }
        }
    }

    // Not quite as lazy, but thread-safe without using locks.

    public sealed class Singleton4
    {
        private static Singleton4 instance = null;

        // Explicit static constructor to tell C# compiler
        // to not mark type as beforefieldinit

        static Singleton4() { }

        private  Singleton4() { }

        public static Singleton4 Instance
        {
            get
            {
                return instance;
            }
        }
    }

    // Super simple but no place to put cdoe around property.

    public sealed class Singleton4Light
    {
        // Explicit static constructor to tell C# compiler
        // to not mark type as beforefieldinit

        static Singleton4Light() { }

        private Singleton4Light() { }

        public static readonly Singleton4Light Instance = null;
    }

    // Fully lazy instantiation without locks

    public sealed class Singleton5
    {
        public static Singleton5 Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // to ot mark type as beforefieldinit

            static Nested()  { }

            internal static readonly Singleton5 instance = new Singleton5();
        }
    }

    // Using .NET 4's Lazy<T> type

    public sealed class Singleton6
    {
        public static readonly Lazy<Singleton6> lazy = new Lazy<Singleton6>(() => new Singleton6());

        public static Singleton6 Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        private Singleton6() { }
    }
}
