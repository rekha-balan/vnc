using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using Microsoft.Office.Interop.Excel;

using Microsoft.SqlServer.Management.Common;
using SMO = Microsoft.SqlServer.Management.Smo;
using SMOWMI = Microsoft.SqlServer.Management.Smo.Wmi;

using SMOH = VNC.SMOHelper;
using XlHlp = VNC.AddinHelper.Excel;

using System.Text.RegularExpressions;

namespace SupportTools_Excel.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucTaskPane_Utilities.xaml
    /// </summary>
    public partial class wucTaskPane_Utilities : UserControl
    {
        #region Fields and Properties


        #endregion

        #region Constructors and Load

        public wucTaskPane_Utilities()
        {
            InitializeComponent();
            LoadControlContents();
        }

        private void LoadControlContents()
        {
            //try
            //{
            //    wucSQLInstance_Picker1.PopulateControlFromFile(Common.cCONFIG_FILE);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //wucSQLInstance_Picker1.ControlChanged += WucSQLInstance_Picker1_ControlChanged;
            //wucTFSProvider_Picker.ControlChanged += tfsProvider_Picker1_ControlChanged;
        }


        #endregion

        #region Event Handlers

        private void btnCreateDataValidationTable_Click(object sender, RoutedEventArgs e)
        {
            CreateDataValidationTable();
        }

        #endregion

        #region Main Function Routines

        private void CreateDataValidationTable()
        {
            string listName;
            string tableName;
            string validationName;
            string refersTo;

            try
            {
                Range rng = Globals.ThisAddIn.Application.Selection;

                listName = rng[1, 1].Value;
                tableName = GetTableName(listName);
                validationName = GetValidationName(listName);
                refersTo = String.Format("={0}[[{1}]]", tableName, listName);

                CreateTableFromSelection(rng, tableName);
                CreateValidationNameFromTableName(validationName, refersTo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion



        #region Utility Routines



        #endregion

        #region Private Methods

        private void CreateTableFromSelection(Range rng, string name)
        {
            Worksheet ws = Globals.ThisAddIn.Application.ActiveSheet;

            ws.ListObjects.Add(XlListObjectSourceType.xlSrcRange, rng, null, XlYesNoGuess.xlYes).Name = name;
        }

        private void CreateValidationNameFromTableName(string name, string refersTo)
        {
            Globals.ThisAddIn.Application.ActiveWorkbook.Names.Add(Name: name, RefersToR1C1: refersTo);
        }

        private string GetTableName(string name)
        {
            string tableName = "tbl_" + name.Replace(" ", "");

            return tableName;
        }

        private string GetValidationName(string name)
        {
            string validationName = name.Replace(" ", "");

            return validationName;
        }

        //private bool GetDisplayOrientation()
        //{
        //    return (bool)ceOrientOutputVertically.IsChecked;
        //}

        private bool ValidUISelections()
        {
            //if (cbeTeamProjectCollections.SelectedText.Length > 0)
            //{
            return true;
            //}
            //else
            //{
            //    MessageBox.Show("Must Select Team Project Collection first", "UI Selection Incomplete");
            //    return false;
            //}
        }

        #endregion
    }
}
