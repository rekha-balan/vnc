using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using System.IO;

namespace CodeGenerationWithRoslyn
{
    class Program
    {
        static void Main(string[] args)
        {
            //Method1();
            //Method2();
            Method3();
        }

        private static void Method1()
        {
            string sourceCode = "a=B+c;";

            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            var walker = new Walker();
            walker.Visit(tree.GetRoot());
            Console.WriteLine("We can get back to the original code by calling ToFullString()");
            Console.WriteLine(tree.GetRoot().ToFullString());
            Console.ReadLine();
        }

        private static void Method2()
        {
            var sourceCode = "";

            using (var sr = new StreamReader("../../GreetingRules.cs"))
            {
                sourceCode = sr.ReadToEnd();
            }

            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            var walker = new Walker();
            walker.Visit(tree.GetRoot());

            Console.ReadLine();
        }

        private static void Method3()
        {
            var work = MSBuildWorkspace.Create();
            var solution = work.OpenSolutionAsync(@"..\..\..\RoslynPlayGround.sln").Result;
            var project = solution.Projects.FirstOrDefault(p => p.Name == "CodeGenerationWithRoslyn");

            if (project == null)
            {
                throw new Exception("Could not find the CodeGenerationWithRoslyn Project");
            }

            var compilation = project.GetCompilationAsync().Result;
            // Do something with the compilation

            Symbols.ReviewSymbolTable(compilation);
            Console.ReadLine();
        }
    }
}
