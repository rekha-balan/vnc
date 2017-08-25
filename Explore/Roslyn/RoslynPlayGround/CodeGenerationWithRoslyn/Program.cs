
using System;

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;

using CodeGenerationWithRoslyn;
using Microsoft.CodeAnalysis.VisualBasic;

namespace CodeGenerationWithRoslyn
{
    class Program
    {
        public delegate void CallMethod();

        public class MethodCall
        {
            public string Name;
            public Int16 Number;
            public CallMethod Target;
            public string Description;

            public MethodCall(string name, short number, CallMethod target, string description)
            {
                Name = name;
                Number = number;
                Target = target;
                Description = description;
            }
        }

        public static Dictionary<Int16, MethodCall> Commands = new Dictionary<Int16, MethodCall>();

        static void ThanksForPlaying()
        {
            Console.WriteLine("Thanks for Playing");
            Console.ReadLine();
        }

        static void InitializeCommands()
        {
            Commands.Add(0, new MethodCall( "Exit", 0, ThanksForPlaying, "Terminate Program"));

            // From 

            Commands.Add(1, new MethodCall("Method1", 1, Method1, "Method1 Description"));
            Commands.Add(2, new MethodCall("Method2", 2, Method2, "Method2 Description"));
            Commands.Add(3, new MethodCall("Method3", 3, Method3, "Method3 Description"));
            Commands.Add(4, new MethodCall("Method4", 4, Method4, "Method4 Description"));
            Commands.Add(5, new MethodCall("Method5", 5, Method5, "Method5 Description"));
            Commands.Add(6, new MethodCall("Method6", 6, Method6, "Method6 Description"));
            Commands.Add(7, new MethodCall("Method7", 7, Method7, "Method7 Description"));
            Commands.Add(8, new MethodCall("Method8", 8, Method8, "Method8 Description"));
            Commands.Add(9, new MethodCall("Method9", 9, Method9, "Method9 Description"));

            // From Learn Roslyn Now - Josh Varty

            // TO get started with Roslyn start a new project and then use NuGet to include Compiler Services
            // Install-Package Microsoft.CodeAnalysis

            // Learn Roslyn Now - E03 - The CSharp Syntax Walker

            Commands.Add(10, new MethodCall("VBSyntaxWalkerDemo", 10, VBSyntaxWalkerDemo, "VBSyntaxWalkerDemo Description"));
            Commands.Add(11, new MethodCall("CSharpSyntaxWalkerDemo", 11, CSharpSyntaxWalkerDemo, "CSharpSyntaxWalkerDemo Description"));

            // Learn Roslyn Now - E04 - The CSharp Syntax Rewriter

            Commands.Add(12, new MethodCall("CSharpSyntaxRewriterDemo1", 12, Method9, "CSharpSyntaxRewriterDemo1 Description"));
            Commands.Add(13, new MethodCall("CSharpSyntaxRewriterDemo2", 13, Method9, "CSharpSyntaxRewriterDemo2 Description"));
            Commands.Add(14, new MethodCall("CSharpSyntaxRewriterDemo3", 14, Method9, "CSharpSyntaxRewriterDemo3 Description"));
            Commands.Add(15, new MethodCall("CSharpSyntaxRewriterDemo4", 15, Method9, "CSharpSyntaxRewriterDemo4 Description"));

            // Learn Roslyn Now - E05 - Semantic Model and Symbols

            Commands.Add(16, new MethodCall("SemanticModelDemo1", 16, Method9, "SemanticModelDemo1 Description"));
            Commands.Add(17, new MethodCall("SemanticModelDemo2", 17, Method9, "SemanticModelDemo2 Description"));

            // Learn Roslyn Now - E06 - MSBuildWorkspace     
            
            Commands.Add(18, new MethodCall("ExploreSolution", 18, Method9, "ExploreSolution Description"));
            Commands.Add(19, new MethodCall("ListFileInfo", 19, ListFileInfo, "ListFileInfo Description"));
            Commands.Add(20, new MethodCall("ChangeFile", 20, Method9, "ChangeFile Description"));

            //VisualStudioWorkSpaceDemo1();

            // Learn Roslyn Now - E07 - VisualStudioWorkspace

            // Learn Roslyn Now - E08 - AdHocWorkspace

            // Learn Roslyn Now - E09 - Introduction to Analyzers

            // Introduction to the .NET Compiler Platform
            // PluralSight Bret De Veers

            // 5 - Semantic Models

            Commands.Add(30, new MethodCall("GettingTheConstantValueOfLiterals", 30, Method9, "GettingTheConstantValueOfLiterals Description"));

            // 7 - Workspace APIs

            Commands.Add(31, new MethodCall("DisplaySolutionInfo", 31, Method9, "DisplaySolutionInfo Description"));
        }

