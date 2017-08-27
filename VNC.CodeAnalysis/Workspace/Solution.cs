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

using Microsoft.CodeAnalysis.VisualBasic;

namespace VNC.CodeAnalysis.Workspace
{
    public class Solution
    {
        public static StringBuilder ListInfo(string solutionFullPath)
        {
            StringBuilder sb = new StringBuilder();

            var workSpace = MSBuildWorkspace.Create();
            var solution = workSpace.OpenSolutionAsync(solutionFullPath).Result;

            // Classify(workSpace, solution);
            // Formatting(solution);
            // SymbolFinding(solution);
            // Recommend(workSpace, solution);
            // Rename(workSpace, solution);
            // Simplification(solution);
            PrintInfo(solution, sb);

            return sb;
        }

        static StringBuilder PrintInfo(Microsoft.CodeAnalysis.Solution solution, StringBuilder sb)
        {
            // Print the root of the solution

           sb.AppendLine(Path.GetFileName(solution.FilePath));

            sb.AppendLine("Projects:");

            foreach (var project in solution.Projects)
            {
                sb.AppendLine("  - " + project.Name);
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

                sb.AppendLine("> " + project.Name);

                sb.AppendLine("  > MetadataReferences");

                foreach (var reference in project.MetadataReferences)
                {
                    sb.AppendLine("     - " + reference.Display);
                }

                sb.AppendLine("  > ProjectReferences");

                foreach (var reference in project.ProjectReferences)
                {
                    sb.AppendLine("     - " + solution.GetProject(reference.ProjectId).Name);
                }

                sb.AppendLine("  > Documents");

                foreach (var document in project.Documents)
                {
                    sb.AppendLine("    - " + document.Name + " " + document.GetType().ToString());
                }
            }

            return sb;
        }
    }
}
