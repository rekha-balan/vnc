using DevExpress.Xpf.Editors;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;

namespace VNCCodeCommandConsole.User_Interface.User_Controls
{
    public partial class wucCodeExplorerContext : wucDXBase
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        #region Constructors

        public wucCodeExplorerContext()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif
        }

        #endregion

        #region Initialization

        internal override void LoadControlContents()
        {
            try
            {
                wucSourceBranch_Picker.PopulateControlFromFile(Common.cCONFIG_FILE);
                //wucSourceBranch_Picker.PopulateControlFromFile(Common.cCONFIG_FILE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        internal override void OnLoaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            // Cheat and force outcome if not using dat
            Common.DataFullyLoaded = true;
            Helper.ValidateDataFullyLoaded();
            LoadControlContents();

            //wucSourceBranch_Picker.ControlChanged += WucSourceBranch_Picker_ControlChanged;

            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    //dataGrid.ItemsSource = VNCWPFUserControls.Common.ApplicationDataSet.ApplicationUsage;
                }

                //ViewMode.ApplyAuthorization(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif
        }

        #endregion

        #region Event Handlers

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            //CustomFormat.FormatStorageColumns(e);
        }

        #endregion

        private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            //UnboundColumns.GetEnvironmentInstanceDatabaseColumns(e);
        }

        #region Main Function Routines



        #endregion

        //private void XXX_Picker_ControlChanged()
        //{

        //}

        private void wucSourceBranch_Picker_ControlChanged()
        {
            try
            {
                teRepositoryName.Text = wucSourceBranch_Picker.Name;
                teRepository.Text = wucSourceBranch_Picker.Repository;
                teRepositoryPath.Text = wucSourceBranch_Picker.SourcePath;
                teSourcePath.Text = teRepositoryPath.Text;

                cbeSourceFile.Items.Clear();
                cbeSourceFile.Clear();
                cbeSourceFile.ItemsSource = null;

                cbeProjectFile.Items.Clear();
                cbeProjectFile.Clear();
                cbeProjectFile.ItemsSource = null;

                cbeSolutionFile.Items.Clear();
                cbeSolutionFile.Clear();
                cbeSolutionFile.ItemsSource = wucSourceBranch_Picker.xElement.Elements("Solution");
                //UpdateSolutionPicker(wucSourceBranch_Picker.xElement);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbeSolutionFile_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                var s = (ComboBoxEdit)sender;

                var a = sender.GetType();
                var b = e.GetType();
                var c = e.NewValue;

                if (s.SelectedItems.Count() == 1)
                {
                    XElement solution = (XElement)s.SelectedItem;

                    cbeSourceFile.Items.Clear();
                    cbeSourceFile.Clear();
                    cbeSourceFile.ItemsSource = null;

                    cbeProjectFile.Items.Clear();
                    cbeProjectFile.Clear();
                    cbeProjectFile.ItemsSource = solution.Elements("Project");

                    string fileName = solution.Attribute("FileName").Value;
                    string folderPath = solution.Attribute("FolderPath").Value;

                    teSolutionFile.Text = teRepositoryPath.Text + "\\" + folderPath + "\\" + fileName;
                    teSourcePath.Text = teRepositoryPath.Text + "\\" + folderPath + "\\";

                }
                else
                {
                    // Have selected multiple solution files, so clear out controls that affect GetFilesToProc()

                    List<XElement> projectElements = new List<XElement>();

                    foreach (XElement solutionElement in cbeSolutionFile.SelectedItems)
                    {
                        foreach (XElement projectElement in solutionElement.Elements("Project"))
                        {
                            projectElements.Add(projectElement);
                        }
                    }

                    cbeProjectFile.Items.Clear();
                    cbeProjectFile.Clear();
                    cbeProjectFile.ItemsSource = projectElements;
                    teProjectFile.Clear();

                    cbeSourceFile.Items.Clear();
                    cbeSourceFile.Clear();
                    cbeSourceFile.ItemsSource = null;
                    teSourceFile.Clear();

                    teSourcePath.Clear();   // not sure if this should be cleared.

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbeProjectFile_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                var s = (ComboBoxEdit)sender;
                var e1 = e.NewValue;
                var e2 = e.OldValue;

                if (s.SelectedItems.Count() == 1)
                {
                    XElement project = (XElement)s.SelectedItem; ;

                    cbeSourceFile.Items.Clear();
                    cbeSourceFile.Clear();
                    cbeSourceFile.ItemsSource = project.Elements("SourceFile");

                    string fileName = project.Attribute("FileName").Value;
                    string folderPath = project.Attribute("FolderPath").Value;
                    string sourcePath = teRepositoryPath.Text + "\\" + folderPath;

                    teSourcePath.Text = sourcePath + "\\";

                    // Set ProjectFile path or clear if no project file.  
                    // This is typical if web site with only a solution file.

                    teProjectFile.Text = fileName != "" ? sourcePath + "\\" + fileName : "";
                }
                else
                {
                    // Have selected multiple project files, so clear out controls that affect GetFilesToProcess()

                    cbeSourceFile.Items.Clear();
                    cbeSourceFile.Clear();
                    teProjectFile.Clear();
                    teSourcePath.Clear();
                    teSourceFile.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbeSourceFile_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                var s = (ComboBoxEdit)sender;
                var e1 = e.NewValue;
                var e2 = e.OldValue;

                var e3 = e1.GetType();

                if (s.SelectedItems.Count() == 1)
                {
                    XElement sourceFileElement = (XElement)s.SelectedItem;
                    string filePath = GetFilePath(sourceFileElement);
                    teSourceFile.Text = teSourcePath.Text + filePath;
                }
                else
                {
                    teSourceFile.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static string GetFilePath(XElement sourceFileElement)
        {
            string fileName = sourceFileElement.Attribute("FileName").Value;
            string folderPath = sourceFileElement.Attribute("FolderPath").Value;
            string filePath = (folderPath != "" ? folderPath + "\\" : "") + fileName;

            return filePath;
        }

        /// <summary>
        /// Returns list of files to process based on selections
        /// </summary>
        /// <returns></returns>
        public List<String> GetFilesToProcess()
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

            string solutionFullPath = teSolutionFile.Text;
            string projectFullPath = teProjectFile.Text;
            
            if (teSourceFile.Text != "")
            {
                // TODO(crhodes)
                // Add check for existence
                filesToProcess.Add(teSourceFile.Text);
            }
            else if (cbeProjectFile.SelectedItems.Count > 1)
            {
                using (var workSpace = MSBuildWorkspace.Create())
                {
                    foreach (XElement projectElement in cbeProjectFile.SelectedItems)
                    {
                        string fileName = projectElement.Attribute("FileName").Value;
                        string folderPath = projectElement.Attribute("FolderPath").Value;
                        string sourcePath = teRepositoryPath.Text + "\\" + folderPath;
                        string projectPath = "";

                        projectPath = fileName != "" ? sourcePath + "\\" + fileName : "";

                        if (projectPath == "")
                        {
                            // No project file exists, so look across all the SourceFile elements

                            foreach (XElement sourceFile in projectElement.Elements("SourceFile"))
                            {
                                //string sourceFileName = sourceFile.Attribute("FileName").Value;
                                //string sourceFolderPath = sourceFile.Attribute("FolderPath").Value;

                                //string filePath = sourcePath + "\\" + sourceFolderPath + "\\" + sourceFileName;

                                // NB. The file names are added manually so we don't have to exclude any.
                                string fileFullPath = sourcePath + "\\" + GetFilePath(sourceFile);

                                filesToProcess.Add(fileFullPath);
                            }
                        }
                        else
                        {
                            var project = workSpace.OpenProjectAsync(projectPath).Result;

                            //Microsoft.CodeAnalysis.Project project = null;

                            //project = Task.Run(async () => project = await workSpace.OpenProjectAsync(projectPath)).Result;


                            //Microsoft.CodeAnalysis.Project project = null;

                            //var foo = Task.Run(async () => project = await workSpace.OpenProjectAsync(projectPath));

                            //foo.Wait();

                            AddFilesFromProject(filesToProcess, project);
                        }
                    }
                }
            }
            else if (projectFullPath != "")
            {
                using (var workSpace = MSBuildWorkspace.Create())
                {

                    var project = workSpace.OpenProjectAsync(projectFullPath).Result;

                    //Microsoft.CodeAnalysis.Project project = null;

                    //Task.Run(async () => project = await workSpace.OpenProjectAsync(projectFullPath));

                    AddFilesFromProject(filesToProcess, project);
                }
            }
            else if (cbeSourceFile.SelectedItems.Count > 0)
            {
                string sourcePath = teSourcePath.Text;

                foreach (XElement sourceFile in cbeSourceFile.SelectedItems)
                {
                    string fileFullPath = sourcePath + "\\" + GetFilePath(sourceFile);

                    filesToProcess.Add(fileFullPath);
                }
            }

            var filesToCheck = filesToProcess.ToList();

            foreach (string filePath in filesToCheck)
            {
                if (! File.Exists(filePath))
                {
                    FileInfo fileInfo = new FileInfo(filePath);

                    if (! Directory.Exists(fileInfo.DirectoryName))
                    {
                        MessageBox.Show(string.Format("Directory\n\n{0}\n\ndoes not exist", fileInfo.DirectoryName), "Check Path or Config File");
                    }
                    else
                    {
                        MessageBox.Show(string.Format("File\n\n{0}\nin\n\n{1}\n\ndoes not exist", fileInfo.Name, fileInfo.DirectoryName), "Check Path or Config File");
                    }

                    filesToProcess.Remove(filePath);
                }
            }

            return filesToProcess;
        }

        private static void AddFilesFromProject(List<string> filesToProcess, Microsoft.CodeAnalysis.Project project)
        {
            foreach (var document in project.Documents)
            {
                string filePath = document.FilePath;

                if (filePath.ToLower().Contains("designer"))
                {
                    continue;
                }

                if (filePath.Contains("My Project"))
                {
                    continue;
                }

                if (document.Name == "Assembly.vb")
                {
                    continue;
                }

                if (document.Name.EndsWith(".vb"))
                {
                    filesToProcess.Add(filePath);
                }
            }
        }

        private void btnBrowseForFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "VB Source (*.vb)|*.vb|CS Source (*.cs)|*.cs|All files (*.*)|*.*";
            //openFileDialog1.Filter = "A files (*.xml)|*.xml|All files (*.*)|*.*";

            openFileDialog1.FileName = "";

            if (true == openFileDialog1.ShowDialog())
            {
                string fileName = openFileDialog1.FileName;

                teSourceFile.Text = fileName;
            }
        }
    }
}
