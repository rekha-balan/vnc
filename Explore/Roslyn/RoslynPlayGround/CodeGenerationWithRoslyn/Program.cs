using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace CodeGenerationWithRoslyn
{
    class Program
    {
        static void Main(string[] args)
        {
            //Method1();
            //Method2();
            //Method3();
            //Method4();
            //Method5();
            //Method6();
            //Method7();
            //Method8();
            Method9();
        }

        private static void Method1()
        {
            string sourceCode = "a=B+c;";

            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
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

        private static void Method4()
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

            var targetType = compilation.GetTypeByMetadataName("CodeGenerationWithRoslyn.IGreetingProfile");
            var type = Symbols.FindClassesDeriviedOrImplementedByType(compilation, targetType);

            Console.WriteLine(type.First().Identifier.ToFullString());

            Console.ReadLine();
        }

        private static void Method5()
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

            Stream outputStreamDLL = new FileStream("CodeGenerationWithRoslyn.dll", FileMode.Create);
            Stream outputStreamPDB = new FileStream("CodeGenerationWithRoslyn.pdb", FileMode.Create);

            var results = compilation.Emit(outputStreamDLL, outputStreamPDB);
            //var results = compilation.Emit("CodeGenerationWithRoslyn.dll", "CodeGenerationWithRoslyn.pdb");

            if (! results.Success)
            {
                foreach (var item in results.Diagnostics)
                {
                    if (item.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error)
                    {
                        Console.WriteLine(item.GetMessage());
                    }
                }
            }

            Console.ReadLine();
        }

        private static void Method6()
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

            var memory = new MemoryStream();

            var results = compilation.Emit(memory);

            if (!results.Success)
            {
                foreach (var item in results.Diagnostics)
                {
                    if (item.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error)
                    {
                        Console.WriteLine(item.GetMessage());
                    }
                }
            }

            var assembly = Assembly.Load(memory.ToArray());
            var types = assembly.GetTypes();

            var greetingRules = types.FirstOrDefault(t => t.Name == "GreetingRules");

            if (greetingRules == null)
            {
                throw new Exception("Could not find the type, GreetingRules");
            }

            foreach (var method in greetingRules.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            {
                Console.WriteLine(method.Name);
            }             

            Console.ReadLine();
        }

        private static void Method7()
        {
            var work = MSBuildWorkspace.Create();
            var solution = work.OpenSolutionAsync(@"..\..\..\RoslynPlayGround.sln").Result;
            var project = solution.Projects.FirstOrDefault(p => p.Name == "CodeGenerationWithRoslyn");

            if (project == null)
            {
                throw new Exception("Could not find the CodeGenerationWithRoslyn Project");
            }

            // Provide compilation options

            var options = new CSharpCompilationOptions(
                Microsoft.CodeAnalysis.OutputKind.ConsoleApplication, 
                optimizationLevel: Microsoft.CodeAnalysis.OptimizationLevel.Release,
                platform: Microsoft.CodeAnalysis.Platform.X64);

            project = project.WithCompilationOptions(options);
            var compilation = project.GetCompilationAsync().Result;

            // Do something with the compilation

            var memory = new MemoryStream();

            var results = compilation.Emit(memory);

            if (!results.Success)
            {
                foreach (var item in results.Diagnostics)
                {
                    if (item.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error)
                    {
                        Console.WriteLine(item.GetMessage());
                    }
                }
            }

            var assembly = Assembly.Load(memory.ToArray());
            var types = assembly.GetTypes();

            var greetingRules = types.FirstOrDefault(t => t.Name == "GreetingRules");

            if (greetingRules == null)
            {
                throw new Exception("Could not find the type, GreetingRules");
            }

            foreach (var method in greetingRules.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            {
                Console.WriteLine(method.Name);
            }

            Console.ReadLine();
        }

        private static void Method8()
        {

            var emptyClassTree = SimpleGenerator.CreateEmptyClass("GreetingBusinessRule");
            var emptyClass = emptyClassTree.GetRoot().DescendantNodes()
                .OfType<ClassDeclarationSyntax>().FirstOrDefault();

            if (emptyClass == null)
            {
                return;
            }

            Console.WriteLine(emptyClass.ToFullString());

            Console.ReadLine();
        }

        private static void Method9()
        {

            var emptyClassTree = SimpleGenerator.CreateEmptyClass("GreetingBusinessRule");
            var emptyClass = emptyClassTree.GetRoot().DescendantNodes()
                .OfType<ClassDeclarationSyntax>().FirstOrDefault();

            if (emptyClass == null)
            {
                return;
            }

            var reference = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var compilation = CSharpCompilation.Create("internal").WithReferences(reference);

            var intType = compilation.GetTypeByMetadataName("System.Int32");
            var stringType = compilation.GetTypeByMetadataName("System.String");
            var dateTimeType = compilation.GetTypeByMetadataName("System.DateTime");

            emptyClass = emptyClass
                .AddProperty("Age", intType)
                .AddProperty("FirstName", stringType)
                .AddProperty("LastName", stringType)
                .AddProperty("DateOfBirth", dateTimeType)
                .NormalizeWhitespace();

            Console.WriteLine(emptyClass.ToFullString());

            Console.ReadLine();
        }
    }
}