        static void Main(string[] args)
        {
            Int16 selectedCommand;

            InitializeCommands();

            while ((selectedCommand = ParseRequest()) != 0)
            {
                if (Commands.ContainsKey(selectedCommand))
                {
                    Console.WriteLine("--- Start of Execution ---");

                    Commands[selectedCommand].Target();
                }
                else
                {
                    Console.WriteLine("Unrecognized command #");
                }

                Console.WriteLine("--- End of Execution ---");
            }
        }

        static void DisplaySolutionInfo()
        {
            string targetSolution = @"C:\EaseSource\Source-ease_main-C7.7\WebApps\CumminsWI\CumminsWI.sln";

            var workSpace = MSBuildWorkspace.Create();
            var solution = workSpace.OpenSolutionAsync(targetSolution).Result;

            // Classify(workSpace, solution);
            // Formatting(solution);
            // SymbolFinding(solution);
            // Recommend(workSpace, solution);
            // Rename(workSpace, solution);
            // Simplification(solution);
            PrintSolution(solution);
        }

        static void PrintSolution(Solution solution)
        {
            // Print the root of the solution

            Console.WriteLine(Path.GetFileName(solution.FilePath));

            Console.WriteLine("Projects:");

            foreach (var project in solution.Projects)
            {
                Console.WriteLine("  - " + project.Name);
            }

            // Get dependency graph to perform a sort

            var dGraph = solution.GetProjectDependencyGraph();
            var ds = dGraph.GetDependencySets();

            //var ddotp = dGraph.GetProjectsThatDirectlyDependOnThisProject();
            //var tdotp = dGraph.GetProjectsThatTransitivelyDependOnThisProject();

            //var tpddo = dGraph.GetProjectsThatThisProjectDirectlyDependsOn();
            //var tptdo = dGraph.GetProjectsThatThisProjectTransitivelyDependsOn);

            var tsp = dGraph.GetTopologicallySortedProjects();

            // Print all projects, their documents, and references

            foreach (var p in tsp)
            {
                var project = solution.GetProject(p);

                Console.WriteLine("> " + project.Name);

                Console.WriteLine("  > MetadataReferences");

                foreach (var reference in project.MetadataReferences)
                {
                    Console.WriteLine("     - " + reference.Display);
                }

                Console.WriteLine("  > ProjectReferences");

                foreach (var reference in project.ProjectReferences)
                {
                    Console.WriteLine("     - " + solution.GetProject(reference.ProjectId).Name);
                }

                Console.WriteLine("  > Documents");

                foreach (var document in project.Documents)
                {
                    Console.WriteLine("    - " + document.Name + " " + document.GetType().ToString());
                }
            }
        }

        static void GettingTheConstantValueOfLiterals()
        {
            var code = @"class Foo { void Bar() { int x = 42; } }";

            var tree = CSharpSyntaxTree.ParseText(code);

            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

            var comp = CSharpCompilation.Create("Dem0")
                .AddSyntaxTrees(tree)
                .AddReferences(mscorlib);

            var root = tree.GetRoot();
            // root     {class Foo { void Bar() { int x = 42; } }}

            var unit = (CompilationUnitSyntax)root;
            // unit     {class Foo { void Bar() { int x = 42; } }}

            var foo = (ClassDeclarationSyntax)unit.Members[0];
            // foo      {class Foo { void Bar() { int x = 42; } }}

            var bar = (MethodDeclarationSyntax)foo.Members[0];
            // bar      {void Bar() { int x = 42; }}

            var decl = (LocalDeclarationStatementSyntax)bar.Body.Statements[0];
            // decl     {int x = 42;}

            var val = decl.Declaration.Variables[0].Initializer.Value;
            // val      42

            var sem = comp.GetSemanticModel(tree);

            var cst = sem.GetConstantValue(val);
            // cst      Microsoft.CodeAnalysis.Optional<object>

            if (cst.HasValue)
            {
                Console.WriteLine(cst.Value);
                // 42
            }

        }

