using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace CSharpStructures
{
    public class Class1
    {
        public struct MyStruct1
        {
            public Int16 i;
            public Int32 j;
            public string s1;
            public string s2;
        }

        public struct MyStruct2
        {
            public Int16 i;
            public Int32 j;
            [VBFixedString(10)]
            public string s1;
            [VBFixedString(20)]
            public string s2;
        }
    }
}
