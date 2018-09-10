
using Microsoft.CodeAnalysis.MSBuild;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        public static IEnumerable<Microsoft.CodeAnalysis.Document> SourceFile(string projectFullPath)
        {
            var workSpace = MSBuildWorkspace.Create();
            var project = workSpace.OpenProjectAsync(projectFullPath).Result;

            // TODO(crhodes)
            // Do something to handle appropriate .vb or .cs files
            // For now just return all documents

            return project.Documents;
        }
    }
}