        static void VBSyntaxWalkerDemo()
        {
            var tree = VisualBasicSyntaxTree.ParseText(@"
    ''' <summary>
    ''' Adds Forward slash (\) if the passed value doesn't have one in the end
    ''' </summary>
    ''' <param name=""strDir"">Directory</param>
    ''' <returns>Directory</returns>
    ''' <remarks></remarks>
    Public Function AddForwardSlash(ByVal strDir As String) As String

    # If TRACE
        Dim startTicks As Long = Log.Trace15(""Enter"", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

        '\ - Slash or Forward Slash, / - Backward Slash
        strDir = Trim("" & strDir)
        If Len(strDir) > 0 Then
            If Right(strDir, 1) <> ""\"" Then
                strDir = strDir & ""\""
            End If
        End If


    # If TRACE
        Log.Trace15(String.Format(""Exit ({0})"", strDir), LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If
        Return strDir
    End Function
");

            // Get errors

            //var diagnostics = tree.GetDiagnostics().Where(n => n.Severity == DiagnosticSeverity.Error).First();

            // Top level node

            var root = tree.GetRoot();

            // ClassDeclarationSyntax
            // MethodDeclarationSyntax
            //
            // TryStatementSyntax


            DisplayHeader("ClassBlockSyntax Node");

            var classS = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.VisualBasic.Syntax.ClassBlockSyntax>().First();

            Console.WriteLine(classS.ToFullString());
            Console.WriteLine(classS.ToString());

            DisplayHeader("MethodBlockSyntax Node");

            var methodS = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.VisualBasic.Syntax.MethodBlockSyntax>().First();

            Console.WriteLine(methodS.ToFullString());
            Console.WriteLine(methodS.ToString());

            DisplayHeader("TryStatementSyntax Node");

            var tryS = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.VisualBasic.Syntax.TryStatementSyntax>().First();
            //var block = tryS.Block;

            Console.WriteLine(tryS.ToFullString());
            Console.WriteLine(tryS.ToString());

            DisplayHeader("Block");

            //Console.WriteLine(block.ToFullString());
            //Console.WriteLine(block.ToString());

            //var method = root.DescendantNodes().OfType<MethodDeclarationSyntax>().First();

            // Use .With after SyntaxNode to see what is available for that node.

            // Rewrite something

            DisplayHeader("Return Int16");

            var returnType = Microsoft.CodeAnalysis.VisualBasic.SyntaxFactory.ParseTypeName("Int16");

            //var newMethodS = methodS.WithReturnType(returnType);

            //Console.WriteLine(newMethodS.ToFullString());
            //Console.WriteLine(newMethodS.ToString());
        }

        static void CSharpSyntaxWalkerDemo()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
class C
{
    void Method1()
    {
        Int32 i = 9;
        Int32 j = 33;

        try
        {
            // Do Stuff
        }
        catch(Exception ex)
        {
        }
    }

    Int32 Method2()
    {
        Int32 i = 9;
        Int32 j = 33;
        
        return i + j;
    }

    void Method3()
    {
        Int32 i = 9;
        Int32 j = 33;

        try
        {
            // Do other stuff
        }
        catch(Exception ex)
        {
        }      
    }
}
");
            // Get errors

            //var diagnostics = tree.GetDiagnostics().Where(n => n.Severity == DiagnosticSeverity.Error).First();

            // Top level node

            var root = tree.GetRoot();

            // ClassDeclarationSyntax
            // MethodDeclarationSyntax
            //
            // TryStatementSyntax


            DisplayHeader("ClassDeclarationSyntax Node");

            var classS = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>().First();

            Console.WriteLine(classS.ToFullString());
            Console.WriteLine(classS.ToString());

            DisplayHeader("MethodDeclarationSyntax Node");

            var methodS = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax>().First();

            Console.WriteLine(methodS.ToFullString());
            Console.WriteLine(methodS.ToString());

            DisplayHeader("TryStatementSyntax Node");

            var tryS = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.TryStatementSyntax>().First();
            var block = tryS.Block;

            Console.WriteLine(tryS.ToFullString());
            Console.WriteLine(tryS.ToString());

            DisplayHeader("Block");

            Console.WriteLine(block.ToFullString());
            Console.WriteLine(block.ToString());

            //var method = root.DescendantNodes().OfType<MethodDeclarationSyntax>().First();

            // Use .With after SyntaxNode to see what is available for that node.

            // Rewrite something

            DisplayHeader("Return Int16");

            var returnType = Microsoft.CodeAnalysis.CSharp.SyntaxFactory.ParseTypeName("Int16");

            var newMethodS = methodS.WithReturnType(returnType);

            Console.WriteLine(newMethodS.ToFullString());
            Console.WriteLine(newMethodS.ToString());

        }

        static void CSharpSyntaxRewriterDemo1()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
class Program
{
    static void Main()
    {
        if (true)
            Console.WriteLine(""It was true!"");
    }
}
");

            var root = tree.GetRoot();

            DisplayHeader("Root Node");

            Console.WriteLine(root.ToFullString());

            var rewriter = new MyRewriter();

            // Pass in node you want to visit.  We will start with root.

            var newRoot = rewriter.Visit(root);

            DisplayHeader("Rewritten Root Node");

            Console.WriteLine(newRoot.ToFullString());
        }

        static void CSharpSyntaxRewriterDemo2()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
class Program
{
    static void Main()
    {
        if (true)
            Console.WriteLine(""It was true!"");
    }
}
");

            var root = tree.GetRoot();

            DisplayHeader("Root Node");

            Console.WriteLine(root.ToFullString());

            var ifStatements = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.IfStatementSyntax>();

            foreach (var ifStatement in ifStatements)
            {
                var body = ifStatement.Statement;
                var block = Microsoft.CodeAnalysis.CSharp.SyntaxFactory.Block(body);
                var newIfStatement = ifStatement.WithStatement(block);
                root = root.ReplaceNode(ifStatement, newIfStatement);
            }

            var result = root;

            DisplayHeader("Rewritten Root Node");

            Console.WriteLine(root.ToFullString());
        }

