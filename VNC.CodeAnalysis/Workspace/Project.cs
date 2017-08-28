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
    public class Project
    {
        public static StringBuilder Display(string projectFullPath)
        {
            var workSpace = MSBuildWorkspace.Create();
            var project = workSpace.OpenProjectAsync(projectFullPath).Result;

            return Display(project);
        }

        static StringBuilder Display(Microsoft.CodeAnalysis.Project project)
        {
            StringBuilder sb = new StringBuilder();
            // Print the root of the solution

            sb.AppendLine(Path.GetFileName(project.FilePath));

            sb.AppendLine("> " + project.Name);

            sb.AppendLine("  > MetadataReferences");

            foreach (var reference in project.MetadataReferences)
            {
                sb.AppendLine("     - " + reference.Display);
            }

            //sb.AppendLine("  > ProjectReferences");

            Microsoft.CodeAnalysis.Solution solution = project.Solution;

            foreach (var reference in project.ProjectReferences)
            {
                sb.AppendLine("     - " + solution.GetProject(reference.ProjectId).Name);
            }

            sb.AppendLine("  > Documents");

            foreach (var document in project.Documents)
            {
                sb.AppendLine("    - " + document.Name + " " + document.GetType().ToString());
            }

            return sb;
        }
    }
}
