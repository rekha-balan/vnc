using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            RuntimeTextTemplate1 template = new RuntimeTextTemplate1();
            string result = template.TransformText();

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