        static void CSharpSyntaxRewriterDemo3()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
class Program
{
    static void Main()
    {
        if (true)
            Console.WriteLine(""It was true!"");
        if (false)
            Console.WriteLine(""OMG Why not?"");
    }
}
");

            var root = tree.GetRoot();

            DisplayHeader("Root Node");

            Console.WriteLine(root.ToFullString());

            var rewriter = new MyRewriter();

            // The CSharpSyntaxRewriter does a depth first pass
            // it builds trees up from the bottom and glues them together
            // and covers both if statements

            var newRoot = rewriter.Visit(root);

            DisplayHeader("Rewritten Root Node");

            Console.WriteLine(newRoot.ToFullString());
        }

        static void CSharpSyntaxRewriterDemo4()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
class Program
{
    static void Main()
    {
        if (true)
            Console.WriteLine(""It was true!"");
        if (false)
            Console.WriteLine(""OMG Why not?"");
    }
}
");

            var root = tree.GetRoot();

            DisplayHeader("Root Node");

            Console.WriteLine(root.ToFullString());

            var ifStatements = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.IfStatementSyntax>();

            foreach (var ifStatement in ifStatements)
            {
                var body = ifStatement.Statement;
                var block = Microsoft.CodeAnalysis.CSharp.SyntaxFactory.Block(body);
                var newIfStatement = ifStatement.WithStatement(block);
                root = root.ReplaceNode(ifStatement, newIfStatement);
            }

            var result = root;

            DisplayHeader("Rewritten Root Node");

            Console.WriteLine(root.ToFullString());
        }

        static void SemanticModelDemo1()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
public partial class MyPartialClass
{
    void MyMethod()
    {
        System.Console.WriteLine(""Hello Roslyn"");
    }
}

public partial class MyPartialClass
{
}
");
            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var compilation = CSharpCompilation.Create("MyCompilation",
                syntaxTrees: new[] { tree }, 
                references: new[] { mscorlib });

            var root = tree.GetRoot();

            var semaniticModel = compilation.GetSemanticModel(tree);
            var methodSyntax = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax>().Single();

            var methodSymbol = semaniticModel.GetDeclaredSymbol(methodSyntax);

            var invokedMethod = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.InvocationExpressionSyntax>().Single();
            var symbolInfo = semaniticModel.GetSymbolInfo(invokedMethod);

            var invokedSymbol = symbolInfo.Symbol;
            var invokedSymbolCandidates = symbolInfo.CandidateSymbols;

            var containingAssembly = invokedSymbol.ContainingAssembly;

            // ISymbol is the base of all Symbols.
            // Can cast to more specific type to get addition info

            var invokedMethodSymbol = symbolInfo.Symbol as IMethodSymbol;
        }

        static void SemanticModelDemo2()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
public partial class MyPartialClass
{
    void MyMethod()
    {
        System.Console.WriteLine(""Hello Roslyn"");
    }
}

