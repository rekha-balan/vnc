using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using Microsoft.Office.Interop.Excel;
using Microsoft.SharePoint.Client;
using Microsoft.Win32;

//using XlHlp = VNC.AddinHelper.Excel;
using XlHlp = VNC.AddinHelper.Excel;
using VNCSP = VNC.SP;

namespace SupportTools_Excel.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for TaskPane_SharePoint_WPF.xaml
    /// </summary>
    public partial class wucTaskPane_SharePoint : UserControl
    {


        #region Constructors and Load
        
        public wucTaskPane_SharePoint()
        {
            InitializeComponent();
            LoadControlContents();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            spSiteCollection_Picker.ControlChanged += spSiteCollection_Picker_ControlChanged;
        }

        #endregion
        
        #region Event Handlers

        private void btnCreateContentType_Click(object sender, RoutedEventArgs e)
        {
            var sUri = teSiteUri.Text;

            string schemaXml = "<Field Type='Text' DisplayName='VNCSiteCol1' Name='VNCSiteCol1' Group='VNC' />";

            using (var ctx = new ClientContext(sUri))
            {
                var rootWeb = ctx.Site.RootWeb;

                try
                {
                    rootWeb.ContentTypes.Add(new ContentTypeCreationInformation
                    {
                        Name = "VNCContentType3",
                        Group = "VNC"
                    });

                    ctx.ExecuteQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                try
                {
                    var parentContentTypes = ctx.LoadQuery(rootWeb.ContentTypes.Where(ct => ct.Name == "Page"));
                    ctx.ExecuteQuery();

                    var parentContentType = parentContentTypes.FirstOrDefault();

                    if (parentContentType != null)
                    {
                        rootWeb.ContentTypes.Add(new ContentTypeCreationInformation
                        {
                            Name = "VNCContentType4",
                            Group = "VNC",
                            ParentContentType = parentContentType
                        });

                        ctx.ExecuteQuery();
                    }
                    else
                    {
                        throw new InvalidOperationException("Parent Content Type not found");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }
        
        private void btnCreateSiteColumn_Click(object sender, RoutedEventArgs e)
        {
            var sUri = teSiteUri.Text;

            //<Field 
            //ID="{56747800-D36E-4625-ABE3-B1BC74A7D5F8}" 
            //Name="LowerValuesAreBetter" 
            //StaticName="LowerValuesAreBetter" 
            //Description="Whether lower is better or higher is better" 
            //Group="Status Indicators" Type="Boolean" 
            //DisplayName="Lower values are better" 
            //SourceID="http://schemas.microsoft.com/sharepoint/v3" />

            string schemaXml = "<Field Type='Text' DisplayName='VNCSiteCol1' Name='VNCSiteCol1' Group='VNC' />";
            using (var ctx = new ClientContext(sUri))
            {
                var rootWeb = ctx.Site.RootWeb;

                VNCSP.Helper.CreateSiteColumn(ctx, rootWeb, schemaXml, AddFieldOptions.AddFieldInternalNameHint);
                //rootWeb.Fields.AddFieldAsXml(schemaXml, true, AddFieldOptions.AddFieldInternalNameHint);
                //ctx.ExecuteQuery();
            }
        }

        private void btnGetSiteCollectionInfo_Click(object sender, RoutedEventArgs e)
        {
            var scUri = spSiteCollection_Picker.Uri;

            try
            {
                XlHlp.ScreenUpdatesOff();

                using (var ctx = new ClientContext(scUri))
                {
                    CreateWorksheet_SiteCollection_Info(ctx, false);
                }

            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }

        }

        private void btnGetSiteInfo_Click(object sender, RoutedEventArgs e)
        {
            var sUri = teSiteUri.Text;

            try
            {
                XlHlp.ScreenUpdatesOff();

                using (var ctx = VNCSP.Helper.GetClientContext(sUri))
                {
                    CreateWorksheet_Web_Info(ctx, ccbeWebInfo.Text, true);
                }

            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        private void btnLinkColumnsToContentTypes_Click(object sender, RoutedEventArgs e)
        {
            var sUri = teSiteUri.Text;

            using (var ctx = new ClientContext(sUri))
            {

                try
                {
                    var rootWeb = ctx.Site.RootWeb;

                    Field sc1 = rootWeb.Fields.GetByInternalNameOrTitle("VNCSiteCol1");

                    var parentContentTypes = ctx.LoadQuery(rootWeb.ContentTypes.Where(ct => ct.Name == "VNCContentType4"));
                    ctx.ExecuteQuery();

                    var ct4 = parentContentTypes.FirstOrDefault();

                    ct4.FieldLinks.Add(new FieldLinkCreationInformation
                    {
                        Field = sc1
                    });

                    ct4.Update(true);

                    ctx.ExecuteQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


        private void btnProvisionSite_Click(object sender, RoutedEventArgs e)
        {
            VNCSP.Helper.ProvisionSite(spSiteCollection_Picker.Uri);
        }

        private void spSiteCollection_Picker_ControlChanged()
        {
            try
            {
                XlHlp.ScreenUpdatesOff();

                teSiteUri.Text = spSiteCollection_Picker.Uri;

                // We just picked a new site collection, perhaps update a list of (sub)sites.
                PopulateSitesPicker(spSiteCollection_Picker.Uri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        #endregion

        #region Main Methods


        #region CreateWorksheet_*

        private void CreateWorksheet_SiteCollection_Info(ClientContext ctx, bool param1)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            var site = ctx.Site;
            var web = ctx.Web;


            ctx.Load(site);
            ctx.ExecuteQuery();

            // Display Site (SiteCollection stuff)

            ctx.Load(web);
            ctx.ExecuteQuery();

            foreach (Web w in web.Webs)
            {
                XlHlp.DisplayInWatchWindow(w.Title);
            }

        }
        private void CreateWorksheet_Web_Info(ClientContext ctx, string sectionsToDisplay, bool orientVertical)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            var web = ctx.Web;

            ctx.Load(web);
            ctx.ExecuteQuery();

            string sheetName = XlHlp.SafeSheetName(string.Format("{0}{1}", "SPWeb>", web.Title));
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 2, column: 1, orientVertical: GetDisplayOrientation());

            if (insertAt.OrientVertical)
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "Web Info", 14, XlHlp.MakeBold.Yes);
                insertAt.IncrementRows();
            }
            else
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "Web Info", 14, XlHlp.MakeBold.Yes, orientation: XlOrientation.xlUpward);
                insertAt.DecrementRows();   // AddRow bumped it.
                insertAt.IncrementColumns();
            }

            if (sectionsToDisplay.Contains("WebInfo"))
            {
                insertAt = AddSection_WebInfo(insertAt, ctx, web);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("Libraries"))
            {
                insertAt = AddSection_Libraries(insertAt, ctx, web);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("Lists"))
            {
                insertAt = AddSection_Lists(insertAt, ctx, web);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("SiteGroups"))
            {
                insertAt = AddSection_SiteGroups(insertAt, ctx, web);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("SiteColumns"))
            {
                insertAt = AddSection_SiteColumns(insertAt, ctx, web);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

            if (sectionsToDisplay.Contains("ContentTypes"))
            {
                insertAt = AddSection_ContentTypes(insertAt, ctx, web);

                insertAt.IncrementPosition(insertAt.OrientVertical);
            }

        }

        #endregion

        #region AddSection_*

        private XlHlp.XlLocation AddSection_ContentTypes(XlHlp.XlLocation insertAt, ClientContext ctx, Web web)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ContentTypes", web.Title);
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ContentTypes", web.Title, orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            ctx.Load(web.ContentTypes);
            ctx.ExecuteQuery();

            //IEnumerable<List> cTypes = ctx.LoadQuery(allContentTypes.Where
            //    (list => list.BaseType == BaseType.DocumentLibrary)
            //    );

            ctx.ExecuteQuery();

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            insertAt = DisplayListOf_ContentTypes(insertAt, web.ContentTypes, false, "Full");

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblContentTypes_{0}", web.Title));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        //public static IEnumerable<List> Get_DocumentLibraries(ClientContext ctx, Web web)
        //{
        //    var alllists = web.Lists;

        //    IEnumerable<List> doclibs = ctx.LoadQuery(alllists.Where
        //        (list => list.BaseType == BaseType.DocumentLibrary)
        //        );

        //    ctx.ExecuteQuery();

        //    return doclibs;
        //}

        private XlHlp.XlLocation AddSection_Libraries(XlHlp.XlLocation insertAt, ClientContext ctx, Web web)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Libraries", web.Title);
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Libraries", web.Title, orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            IEnumerable<List> doclibs = VNC.SP.Helper.GetDocumentLibraries(ctx, web);

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            insertAt = DisplayListOf_Libraries(insertAt, doclibs, false, "OneLevel");

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblLibraries_{0}", web.Title));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        //private static IEnumerable<List> Get_Lists(ClientContext ctx, Web web)
        //{
        //    var alllists = web.Lists;

        //    IEnumerable<List> lists = ctx.LoadQuery(alllists.Where
        //        (list => list.BaseType == BaseType.GenericList)
        //        );

        //    ctx.ExecuteQuery();
        //    return lists;
        //}

        private XlHlp.XlLocation AddSection_Lists(XlHlp.XlLocation insertAt, ClientContext ctx, Web web)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Lists", web.Title);
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Lists", web.Title, orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            IEnumerable<List> lists = VNCSP.Helper.GetLists(ctx, web);

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            insertAt = DisplayListOf_Lists(insertAt, lists, false, "OneLevel");

            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblLists_{0}", web.Title));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_SiteColumns(XlHlp.XlLocation insertAt, ClientContext ctx, Web web)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            if (insertAt.OrientVertical)
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "SiteColumns", web.Title);
            }
            else
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "SiteColumns", web.Title, orientation: XlOrientation.xlUpward);
                insertAt.IncrementColumns();
            }

            var siteColumns = VNCSP.Helper.GetSiteColumns(ctx, web);

            //ctx.Load(web.Fields);
            //ctx.ExecuteQuery();

            //var siteColumns = web.Fields;

            //ctx.ExecuteQuery();


            insertAt.MarkStart(XlHlp.MarkType.GroupTable);


            insertAt = DisplayListOf_SiteColumns(insertAt, siteColumns, false, "OneLevel");


            insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblSiteColumns_{0}", web.Title));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_SiteGroups(XlHlp.XlLocation insertAt, ClientContext ctx, Web web)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            //XlHlp.AddTitledInfo(ws.Cells[startingRow++, 1], "Groups", "SiteName");

            //Range rng = ws.Cells[startingRow, 1];
            //int rowsAdded = 0;

            //var siteGroups = web.SiteGroups;

            ////IEnumerable<List> groups = ctx.LoadQuery(siteGroups.Where(true));

            //ctx.Load(siteGroups);

            //ctx.ExecuteQuery();

            //foreach (var group in siteGroups)
            //{
            //    int col = 0;
            //    //XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], group.LoginName); // Seems same as Title
            //    XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], group.Title);
            //    XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], group.Id.ToString());
            //    //XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], group.Owner.LoginName);
            //    XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], group.PrincipalType.ToString());

            //    rowsAdded++;

            //    UserCollection users = ctx.Web.SiteGroups.GetById(group.Id).Users;

            //    ctx.Load(users);
            //    ctx.ExecuteQuery();

            //    foreach (var user in users)
            //    {
            //        col = 4;
            //        //XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], user.LoginName);
            //        XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], user.Title);
            //        XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], user.Id.ToString());
            //        //XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], user.UserId.ToString());
            //        XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], user.PrincipalType.ToString());
            //        rowsAdded++;
            //    }
            //    rowsAdded++;

            //}

            //insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblSiteGroups_{0}", web.Title));

            //insertAt.Group(insertAt.OrientVertical, hide: true);

            //insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            //XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }


        private XlHlp.XlLocation AddSection_WebInfo(XlHlp.XlLocation insertAt, ClientContext ctx, Web web)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            //ctx.Load(web, info => info.HasUniqueRoleAssignments);
            //ctx.Load(web1, 
            //    info => info.Url, 
            //    info => info.MasterUrl, 
            //    info => info.Description,
            //    info => info.HasUniqueRoleAssignments,
            //    info => info.Created);
            //ctx.ExecuteQuery();

            XlHlp.AddTitledInfo(insertAt.AddOffsetRow(), "Url:", ctx.Url);
            XlHlp.AddTitledInfo(insertAt.AddOffsetRow(), "Title:", web.Title);
            XlHlp.AddTitledInfo(insertAt.AddOffsetRow(), "Created:", web.Created.ToString());
            XlHlp.AddTitledInfo(insertAt.AddOffsetRow(), "Description:", web.Description);
            XlHlp.AddTitledInfo(insertAt.AddOffsetRow(), "Id:", web.Id.ToString());
            //XlHlp.AddTitledInfo(ws.Cells[startingRow++, 1], "HasUniqueRoleAssignments:", web.HasUniqueRoleAssignments.ToString());
            //XlHlp.AddTitledInfo(ws.Cells[startingRow++, 1], "MasterUrl:", web.MasterUrl);

            return insertAt;
        }

        #endregion


        #region DisplayListOf_*

        private XlHlp.XlLocation DisplayListOf_ContentTypes(XlHlp.XlLocation insertAt, ContentTypeCollection contentTypes, bool displayDataOnly, string tableSuffix)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            Worksheet ws = insertAt.workSheet;

            if (!displayDataOnly)
            {
                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 40, "Name");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 30, "Group");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "Id");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "SchemaXml");

                insertAt.IncrementRows();
            }

            foreach (var cType in contentTypes)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), cType.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), cType.Group);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), cType.Id.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), cType.SchemaXml);

                insertAt.IncrementRows();
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_Libraries(XlHlp.XlLocation insertAt, IEnumerable<List> libraries, bool displayDataOnly, string tableSuffix)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            Worksheet ws = insertAt.workSheet;

            if (!displayDataOnly)
            {
                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 40, "Title");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "Id");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "SchemaXml");

                insertAt.IncrementRows();
            }

            foreach (var library in libraries)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), library.Title);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), library.Id.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), library.SchemaXml);

                insertAt.IncrementRows();
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }
        
        private VNC.AddinHelper.Excel.XlLocation DisplayListOf_Lists(VNC.AddinHelper.Excel.XlLocation insertAt, IEnumerable<List> lists, bool displayDataOnly, string tableSuffix)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            Worksheet ws = insertAt.workSheet;

            if (!displayDataOnly)
            {
                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 40, "Title");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "Id");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "SchemaXml");

                insertAt.IncrementRows();
            }

            foreach (var list in lists)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), list.Title);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), list.Id.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), list.SchemaXml);

                insertAt.IncrementRows();
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_SiteColumns(XlHlp.XlLocation insertAt, IEnumerable<Field> siteColumns, bool displayDataOnly, string tableSuffix)
        {
            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt);

            Worksheet ws = insertAt.workSheet;

            //<Field 
            //ID="{56747800-D36E-4625-ABE3-B1BC74A7D5F8}" 
            //Name="LowerValuesAreBetter" 
            //StaticName="LowerValuesAreBetter" 
            //Description="Whether lower is better or higher is better" 
            //Group="Status Indicators" Type="Boolean" 
            //DisplayName="Lower values are better" 
            //SourceID="http://schemas.microsoft.com/sharepoint/v3" />

            //<Field 
            //Type="Lookup" 
            //DisplayName="FA Page Type" 
            //Required="FALSE" 
            //EnforceUniqueValues="FALSE" 
            //List="{b1d6a5ff-876b-4761-a05d-210038e31639}" 
            //WebId="666b287f-2709-476b-a739-e1de9150fb37" 
            //ShowField="Title" 
            //UnlimitedLengthInDocumentLibrary="FALSE" 
            //Group="Custom Columns" 
            //ID="{3090abc3-526c-458f-9c65-302ad853db65}" 
            //SourceID="{666b287f-2709-476b-a739-e1de9150fb37}" 
            //StaticName="FA_x0020_Page_x0020_Type" 
            //Name="FA_x0020_Page_x0020_Type" />


            if (!displayDataOnly)
            {
                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 40, "Title");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 40, "Name");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 40, "StaticName");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 40, "Description");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 30, "Group");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "Id");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "TypeAsString");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "SchemaXml");

                insertAt.IncrementRows();
            }

            foreach (var field in siteColumns)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), field.Title);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), field.Group);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), field.Id.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), field.TypeAsString.ToString());
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), field.SchemaXml);

                insertAt.IncrementRows();
            }

            XlHlp.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, insertAt, "End");

            return insertAt;
        }
        #endregion

        #endregion

        #region Private Methods

        private bool GetDisplayOrientation()
        {
            return (bool)ceOrientVertical.IsChecked;
        }

        private void LoadControlContents()
        {
            try
            {
                spSiteCollection_Picker.PopulateControlFromFile(Common.cCONFIG_FILE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PopulateSitesPicker(string uri)
        {
            // NB.  This does not recurse.  Just gets direct subsites.

            try
            {
                using (var ctx = new ClientContext(uri))
                {
                    //ctx.Credentials = new NetworkCredential("user", "password", "domain");

                    var web = ctx.Web;

                    ctx.Load(web, info => info.Webs);
                    ctx.ExecuteQuery();

                    //ctx.Load(web.Webs);
                    //ctx.ExecuteQuery();

                    var itemCol = cbeWeb.Items;

                    itemCol.BeginUpdate();
                    itemCol.Clear();

                    foreach (Web w in web.Webs)
                    {
                        // Some stuff came with the web already
                        //ctx.Load(w);
                        ////ctx.Load(w, info => info.Url, info => info.Title);
                        //ctx.ExecuteQuery();

                        XlHlp.DisplayInWatchWindow(string.Format("{0} - {1}", w.Title, w.ServerRelativeUrl));
                        string subSiteUrl = string.Format("{0}{1}", uri, w.ServerRelativeUrl);
                        itemCol.Add(subSiteUrl);
                    }

                    itemCol.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

    }
}
