using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Microsoft.CodeAnalysis.MSBuild;

namespace VNC.CodeAnalysis.Workspace
{
    public static class Helper
    {
        public static List<String> GetSourceFilesToProcessFromConfigFile(string configFileFullPath, string branchName, string solutionName, string projectName)
        {
            List<String> filesToProcess = new List<string>();

            // This  method returns a list of files to process.
            // If a specific SourceFile is specified, return it
            //
            // If multiple SolutionFiles are selected, the user must select
            //  one or more project files.  Handle as multiple project files
            //
            // If multiple ProjectFiles are selected, 
            //  Loop across <Project> elements 
            //      and add files from project
            //      and add files listed in <Project> elements
            //
            // If a ProjectFile is available, use it to get the list of files
            // Otherwise return the files selected in cbeSourceFiles.


            XElement configFileElements = XElement.Load(new Uri(configFileFullPath).ToString());
            XElement projectElements = configFileElements;


            //string solutionFullPath = teSolutionFile.Text;
            //string projectFullPath = teProjectFile.Text;

            //if (teSourceFile.Text != "")
            //{
            //    // TODO(crhodes)
            //    // Add check for existence
            //    filesToProcess.Add(teSourceFile.Text);
            //}
            //else if (cbeProjectFile.SelectedItems.Count > 1)
            //{
            //    using (var workSpace = MSBuildWorkspace.Create())
            //    {
            //        foreach (XElement projectElement in cbeProjectFile.SelectedItems)
            //        {
            //            string fileName = projectElement.Attribute("FileName").Value;
            //            string folderPath = projectElement.Attribute("FolderPath").Value;
            //            string sourcePath = teRepositoryPath.Text + "\\" + folderPath;
            //            string projectPath = "";

            //            projectPath = fileName != "" ? sourcePath + "\\" + fileName : "";

            //            if (projectPath == "")
            //            {
            //                // No project file exists, so look across all the SourceFile elements

            //                foreach (XElement sourceFile in projectElement.Elements("SourceFile"))
            //                {
            //                    //string sourceFileName = sourceFile.Attribute("FileName").Value;
            //                    //string sourceFolderPath = sourceFile.Attribute("FolderPath").Value;

            //                    //string filePath = sourcePath + "\\" + sourceFolderPath + "\\" + sourceFileName;

            //                    // NB. The file names are added manually so we don't have to exclude any.
            //                    string fileFullPath = sourcePath + "\\" + GetFilePath(sourceFile);

            //                    filesToProcess.Add(fileFullPath);
            //                }
            //            }
            //            else
            //            {
            //                var project = workSpace.OpenProjectAsync(projectPath).Result;

            //                //Microsoft.CodeAnalysis.Project project = null;

            //                //project = Task.Run(async () => project = await workSpace.OpenProjectAsync(projectPath)).Result;


            //                //Microsoft.CodeAnalysis.Project project = null;

            //                //var foo = Task.Run(async () => project = await workSpace.OpenProjectAsync(projectPath));

            //                //foo.Wait();

            //                AddFilesFromProject(filesToProcess, project);
            //            }
            //        }
            //    }
            //}
            //else if (projectFullPath != "")
            //{
            //    using (var workSpace = MSBuildWorkspace.Create())
            //    {

            //        var project = workSpace.OpenProjectAsync(projectFullPath).Result;

            //        //Microsoft.CodeAnalysis.Project project = null;

            //        //Task.Run(async () => project = await workSpace.OpenProjectAsync(projectFullPath));

            //        AddFilesFromProject(filesToProcess, project);
            //    }
            //}
            //else if (cbeSourceFile.SelectedItems.Count > 0)
            //{
            //    string sourcePath = teSourcePath.Text;

            //    foreach (XElement sourceFile in cbeSourceFile.SelectedItems)
            //    {
            //        string fileFullPath = sourcePath + "\\" + GetFilePath(sourceFile);

            //        filesToProcess.Add(fileFullPath);
            //    }
            //}

            //var filesToCheck = filesToProcess.ToList();

            //foreach (string filePath in filesToCheck)
            //{
            //    if (!File.Exists(filePath))
            //    {
            //        FileInfo fileInfo = new FileInfo(filePath);

            //        if (!Directory.Exists(fileInfo.DirectoryName))
            //        {
            //            MessageBox.Show(string.Format("Directory\n\n{0}\n\ndoes not exist", fileInfo.DirectoryName), "Check Path or Config File");
            //        }
            //        else
            //        {
            //            MessageBox.Show(string.Format("File\n\n{0}\nin\n\n{1}\n\ndoes not exist", fileInfo.Name, fileInfo.DirectoryName), "Check Path or Config File");
            //        }

            //        filesToProcess.Remove(filePath);
            //    }
            //}

            return filesToProcess;
        }

