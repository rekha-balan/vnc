using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInitializerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        void MethodName()
        {
            int i;
            i++;
            i++;
            i++;
        }
    }

    class Foo
    {
        public Foo F { get; set; }
    }

    class Bar
    {
        void Baz()
        {
            var f = new Foo() { F = new Foo() };
        }
    }
}
