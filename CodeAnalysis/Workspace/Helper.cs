﻿using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace VNC.CodeAnalysis.Workspace
{

    public static class Helper
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.HELPER;
        private const string LOG_APPNAME = ErrorNumbers.CLASSNAME;

        public static List<String> GetSourceFilesToProcessFromConfigFile(string configFileFullPath, string branchName, string solutionName, string projectName)
        {

            List<String> filesToProcess = null;
            string sourcePath = string.Empty;
            string projectFolderPath = string.Empty;
            string projectFileName = string.Empty;

            // This  method returns a list of files to process.
            //
            // If a VS ProjectFile is available, use it to get the list of files
            // Otherwise return the files specified in the <SourceFile /> elements

            XElement configFileElement = XElement.Load(new Uri(configFileFullPath).ToString());
            XElement branchElement = configFileElement.Descendants("Branch").Where(attr => attr.Attribute("Name").Value == branchName).First();

            if (branchElement != null)
            {
                sourcePath = branchElement.Attribute("SourcePath").Value;

                // TODO(crhodes)
                // Add some validation         
            }

            XElement solutionElement = branchElement.Descendants("Solution").Where(attr => attr.Attribute("Name").Value == solutionName).First();
            XElement projectElement = solutionElement.Descendants("Project").Where(attr => attr.Attribute("Name").Value == projectName).First();

            if (projectElement != null)
            {
                projectFolderPath = projectElement.Attribute("FolderPath").Value;
                projectFileName = projectElement.Attribute("FileName").Value;
            }

            if (projectFileName.Length > 0)
            {
                // TODO(crhodes)
                // Verify the project file exists.  
                // If so, open it and get the list of files.
                filesToProcess = GetSourceFilesToProcessFromVSProject(sourcePath + "\\" + projectFolderPath + "\\" + projectFileName);
            }
            else
            {
                filesToProcess = new List<string>();

                string projectBasePath = sourcePath + "\\" + projectFolderPath + "\\";

                foreach (XElement sourceFile in projectElement.Descendants("SourceFile"))
                {
                    string sourceFolderPath = sourceFile.Attribute("FolderPath").Value;

                    if (sourceFolderPath.Length > 0)
                    {
                        sourceFolderPath += "\\";
                    }

                    string sourceFullPath = projectBasePath + sourceFolderPath + sourceFile.Attribute("FileName").Value;

                    if (File.Exists(sourceFullPath))
                    {
                        filesToProcess.Add(sourceFullPath);
                    }
                    else
                    {                      
                        // TODO(crhodes)
                        // Handle errors.

                        //FileInfo fileInfo = new FileInfo(filePath);

                        //if (!Directory.Exists(fileInfo.DirectoryName))
                        //{
                        //    MessageBox.Show(string.Format("Directory\n\n{0}\n\ndoes not exist", fileInfo.DirectoryName), "Check Path or Config File");
                        //}
                        //else
                        //{
                        //    MessageBox.Show(string.Format("File\n\n{0}\nin\n\n{1}\n\ndoes not exist", fileInfo.Name, fileInfo.DirectoryName), "Check Path or Config File");
                        //}
                    }
                }
            }

            return filesToProcess;
        }

        public static List<String> GetSourceFilesToProcessFromVSProject(string projectFullPath)
        {
            VNC.AppLog.Trace(string.Format("projectFullPath:({0})", projectFullPath), LOG_APPNAME);

            List<String> filesToProcess = new List<string>();


            // This  method returns a list of files to process.

            // If a ProjectFile is available, use it to get the list of files
            // Otherwise return the files selected in cbeSourceFiles.

            try
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
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME);
            }

            return filesToProcess;
        }

        public static void AddSourceFilesFromVBProject(List<string> filesToProcess, Microsoft.CodeAnalysis.Project project)
        {
            VNC.AppLog.Trace1(string.Format("Enter: project.Path:({0})", project.FilePath), LOG_APPNAME);

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

            VNC.AppLog.Trace(string.Format("Exit: Count({0})", filesToProcess.Count), LOG_APPNAME);
        }

        public static void AddSourceFilesFromCSProject(List<string> filesToProcess, Microsoft.CodeAnalysis.Project project)
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