public partial class MyPartialClass
{
}
");
            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var compilation = CSharpCompilation.Create("MyCompilation",
                syntaxTrees: new[] { tree },
                references: new[] { mscorlib });

            var root = tree.GetRoot();

            var semaniticModel = compilation.GetSemanticModel(tree);
            var methodSyntax = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax>().Single();

            var methodSymbol = semaniticModel.GetDeclaredSymbol(methodSyntax);

            var parentAssembly = methodSymbol.ContainingAssembly;

            var firstClass = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>().First();
            var secondClass = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>().Last();

            var firstSymbol = semaniticModel.GetDeclaredSymbol(firstClass);
            var secondSymbol = semaniticModel.GetDeclaredSymbol(secondClass);

            var areEqual = firstSymbol == secondSymbol;
        }

        private static void DisplayHeader(string headerMessage)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine(headerMessage);
            Console.WriteLine("---------------------------");
            Console.WriteLine("\n");
        }

        static void ExploreSolution()
        {
            ExploreSolution(@"C:\GitHub\VNC\Explore\Roslyn\RoslynPlayGround\RoslynPlayground.sln");
        }

        static void ExploreSolution(string solutionFullPath)
        {
            var msBuildWS = MSBuildWorkspace.Create();
            // Should await task. Be lazy and just get result
            var solution = msBuildWS.OpenSolutionAsync(solutionFullPath).Result;

            foreach (var project in solution.Projects)
            {
                foreach (var document in project.Documents)
                {
                    Console.WriteLine(string.Format("Name: {0}  FilePath: {1}\n", document.Name, document.FilePath));
                }
            }
        }

        static void ChangeFile()
        {
            ChangeFile(@"C:\GitHub\VNC\Explore\Roslyn\RoslynPlayGround\RoslynPlayground.sln");
        }

        static void ChangeFile(string solutionFullPath)
        {
            string path = @"C:\GitHub\VNC\Explore\Roslyn\RoslynPlayGround\RoslynPlayground.sln";

            var msBuildWS = MSBuildWorkspace.Create();
            // Should await task. Be lazy and just get result
            var solution = msBuildWS.OpenSolutionAsync(path).Result;

            var document = solution.Projects.SelectMany(n => n.Documents.Where(m => m.Name == "TestFile.cs")).Single();

            var documentSourceText = document.GetTextAsync().Result;
            string documentString = documentSourceText.ToString();

            Console.WriteLine(documentString);

            // Add the comment at the beginning of the file.

            var newSourceText = SourceText.From("// This is a new comment\n" + documentString);

            // NB.  WithText replaces all the contents.

            var newDocument = document.WithText(newSourceText);

            var newDocumentText = newDocument.GetTextAsync().Result;

            Console.WriteLine(newDocumentText.ToString());

            var result = msBuildWS.TryApplyChanges(newDocument.Project.Solution);
        }

        static void ListFileInfo()
        {
            string path = @"C:\GitHub\VNC\Explore\Roslyn\RoslynPlayGround\RoslynPlayground.sln";

            var msBuildWS = MSBuildWorkspace.Create();
            // Should await task. Be lazy and just get result
            var solution = msBuildWS.OpenSolutionAsync(path).Result;
            string targetFile = "TestFile.cs";

            ListFileInfo(solution, targetFile);
        }

        static void ListFileInfo(Microsoft.CodeAnalysis.Solution solution, string targetFile)
        {
            var document = solution.Projects.SelectMany(n => n.Documents.Where(m => m.Name == targetFile)).Single();

            var documentSourceText = document.GetTextAsync().Result;
            string documentString = documentSourceText.ToString();

            Console.WriteLine(documentString);
        }

        private static void Method1()
        {
            string sourceCode = "a=B+c;";
            Console.WriteLine("sourceCode");
            Console.WriteLine(sourceCode);

            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
            var walker = new SyntaxWalkers.CS.AllNodes();
            walker.Visit(tree.GetRoot());
            Console.WriteLine("We can get back to the original code by calling ToFullString()");
            Console.WriteLine(tree.GetRoot().ToFullString());
        }

        private static void Method2()
        {
            var sourceCode = "";

            using (var sr = new StreamReader("../../GreetingRules.cs"))
            {
                sourceCode = sr.ReadToEnd();
            }

            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            var walker = new SyntaxWalkers.CS.AllNodes();
            walker.Visit(tree.GetRoot());
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
        }

        #region Utilities

        static Int16 ParseRequest()
        {
            Int16 selectedCommand;
            Boolean success = false;

            do
            {
                DisplayCommands();
                string input = Console.ReadLine();
                success = Int16.TryParse(input, out selectedCommand);
            } while (!success);

            return selectedCommand;
        }

        static void DisplayCommands()
        {
            Console.WriteLine("Enter # of Command to Execute");

            foreach (var command in Commands)
            {
                Console.WriteLine(string.Format("  {0,2} - {1} - {2}", command.Key, command.Value.Name, command.Value.Description));
            }
        }

        #endregion

    }
}