        public static List<String> GetSourceFilesToProcessFromVSProject(string projectFullPath)
        {
            List<String> filesToProcess = new List<string>();


            // This  method returns a list of files to process.
            // If a specific SourceFile is specified, return it
            //
            // If multiple SolutionFiles are selected, the user must select
            //  one or more project files.  Handle as multiple project files
            //
            // If multiple ProjectFiles are selected, 
            //  Loop across <Project> elements 
            //      and add files from project
            //      and add files listed in <Project> elements
            //
            // If a ProjectFile is available, use it to get the list of files
            // Otherwise return the files selected in cbeSourceFiles.

            //string solutionFullPath = teSolutionFile.Text;
            //string projectFullPath = teProjectFile.Text;

            //if (teSourceFile.Text != "")
            //{
            //    // TODO(crhodes)
            //    // Add check for existence
            //    filesToProcess.Add(teSourceFile.Text);
            //}
            //else if (cbeProjectFile.SelectedItems.Count > 1)
            //{
            //    using (var workSpace = MSBuildWorkspace.Create())
            //    {
            //        foreach (XElement projectElement in cbeProjectFile.SelectedItems)
            //        {
            //            string fileName = projectElement.Attribute("FileName").Value;
            //            string folderPath = projectElement.Attribute("FolderPath").Value;
            //            string sourcePath = teRepositoryPath.Text + "\\" + folderPath;
            //            string projectPath = "";

            //            projectPath = fileName != "" ? sourcePath + "\\" + fileName : "";

            //            if (projectPath == "")
            //            {
            //                // No project file exists, so look across all the SourceFile elements

            //                foreach (XElement sourceFile in projectElement.Elements("SourceFile"))
            //                {
            //                    //string sourceFileName = sourceFile.Attribute("FileName").Value;
            //                    //string sourceFolderPath = sourceFile.Attribute("FolderPath").Value;

            //                    //string filePath = sourcePath + "\\" + sourceFolderPath + "\\" + sourceFileName;

            //                    // NB. The file names are added manually so we don't have to exclude any.
            //                    string fileFullPath = sourcePath + "\\" + GetFilePath(sourceFile);

            //                    filesToProcess.Add(fileFullPath);
            //                }
            //            }
            //            else
            //            {
            //                var project = workSpace.OpenProjectAsync(projectPath).Result;

            //                //Microsoft.CodeAnalysis.Project project = null;

            //                //project = Task.Run(async () => project = await workSpace.OpenProjectAsync(projectPath)).Result;


            //                //Microsoft.CodeAnalysis.Project project = null;

            //                //var foo = Task.Run(async () => project = await workSpace.OpenProjectAsync(projectPath));

            //                //foo.Wait();

            //                AddFilesFromProject(filesToProcess, project);
            //            }
            //        }
            //    }
            //}
            //else 
            if (projectFullPath != "")
            {
                using (var workSpace = MSBuildWorkspace.Create())
                {

                    var project = workSpace.OpenProjectAsync(projectFullPath).Result;

                    //Microsoft.CodeAnalysis.Project project = null;

                    //Task.Run(async () => project = await workSpace.OpenProjectAsync(projectFullPath));

                    FileInfo fileInfo = new FileInfo(projectFullPath);

                    switch (fileInfo.Extension)
                    {
                        case ".csproj":
                            AddSourceFilesFromCSProject(filesToProcess, project);
                            break;

                        case ".vbproj":
                            AddSourceFilesFromVBProject(filesToProcess, project);
                            break;

                        default:
                            // TODO(crhodes)
                            // How to handle unsupported project types
                            break;
                    }

                }
            }

            //else if (cbeSourceFile.SelectedItems.Count > 0)
            //{
            //    string sourcePath = teSourcePath.Text;

            //    foreach (XElement sourceFile in cbeSourceFile.SelectedItems)
            //    {
            //        string fileFullPath = sourcePath + "\\" + GetFilePath(sourceFile);

            //        filesToProcess.Add(fileFullPath);
            //    }
            //}

            var filesToCheck = filesToProcess.ToList();

            foreach (string filePath in filesToCheck)
            {
                if (!File.Exists(filePath))
                {
                    FileInfo fileInfo = new FileInfo(filePath);

                    //if (!Directory.Exists(fileInfo.DirectoryName))
                    //{
                    //    MessageBox.Show(string.Format("Directory\n\n{0}\n\ndoes not exist", fileInfo.DirectoryName), "Check Path or Config File");
                    //}
                    //else
                    //{
                    //    MessageBox.Show(string.Format("File\n\n{0}\nin\n\n{1}\n\ndoes not exist", fileInfo.Name, fileInfo.DirectoryName), "Check Path or Config File");
                    //}

                    filesToProcess.Remove(filePath);
                }
            }

            return filesToProcess;
        }


        private static void AddSourceFilesFromVBProject(List<string> filesToProcess, Microsoft.CodeAnalysis.Project project)
        {
            foreach (var document in project.Documents)
            {
                string filePath = document.FilePath;

                // Skip a few files

                if (filePath.ToLower().Contains("designer"))
                {
                    continue;
                }

                if (filePath.Contains("My Project"))
                {
                    continue;
                }

                if (document.Name == "AssemblyInfo.vb")
                {
                    continue;
                }

                // Add everything else

                if (document.Name.EndsWith(".vb"))
                {
                    filesToProcess.Add(filePath);
                }
            }
        }

        private static void AddSourceFilesFromCSProject(List<string> filesToProcess, Microsoft.CodeAnalysis.Project project)
        {
            foreach (var document in project.Documents)
            {
                string filePath = document.FilePath;

                // Skip a few files

                if (filePath.ToLower().Contains("designer"))
                {
                    continue;
                }

                if (document.Name == "AssemblyInfo.cs")
                {
                    continue;
                }

                // Add everything else

                if (document.Name.EndsWith(".cs"))
                {
                    filesToProcess.Add(filePath);
                }
            }
        }
    }
}
