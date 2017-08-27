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
using CS=Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;

using VB=Microsoft.CodeAnalysis.VisualBasic;

using VNC.CodeAnalysis;

namespace VNCCodeCommandConsole.Commands
{
    class Explore
    {
        #region Main Methods

        public static StringBuilder DisplayClassesVB(string fileNameAndPath, Boolean includeTrivia, Boolean statementsOnly)
        {
            StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(fileNameAndPath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);

            IEnumerable<SyntaxNode> syntaxNodes;

            if (statementsOnly)
            {
                syntaxNodes = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ClassStatementSyntax>();
            }
            else
            {
                syntaxNodes = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ClassBlockSyntax>();
            }

            foreach (SyntaxNode node in syntaxNodes)
            {
                sb.AppendLine(node.ToFullString());
            }

            return sb;
        }

        public static StringBuilder DisplayModulesVB(string fileNameAndPath, Boolean includeTrivia, Boolean statementsOnly)
        {
            StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(fileNameAndPath))
            {
                sourceCode = sr.ReadToEnd();
            }

            var tree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);

            IEnumerable<SyntaxNode> syntaxNodes;

            if (statementsOnly)
            {
                syntaxNodes = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ModuleStatementSyntax>();
            }
            else
            {
                syntaxNodes = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ModuleBlockSyntax>();
            }

            foreach (SyntaxNode node in syntaxNodes)
            {
                sb.AppendLine(node.ToFullString());
            }

