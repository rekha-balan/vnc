using System;
using System.Collections.Generic;
using System.Linq;
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

        private void LoadControlContents()
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

        private void XXX_Picker_ControlChanged()
        {

        }


        private void wucSourceBranch_Picker_ControlChanged()
        {
            teRepositoryName.Text = wucSourceBranch_Picker.Name;
            teRepository.Text = wucSourceBranch_Picker.Repository;
            teRepositoryPath.Text = wucSourceBranch_Picker.SourcePath;
            teSourcePath.Text = teRepositoryPath.Text;

            cbeSolutionFile.Items.Clear();
            cbeSolutionFile.ItemsSource = wucSourceBranch_Picker.xElement.Elements("Solution");
            //UpdateSolutionPicker(wucSourceBranch_Picker.xElement);
        }

        private void cbeSolutionFile_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            var a = sender.GetType();
            var b = e.GetType();
            var c = e.NewValue;
            XElement solution = (XElement)e.NewValue;

            cbeProjectFile.Items.Clear();
            cbeProjectFile.ItemsSource = solution.Elements("Project");

            string fileName = solution.Attribute("FileName").Value;
            string folderPath = solution.Attribute("FolderPath").Value;

            teSolutionFile.Text = teRepositoryPath.Text + "\\" + folderPath + "\\" + fileName;
            teSourcePath.Text = teRepositoryPath.Text + "\\" + folderPath + "\\";
        }

        private void cbeProjectFile_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            XElement project = (XElement)e.NewValue;

            cbeSourceFile.Items.Clear();
            cbeSourceFile.ItemsSource = project.Elements("SourceFile");

            string fileName = project.Attribute("FileName").Value;
            string folderPath = project.Attribute("FolderPath").Value;
            string sourcePath = teRepositoryPath.Text + "\\" + folderPath;

            teSourcePath.Text = sourcePath + "\\";

            // Clear if no project file.  This is typical if web site with only a solution file.

            teProjectFile.Text = fileName != "" ? sourcePath + "\\" + fileName : "";
        }

        private void cbeSourceFile_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            XElement sourceFile = (XElement)e.NewValue;
            string fileName = sourceFile.Attribute("FileName").Value;
            string folderPath = sourceFile.Attribute("FolderPath").Value;
            string filePath = (folderPath != "" ? folderPath + "\\" : "") + fileName;

            teSourceFile.Text = teSourcePath.Text + filePath;
        }
    }
}