            return sb;
        }

        public static StringBuilder DisplayStructuresVB(string fileNameAndPath, Boolean includeTrivia, Boolean statementsOnly)
        {
            StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(fileNameAndPath))
            {
                sourceCode = sr.ReadToEnd();
            }

            var tree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);

            IEnumerable<SyntaxNode> syntaxNodes;

            if (statementsOnly)
            {
                syntaxNodes = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.StructureStatementSyntax>();
            }
            else
            {
                syntaxNodes = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.StructureBlockSyntax>();
            }

            foreach (SyntaxNode node in syntaxNodes)
            {
                sb.AppendLine(node.ToFullString());
            }

            return sb;
        }

        #endregion

        #region Internal Methods

        internal static StringBuilder DisplayAllStructuredTriviaCS(string fileNameAndPath)
        {
            StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(fileNameAndPath))
            {
                sourceCode = sr.ReadToEnd();
            }

            var tree = CS.CSharpSyntaxTree.ParseText(sourceCode);
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.CS.AllStructuredTrivia();
            walker.StringBuilder = sb;
            walker.Visit(tree.GetRoot());

            return sb;
        }

        internal static StringBuilder DisplayAllStructuredTriviaVB(string fileNameAndPath)
        {
            StringBuilder sb = new StringBuilder();
            var sourceCode = "";

            using (var sr = new StreamReader(fileNameAndPath))
            {
                sourceCode = sr.ReadToEnd();
            }

            var tree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.AllStructuredTrivia();
            walker.StringBuilder = sb;
            walker.Visit(tree.GetRoot());

            return sb;
        }

        internal static void DisplayMethodsCS(string fileNameAndPath)
        {
            var sourceCode = "";

            using (var sr = new StreamReader(fileNameAndPath))
            {
                sourceCode = sr.ReadToEnd();
            }

            SyntaxTree tree = CS.CSharpSyntaxTree.ParseText(sourceCode);

            var syntaxNodes = tree.GetRoot().DescendantNodes().OfType<CS.Syntax.MethodDeclarationSyntax>();

            var walker = new VNC.CodeAnalysis.SyntaxWalkers.CS.AllNode();

            foreach (SyntaxNode node in syntaxNodes)
            {
                walker.Visit(node);
            }
        }

        internal static StringBuilder DisplayMethodsVB(string fileNameAndPath, Boolean includeTrivia, Boolean statementsOnly)
        {
            StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(fileNameAndPath))
            {
                sourceCode = sr.ReadToEnd();
            }

            var tree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);

            IEnumerable<SyntaxNode> syntaxNodes;

            if (statementsOnly)
            {
                syntaxNodes = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.MethodStatementSyntax>();      
            }
            else
            {
                syntaxNodes = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.MethodBlockSyntax>();            
            }

            foreach (SyntaxNode node in syntaxNodes)
            {
                sb.AppendLine(node.ToFullString());
            }

            return sb;
        }

        internal static StringBuilder ParseCSDepthNode(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = CS.CSharpSyntaxTree.ParseText(sourceCode);
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.CS.AllNode();
            walker.StringBuilder = sb;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        internal static StringBuilder ParseVBDepthNode(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.AllNode();
            walker.StringBuilder = sb;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        internal static StringBuilder ParseCSStructuredTrivia(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = CS.CSharpSyntaxTree.ParseText(sourceCode);
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.CS.AllStructuredTrivia();
            walker.StringBuilder = sb;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        internal static StringBuilder ParseVBStructuredTrivia(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.AllStructuredTrivia();
            walker.StringBuilder = sb;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        internal static StringBuilder ParseCSDepthToken(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = CS.CSharpSyntaxTree.ParseText(sourceCode);
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.CS.AllToken();
            walker.StringBuilder = sb;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        internal static StringBuilder ParseVBDepthToken(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.AllToken();
            walker.StringBuilder = sb;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        internal static StringBuilder ParseCSDepthTrivia(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = CS.CSharpSyntaxTree.ParseText(sourceCode);
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.CS.AllTrivia();
            walker.StringBuilder = sb;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        internal static StringBuilder ParseVBDepthTrivia(string sourceCode)
        {
            StringBuilder sb = new StringBuilder();

            SyntaxTree tree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);
            var walker = new VNC.CodeAnalysis.SyntaxWalkers.VB.AllTrivia();
            walker.StringBuilder = sb;

            walker.Visit(tree.GetRoot());

            return sb;
        }

        internal static StringBuilder EmitSourceVB(SyntaxTree syntaxTree)
        {
            StringBuilder sb = new StringBuilder();

            //string sourceCode = "a=B+c;";
            //Console.WriteLine("sourceCode");
            //Console.WriteLine(sourceCode);

            //SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
            //var walker = new VNC.CodeAnalysis.SyntaxWalkers.CS.AllNodes();
            //walker.Visit(tree.GetRoot());
            //Console.WriteLine("We can get back to the original code by calling ToFullString()");
            //Console.WriteLine(tree.GetRoot().ToFullString());

            sb.Append(syntaxTree.GetRoot().ToFullString());

            return sb;
        }


        #endregion

        #region Private Methods

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

        static void CSharpSyntaxRewriterDemo1()
        {
            var tree = CS.CSharpSyntaxTree.ParseText(@"
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

            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.CS.MyRewriter();

            // Pass in node you want to visit.  We will start with root.

            var newRoot = rewriter.Visit(root);

            DisplayHeader("Rewritten Root Node");

            Console.WriteLine(newRoot.ToFullString());
        }

        static void CSharpSyntaxRewriterDemo2()
        {
            var tree = CS.CSharpSyntaxTree.ParseText(@"
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

            var ifStatements = root.DescendantNodes().OfType<CS.Syntax.IfStatementSyntax>();

            foreach (var ifStatement in ifStatements)
            {
                var body = ifStatement.Statement;
                var block = CS.SyntaxFactory.Block(body);
                var newIfStatement = ifStatement.WithStatement(block);
                root = root.ReplaceNode(ifStatement, newIfStatement);
            }

            var result = root;

            DisplayHeader("Rewritten Root Node");

            Console.WriteLine(root.ToFullString());
        }

        static void CSharpSyntaxRewriterDemo3()
        {
            var tree = CS.CSharpSyntaxTree.ParseText(@"
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

            var rewriter = new VNC.CodeAnalysis.SyntaxRewriters.CS.MyRewriter();

            // The CSharpSyntaxRewriter does a depth first pass
            // it builds trees up from the bottom and glues them together
            // and covers both if statements

            var newRoot = rewriter.Visit(root);

            DisplayHeader("Rewritten Root Node");

            Console.WriteLine(newRoot.ToFullString());
        }

        static void CSharpSyntaxRewriterDemo4()
        {
            var tree = CS.CSharpSyntaxTree.ParseText(@"
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

            var ifStatements = root.DescendantNodes().OfType<CS.Syntax.IfStatementSyntax>();

            foreach (var ifStatement in ifStatements)
            {
                var body = ifStatement.Statement;
                var block = CS.SyntaxFactory.Block(body);
                var newIfStatement = ifStatement.WithStatement(block);
                root = root.ReplaceNode(ifStatement, newIfStatement);
            }

            var result = root;

            DisplayHeader("Rewritten Root Node");

            Console.WriteLine(root.ToFullString());
        }

        static void CSharpSyntaxWalkerDemo()
        {
            var tree = CS.CSharpSyntaxTree.ParseText(@"
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

            var classS = root.DescendantNodes().OfType<CS.Syntax.ClassDeclarationSyntax>().First();

            Console.WriteLine(classS.ToFullString());
            Console.WriteLine(classS.ToString());

            DisplayHeader("MethodDeclarationSyntax Node");

            var methodS = root.DescendantNodes().OfType<CS.Syntax.MethodDeclarationSyntax>().First();

            Console.WriteLine(methodS.ToFullString());
            Console.WriteLine(methodS.ToString());

            DisplayHeader("TryStatementSyntax Node");

            var tryS = root.DescendantNodes().OfType<CS.Syntax.TryStatementSyntax>().First();
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

            var returnType = CS.SyntaxFactory.ParseTypeName("Int16");

            var newMethodS = methodS.WithReturnType(returnType);

            Console.WriteLine(newMethodS.ToFullString());
            Console.WriteLine(newMethodS.ToString());

        }

        private static void DisplayHeader(string headerMessage)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine(headerMessage);
            Console.WriteLine("---------------------------");
            Console.WriteLine("\n");
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

        static void GettingTheConstantValueOfLiterals()
        {
            var code = @"class Foo { void Bar() { int x = 42; } }";

            var tree = CS.CSharpSyntaxTree.ParseText(code);

            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

            var comp = CS.CSharpCompilation.Create("Dem0")
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

            var options = new CS.CSharpCompilationOptions(
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

        static void SemanticModelDemo1()
        {
            var tree = CS.CSharpSyntaxTree.ParseText(@"
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
            var compilation = CS.CSharpCompilation.Create("MyCompilation",
                syntaxTrees: new[] { tree },
                references: new[] { mscorlib });

            var root = tree.GetRoot();

            var semaniticModel = compilation.GetSemanticModel(tree);
            var methodSyntax = root.DescendantNodes().OfType<CS.Syntax.MethodDeclarationSyntax>().Single();

            var methodSymbol = semaniticModel.GetDeclaredSymbol(methodSyntax);

            var invokedMethod = root.DescendantNodes().OfType<CS.Syntax.InvocationExpressionSyntax>().Single();
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
            var tree = CS.CSharpSyntaxTree.ParseText(@"
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
            var compilation = CS.CSharpCompilation.Create("MyCompilation",
                syntaxTrees: new[] { tree },
                references: new[] { mscorlib });

            var root = tree.GetRoot();

            var semaniticModel = compilation.GetSemanticModel(tree);
            var methodSyntax = root.DescendantNodes().OfType<CS.Syntax.MethodDeclarationSyntax>().Single();

            var methodSymbol = semaniticModel.GetDeclaredSymbol(methodSyntax);

            var parentAssembly = methodSymbol.ContainingAssembly;

            var firstClass = root.DescendantNodes().OfType<CS.Syntax.ClassDeclarationSyntax>().First();
            var secondClass = root.DescendantNodes().OfType<CS.Syntax.ClassDeclarationSyntax>().Last();

            var firstSymbol = semaniticModel.GetDeclaredSymbol(firstClass);
            var secondSymbol = semaniticModel.GetDeclaredSymbol(secondClass);

            var areEqual = firstSymbol == secondSymbol;
        }

        static void VBSyntaxWalkerDemo()
        {
            var tree = VB.VisualBasicSyntaxTree.ParseText(@"
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

            var classS = root.DescendantNodes().OfType<VB.Syntax.ClassBlockSyntax>().First();

            Console.WriteLine(classS.ToFullString());
            Console.WriteLine(classS.ToString());

            DisplayHeader("MethodBlockSyntax Node");

            var methodS = root.DescendantNodes().OfType<VB.Syntax.MethodBlockSyntax>().First();

            Console.WriteLine(methodS.ToFullString());
            Console.WriteLine(methodS.ToString());

            DisplayHeader("TryStatementSyntax Node");

            var tryS = root.DescendantNodes().OfType<VB.Syntax.TryStatementSyntax>().First();
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

            var returnType = VB.SyntaxFactory.ParseTypeName("Int16");

            //var newMethodS = methodS.WithReturnType(returnType);

            //Console.WriteLine(newMethodS.ToFullString());
            //Console.WriteLine(newMethodS.ToString());
        }
        public static StringBuilder CodeToCommentRatioVB(string fileNameAndPath)
        {
            StringBuilder sb = new StringBuilder();

            var sourceCode = "";

            using (var sr = new StreamReader(fileNameAndPath))
            {
                sourceCode = sr.ReadToEnd();
            }

            var tree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);

            IEnumerable<SyntaxNode> syntaxNodes;

            // Both of these return the same results.

            //var x1 = tree.GetRoot().DescendantNodes().Where(syn => syn.IsKind(VB.SyntaxKind.ClassBlock));
            //var x2 = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ClassBlockSyntax>();

            //sb.AppendLine("Where(...)");
            //foreach (SyntaxNode node in x1)
            //{
            //    sb.AppendLine(node.ToFullString());
            //}

            //sb.AppendLine("OfType(...)");
            //foreach (SyntaxNode node in x2)
            //{
            //    sb.AppendLine(node.ToFullString());
            //}

            //var x3 = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ClassBlockSyntax>()
            //    .Cast<VB.Syntax.ClassBlockSyntax>()
            //    .Select(c =>
            //       new
            //       {
            //           ClassName = c.BlockStatement.Identifier,
            //           Methods = c.Members.OfType<VB.Syntax.MethodBlockSyntax>()
            //       });

            //foreach (var node in x3)
            //{
            //    sb.AppendLine(node.ClassName.Text);
            //    //sb.AppendLine(node.ClassName.Value.ToString());

            //    foreach (var method in node.Methods)
            //    {

            //        sb.AppendLine(method.ToString());
            //    }
            //}

            //var x4 = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ClassBlockSyntax>()
            //    .Cast<VB.Syntax.ClassBlockSyntax>()
            //    .Select(c =>
            //       new
            //       {
            //           ClassName = c.BlockStatement.Identifier,
            //           Methods = c.Members.OfType<VB.Syntax.MethodBlockSyntax>()
            //       });

            //foreach (var node in x4)
            //{
            //    //sb.AppendLine(node.ClassName.Text);
            //    sb.AppendLine(node.ClassName.Value.ToString());

            //    foreach (var method in node.Methods)
            //    {
            //        sb.AppendLine("Method");
            //        VB.Syntax.MethodStatementSyntax statement = method.DescendantNodes().First() as VB.Syntax.MethodStatementSyntax;
            //        sb.AppendLine(statement.Identifier.ToString());
            //        sb.AppendLine("Parameters");
            //        sb.AppendLine(statement.ParameterList.ToString());
            //    }
            //}

            var x5 = tree.GetRoot().DescendantNodes().OfType<VB.Syntax.ClassBlockSyntax>()
                .Cast<VB.Syntax.ClassBlockSyntax>()
                .Select(c =>
                   new
                   {
                       ClassName = c.BlockStatement.Identifier,
                       Methods = c.Members.OfType<VB.Syntax.MethodBlockSyntax>()
                   })
                .Select(t =>
                    new
                    {
                        ClassName = t.ClassName,
                        MethodDetails = t.Methods
                            .Select(m =>
                               new
                               {
                                   Name = ((VB.Syntax.MethodStatementSyntax)m.DescendantNodes().First()).Identifier.ValueText,
                                   Lines = m.Statements.Count,
                                   Comments = m.DescendantTrivia().Count(b => b.IsKind(VB.SyntaxKind.CommentTrivia))
                               }
                            )
                    });

            foreach (var item in x5)
            {
                sb.AppendLine(item.ClassName.Text);

                foreach (var detail in item.MethodDetails)
                {
                    sb.AppendLine(string.Format("   {0,-40}   Statements:{1,5}    Comments:{2,5}", 
                        detail.Name,
                        detail.Lines.ToString(),
                        detail.Comments.ToString()));
                }
            }

            return sb;

        }

        #endregion

        //private static void Method8()
        //{

        //    var emptyClassTree = SimpleGenerator.CreateEmptyClass("GreetingBusinessRule");
        //    var emptyClass = emptyClassTree.GetRoot().DescendantNodes()
        //        .OfType<ClassDeclarationSyntax>().FirstOrDefault();

        //    if (emptyClass == null)
        //    {
        //        return;
        //    }

        //    Console.WriteLine(emptyClass.ToFullString());
        //}

        //private static void Method9()
        //{

        //    var emptyClassTree = SimpleGenerator.CreateEmptyClass("GreetingBusinessRule");
        //    var emptyClass = emptyClassTree.GetRoot().DescendantNodes()
        //        .OfType<ClassDeclarationSyntax>().FirstOrDefault();

        //    if (emptyClass == null)
        //    {
        //        return;
        //    }

        //    var reference = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
        //    var compilation = CSharpCompilation.Create("internal").WithReferences(reference);

        //    var intType = compilation.GetTypeByMetadataName("System.Int32");
        //    var stringType = compilation.GetTypeByMetadataName("System.String");
        //    var dateTimeType = compilation.GetTypeByMetadataName("System.DateTime");

        //    emptyClass = emptyClass
        //        .AddProperty("Age", intType)
        //        .AddProperty("FirstName", stringType)
        //        .AddProperty("LastName", stringType)
        //        .AddProperty("DateOfBirth", dateTimeType)
        //        .NormalizeWhitespace();

        //    Console.WriteLine(emptyClass.ToFullString());
        //}
    }
}
