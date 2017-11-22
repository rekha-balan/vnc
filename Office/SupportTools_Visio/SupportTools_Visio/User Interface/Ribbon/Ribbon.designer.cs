namespace SupportTools_Visio
{
    partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl1 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl2 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl3 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl4 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl5 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl6 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl7 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl8 = this.Factory.CreateRibbonDropDownItem();
            this.tab1 = this.Factory.CreateRibbonTab();
            this.tabSupportTools = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btnLaunchWPFCommandCockpit = this.Factory.CreateRibbonButton();
            this.btnCommandCockpitWPFWindow = this.Factory.CreateRibbonButton();
            this.rgDocumentActions = this.Factory.CreateRibbonGroup();
            this.btnGetApplicationInfo = this.Factory.CreateRibbonButton();
            this.btnAddTableOfContents = this.Factory.CreateRibbonButton();
            this.btnAddHeaderAndFooter = this.Factory.CreateRibbonButton();
            this.btnAddDefaultLayers = this.Factory.CreateRibbonButton();
            this.btnRemoveLayers = this.Factory.CreateRibbonButton();
            this.btnSortAllPages = this.Factory.CreateRibbonButton();
            this.btnDisplayPageNames = this.Factory.CreateRibbonButton();
            this.btnUpdatePageNameShapes = this.Factory.CreateRibbonButton();
            this.btnAddNavigationLinks = this.Factory.CreateRibbonButton();
            this.btnGetStencilInfo = this.Factory.CreateRibbonButton();
            this.btnGetDocumentInfo = this.Factory.CreateRibbonButton();
            this.btnSyncPageNames = this.Factory.CreateRibbonButton();
            this.rgDisplayLayers = this.Factory.CreateRibbonGroup();
            this.btnPageOn = this.Factory.CreateRibbonButton();
            this.btnPageOff = this.Factory.CreateRibbonButton();
            this.cmbLayers = this.Factory.CreateRibbonComboBox();
            this.btnAllPageOn = this.Factory.CreateRibbonButton();
            this.btnAllPageOff = this.Factory.CreateRibbonButton();
            this.btnLayerManager = this.Factory.CreateRibbonButton();
            this.rgPageActions = this.Factory.CreateRibbonGroup();
            this.btnGetPageInfo = this.Factory.CreateRibbonButton();
            this.btnUpdatePageNameShapesPage = this.Factory.CreateRibbonButton();
            this.btnAddNavLinks = this.Factory.CreateRibbonButton();
            this.btnAddDefaultLayers_Page = this.Factory.CreateRibbonButton();
            this.btnRemoveLayers_Page = this.Factory.CreateRibbonButton();
            this.btnPrintPages = this.Factory.CreateRibbonButton();
            this.btnPrintPage = this.Factory.CreateRibbonButton();
            this.btnSavePages = this.Factory.CreateRibbonButton();
            this.btnSavePage = this.Factory.CreateRibbonButton();
            this.btnDeletePages = this.Factory.CreateRibbonButton();
            this.btnSyncPageNamesPage = this.Factory.CreateRibbonButton();
            this.btnXMLPagesCommands = this.Factory.CreateRibbonButton();
            this.button1 = this.Factory.CreateRibbonButton();
            this.rgShapeActions = this.Factory.CreateRibbonGroup();
            this.btnGetShapeInfo = this.Factory.CreateRibbonButton();
            this.btnAddTextControl = this.Factory.CreateRibbonButton();
            this.btnAddIsPageName = this.Factory.CreateRibbonButton();
            this.btnAddHyperLink = this.Factory.CreateRibbonButton();
            this.btnAddColorSupport = this.Factory.CreateRibbonButton();
            this.btnMakeLinkableMaster = this.Factory.CreateRibbonButton();
            this.btnShapeEditor = this.Factory.CreateRibbonButton();
            this.btnZeroMargins = this.Factory.CreateRibbonButton();
            this.btnAddIDSupport = this.Factory.CreateRibbonButton();
            this.btnAddIDAndTextSupport = this.Factory.CreateRibbonButton();
            this.rgSMARTS = this.Factory.CreateRibbonGroup();
            this.btnRetrive = this.Factory.CreateRibbonButton();
            this.btnWebPage = this.Factory.CreateRibbonButton();
            this.btnValidate = this.Factory.CreateRibbonButton();
            this.btnReleatedProcess = this.Factory.CreateRibbonButton();
            this.btnRelatedSystem = this.Factory.CreateRibbonButton();
            this.btnRelatedIntfrastructure = this.Factory.CreateRibbonButton();
            this.btnNavigateUp = this.Factory.CreateRibbonButton();
            this.btnNavigateDown = this.Factory.CreateRibbonButton();
            this.btnHilight = this.Factory.CreateRibbonButton();
            this.grpCommands = this.Factory.CreateRibbonGroup();
            this.btnVisioCommands = this.Factory.CreateRibbonButton();
            this.grpDebug = this.Factory.CreateRibbonGroup();
            this.btnDebugWindow = this.Factory.CreateRibbonButton();
            this.btnWatchWindow = this.Factory.CreateRibbonButton();
            this.chkEnableAppEvents = this.Factory.CreateRibbonCheckBox();
            this.chkDisplayEvents = this.Factory.CreateRibbonCheckBox();
            this.chkDisplayChattyEvents = this.Factory.CreateRibbonCheckBox();
            this.grpHelp = this.Factory.CreateRibbonGroup();
            this.btnAddInInfo = this.Factory.CreateRibbonButton();
            this.btnDeveloperMode = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.tabSupportTools.SuspendLayout();
            this.group1.SuspendLayout();
            this.rgDocumentActions.SuspendLayout();
            this.rgDisplayLayers.SuspendLayout();
            this.rgPageActions.SuspendLayout();
            this.rgShapeActions.SuspendLayout();
            this.rgSMARTS.SuspendLayout();
            this.grpCommands.SuspendLayout();
            this.grpDebug.SuspendLayout();
            this.grpHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // tabSupportTools
            // 
            this.tabSupportTools.Groups.Add(this.group1);
            this.tabSupportTools.Groups.Add(this.rgDocumentActions);
            this.tabSupportTools.Groups.Add(this.rgDisplayLayers);
            this.tabSupportTools.Groups.Add(this.rgPageActions);
            this.tabSupportTools.Groups.Add(this.rgShapeActions);
            this.tabSupportTools.Groups.Add(this.rgSMARTS);
            this.tabSupportTools.Groups.Add(this.grpCommands);
            this.tabSupportTools.Groups.Add(this.grpDebug);
            this.tabSupportTools.Groups.Add(this.grpHelp);
            this.tabSupportTools.Label = "Support Tools";
            this.tabSupportTools.Name = "tabSupportTools";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnLaunchWPFCommandCockpit);
            this.group1.Items.Add(this.btnCommandCockpitWPFWindow);
            this.group1.Label = "WPF UI";
            this.group1.Name = "group1";
            // 
            // btnLaunchWPFCommandCockpit
            // 
            this.btnLaunchWPFCommandCockpit.Label = "Command Cockpit";
            this.btnLaunchWPFCommandCockpit.Name = "btnLaunchWPFCommandCockpit";
            this.btnLaunchWPFCommandCockpit.ScreenTip = "Launch WPF Command Cockpit";
            this.btnLaunchWPFCommandCockpit.SuperTip = "Launch WPF Command Cockpit.   Use SupportTools_Config to add behavior";
            this.btnLaunchWPFCommandCockpit.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLaunchWPFCommandCockpit_Click);
            // 
            // btnCommandCockpitWPFWindow
            // 
            this.btnCommandCockpitWPFWindow.Label = "Command Cockpit WPF Window";
            this.btnCommandCockpitWPFWindow.Name = "btnCommandCockpitWPFWindow";
            this.btnCommandCockpitWPFWindow.ScreenTip = "Launch WPF Command Cockpit";
            this.btnCommandCockpitWPFWindow.SuperTip = "Launch WPF Command Cockpit.   Use SupportTools_Config to add behavior";
            this.btnCommandCockpitWPFWindow.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnCommandCockpitWPFWindow_Click);
            // 
            // rgDocumentActions
            // 
            this.rgDocumentActions.DialogLauncher = ribbonDialogLauncherImpl1;
            this.rgDocumentActions.Items.Add(this.btnGetApplicationInfo);
            this.rgDocumentActions.Items.Add(this.btnAddTableOfContents);
            this.rgDocumentActions.Items.Add(this.btnAddHeaderAndFooter);
            this.rgDocumentActions.Items.Add(this.btnAddDefaultLayers);
            this.rgDocumentActions.Items.Add(this.btnRemoveLayers);
            this.rgDocumentActions.Items.Add(this.btnSortAllPages);
            this.rgDocumentActions.Items.Add(this.btnDisplayPageNames);
            this.rgDocumentActions.Items.Add(this.btnUpdatePageNameShapes);
            this.rgDocumentActions.Items.Add(this.btnAddNavigationLinks);
            this.rgDocumentActions.Items.Add(this.btnGetStencilInfo);
            this.rgDocumentActions.Items.Add(this.btnGetDocumentInfo);
            this.rgDocumentActions.Items.Add(this.btnSyncPageNames);
            this.rgDocumentActions.Label = "Document Actions";
            this.rgDocumentActions.Name = "rgDocumentActions";
            // 
            // btnGetApplicationInfo
            // 
            this.btnGetApplicationInfo.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnGetApplicationInfo.Image = global::SupportTools_Visio.Properties.Resources.Application_Info_64x64;
            this.btnGetApplicationInfo.Label = "Appliction Info";
            this.btnGetApplicationInfo.Name = "btnGetApplicationInfo";
            this.btnGetApplicationInfo.ScreenTip = "Get Application Info";
            this.btnGetApplicationInfo.ShowImage = true;
            this.btnGetApplicationInfo.SuperTip = "Get Informtation from Application Object";
            // 
            // btnAddTableOfContents
            // 
            this.btnAddTableOfContents.Label = "Add Table of Contents";
            this.btnAddTableOfContents.Name = "btnAddTableOfContents";
            this.btnAddTableOfContents.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddTableOfContents_Click);
            // 
            // btnAddHeaderAndFooter
            // 
            this.btnAddHeaderAndFooter.Label = "Add Header and Footer";
            this.btnAddHeaderAndFooter.Name = "btnAddHeaderAndFooter";
            this.btnAddHeaderAndFooter.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddHeaderAndFooter_Click);
            // 
            // btnAddDefaultLayers
            // 
            this.btnAddDefaultLayers.Label = "Add DefaultLayers";
            this.btnAddDefaultLayers.Name = "btnAddDefaultLayers";
            this.btnAddDefaultLayers.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddDefaultLayers_Click);
            // 
            // btnRemoveLayers
            // 
            this.btnRemoveLayers.Label = "Remove Layers";
            this.btnRemoveLayers.Name = "btnRemoveLayers";
            this.btnRemoveLayers.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRemoveLayers_Click);
            // 
            // btnSortAllPages
            // 
            this.btnSortAllPages.Label = "Sort All Pages";
            this.btnSortAllPages.Name = "btnSortAllPages";
            this.btnSortAllPages.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSortAllPages_Click);
            // 
            // btnDisplayPageNames
            // 
            this.btnDisplayPageNames.Label = "Display Page Names";
            this.btnDisplayPageNames.Name = "btnDisplayPageNames";
            this.btnDisplayPageNames.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnDisplayPageNames_Click);
            // 
            // btnUpdatePageNameShapes
            // 
            this.btnUpdatePageNameShapes.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnUpdatePageNameShapes.Image = global::SupportTools_Visio.Properties.Resources.Update_Name_Shapes64x64;
            this.btnUpdatePageNameShapes.Label = "Update Shapes";
            this.btnUpdatePageNameShapes.Name = "btnUpdatePageNameShapes";
            this.btnUpdatePageNameShapes.ScreenTip = "Update PageName Shapes";
            this.btnUpdatePageNameShapes.ShowImage = true;
            this.btnUpdatePageNameShapes.SuperTip = "Update Page Name Shapes from Page Name text";
            this.btnUpdatePageNameShapes.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnUpdatePageNameShapes_Click);
            // 
            // btnAddNavigationLinks
            // 
            this.btnAddNavigationLinks.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAddNavigationLinks.Image = global::SupportTools_Visio.Properties.Resources.Navigation_Links_64x64;
            this.btnAddNavigationLinks.Label = "Nav Links";
            this.btnAddNavigationLinks.Name = "btnAddNavigationLinks";
            this.btnAddNavigationLinks.ScreenTip = "Add Navigation Links";
            this.btnAddNavigationLinks.ShowImage = true;
            this.btnAddNavigationLinks.SuperTip = "Add Navigation Links from Navigation Links Background Page";
            this.btnAddNavigationLinks.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddNavigationLinks_Click);
            // 
            // btnGetStencilInfo
            // 
            this.btnGetStencilInfo.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnGetStencilInfo.Image = global::SupportTools_Visio.Properties.Resources.Stencil_Info_64x64;
            this.btnGetStencilInfo.Label = "Stencil Info";
            this.btnGetStencilInfo.Name = "btnGetStencilInfo";
            this.btnGetStencilInfo.ScreenTip = "Get Stencil Info";
            this.btnGetStencilInfo.ShowImage = true;
            this.btnGetStencilInfo.SuperTip = "Get Information from Stencil Object";
            this.btnGetStencilInfo.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnGetStencilInfo_Click);
            // 
            // btnGetDocumentInfo
            // 
            this.btnGetDocumentInfo.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnGetDocumentInfo.Image = global::SupportTools_Visio.Properties.Resources.Document_Info_64x64;
            this.btnGetDocumentInfo.Label = "Document Info";
            this.btnGetDocumentInfo.Name = "btnGetDocumentInfo";
            this.btnGetDocumentInfo.ScreenTip = "Get Document Info";
            this.btnGetDocumentInfo.ShowImage = true;
            this.btnGetDocumentInfo.SuperTip = "Get Information from Document Object";
            this.btnGetDocumentInfo.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnGetDocumentInfo_Click);
            // 
            // btnSyncPageNames
            // 
            this.btnSyncPageNames.Label = "Sync Name(U)";
            this.btnSyncPageNames.Name = "btnSyncPageNames";
            this.btnSyncPageNames.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSyncPageNames_Click);
            // 
            // rgDisplayLayers
            // 
            this.rgDisplayLayers.Items.Add(this.btnPageOn);
            this.rgDisplayLayers.Items.Add(this.btnPageOff);
            this.rgDisplayLayers.Items.Add(this.cmbLayers);
            this.rgDisplayLayers.Items.Add(this.btnAllPageOn);
            this.rgDisplayLayers.Items.Add(this.btnAllPageOff);
            this.rgDisplayLayers.Items.Add(this.btnLayerManager);
            this.rgDisplayLayers.Label = "Display Layers";
            this.rgDisplayLayers.Name = "rgDisplayLayers";
            // 
            // btnPageOn
            // 
            this.btnPageOn.Label = "Page On";
            this.btnPageOn.Name = "btnPageOn";
            this.btnPageOn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnPageOn_Click);
            // 
            // btnPageOff
            // 
            this.btnPageOff.Label = "Page Off";
            this.btnPageOff.Name = "btnPageOff";
            this.btnPageOff.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnPageOff_Click);
            // 
            // cmbLayers
            // 
            ribbonDropDownItemImpl1.Label = "Navigation";
            ribbonDropDownItemImpl2.Label = "Header";
            ribbonDropDownItemImpl3.Label = "Security";
            ribbonDropDownItemImpl4.Label = "Application";
            ribbonDropDownItemImpl5.Label = "Level0";
            ribbonDropDownItemImpl6.Label = "Level1";
            ribbonDropDownItemImpl7.Label = "Level2";
            ribbonDropDownItemImpl8.Label = "Notes";
            this.cmbLayers.Items.Add(ribbonDropDownItemImpl1);
            this.cmbLayers.Items.Add(ribbonDropDownItemImpl2);
            this.cmbLayers.Items.Add(ribbonDropDownItemImpl3);
            this.cmbLayers.Items.Add(ribbonDropDownItemImpl4);
            this.cmbLayers.Items.Add(ribbonDropDownItemImpl5);
            this.cmbLayers.Items.Add(ribbonDropDownItemImpl6);
            this.cmbLayers.Items.Add(ribbonDropDownItemImpl7);
            this.cmbLayers.Items.Add(ribbonDropDownItemImpl8);
            this.cmbLayers.Label = "Layer";
            this.cmbLayers.Name = "cmbLayers";
            this.cmbLayers.Text = null;
            // 
            // btnAllPageOn
            // 
            this.btnAllPageOn.Label = "All Pages On";
            this.btnAllPageOn.Name = "btnAllPageOn";
            this.btnAllPageOn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAllPageOn_Click);
            // 
            // btnAllPageOff
            // 
            this.btnAllPageOff.Label = "All Pages Off";
            this.btnAllPageOff.Name = "btnAllPageOff";
            this.btnAllPageOff.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAllPageOff_Click);
            // 
            // btnLayerManager
            // 
            this.btnLayerManager.Label = "Layer Manager";
            this.btnLayerManager.Name = "btnLayerManager";
            this.btnLayerManager.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLoadLayers_Click);
            // 
            // rgPageActions
            // 
            this.rgPageActions.Items.Add(this.btnGetPageInfo);
            this.rgPageActions.Items.Add(this.btnUpdatePageNameShapesPage);
            this.rgPageActions.Items.Add(this.btnAddNavLinks);
            this.rgPageActions.Items.Add(this.btnAddDefaultLayers_Page);
            this.rgPageActions.Items.Add(this.btnRemoveLayers_Page);
            this.rgPageActions.Items.Add(this.btnPrintPages);
            this.rgPageActions.Items.Add(this.btnPrintPage);
            this.rgPageActions.Items.Add(this.btnSavePages);
            this.rgPageActions.Items.Add(this.btnSavePage);
            this.rgPageActions.Items.Add(this.btnDeletePages);
            this.rgPageActions.Items.Add(this.btnSyncPageNamesPage);
            this.rgPageActions.Items.Add(this.btnXMLPagesCommands);
            this.rgPageActions.Items.Add(this.button1);
            this.rgPageActions.Label = "Page Actions";
            this.rgPageActions.Name = "rgPageActions";
            // 
            // btnGetPageInfo
            // 
            this.btnGetPageInfo.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnGetPageInfo.Image = global::SupportTools_Visio.Properties.Resources.Page_Info_64x64;
            this.btnGetPageInfo.Label = "Page Info";
            this.btnGetPageInfo.Name = "btnGetPageInfo";
            this.btnGetPageInfo.ScreenTip = "Get Page Info";
            this.btnGetPageInfo.ShowImage = true;
            this.btnGetPageInfo.SuperTip = "Get Information from Page Object";
            this.btnGetPageInfo.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnGetPageInfo_Click);
            // 
            // btnUpdatePageNameShapesPage
            // 
            this.btnUpdatePageNameShapesPage.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnUpdatePageNameShapesPage.Image = global::SupportTools_Visio.Properties.Resources.Update_Name_Shapes64x64;
            this.btnUpdatePageNameShapesPage.Label = "Update Shapes";
            this.btnUpdatePageNameShapesPage.Name = "btnUpdatePageNameShapesPage";
            this.btnUpdatePageNameShapesPage.ScreenTip = "Update PageName Shapes";
            this.btnUpdatePageNameShapesPage.ShowImage = true;
            this.btnUpdatePageNameShapesPage.SuperTip = "Update Page Name Shapes from Page Name text";
            this.btnUpdatePageNameShapesPage.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnUpdatePageNameShapesPage_Click);
            // 
            // btnAddNavLinks
            // 
            this.btnAddNavLinks.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAddNavLinks.Image = global::SupportTools_Visio.Properties.Resources.Navigation_Links_32x32;
            this.btnAddNavLinks.Label = "Nav Links";
            this.btnAddNavLinks.Name = "btnAddNavLinks";
            this.btnAddNavLinks.ScreenTip = "Add Navigation Links";
            this.btnAddNavLinks.ShowImage = true;
            this.btnAddNavLinks.SuperTip = "Add Navigation Links from Navigation Links Background Page";
            this.btnAddNavLinks.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddNavLinks_Click);
            // 
            // btnAddDefaultLayers_Page
            // 
            this.btnAddDefaultLayers_Page.Label = "Add DefaultLayers";
            this.btnAddDefaultLayers_Page.Name = "btnAddDefaultLayers_Page";
            this.btnAddDefaultLayers_Page.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddDefaultLayers_Page_Click);
            // 
            // btnRemoveLayers_Page
            // 
            this.btnRemoveLayers_Page.Label = "Remove Layers";
            this.btnRemoveLayers_Page.Name = "btnRemoveLayers_Page";
            this.btnRemoveLayers_Page.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRemoveLayers_Page_Click);
            // 
            // btnPrintPages
            // 
            this.btnPrintPages.Label = "Print Pages";
            this.btnPrintPages.Name = "btnPrintPages";
            this.btnPrintPages.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnPrintPages_Click);
            // 
            // btnPrintPage
            // 
            this.btnPrintPage.Label = "Print Page";
            this.btnPrintPage.Name = "btnPrintPage";
            this.btnPrintPage.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnPrintPage_Click);
            // 
            // btnSavePages
            // 
            this.btnSavePages.Label = "Save Pages";
            this.btnSavePages.Name = "btnSavePages";
            this.btnSavePages.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSavePages_Click);
            // 
            // btnSavePage
            // 
            this.btnSavePage.Label = "Save Page";
            this.btnSavePage.Name = "btnSavePage";
            this.btnSavePage.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSavePage_Click);
            // 
            // btnDeletePages
            // 
            this.btnDeletePages.Label = "Delete Pages";
            this.btnDeletePages.Name = "btnDeletePages";
            this.btnDeletePages.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnDeletePages_Click);
            // 
            // btnSyncPageNamesPage
            // 
            this.btnSyncPageNamesPage.Label = "Sync Name(U)";
            this.btnSyncPageNamesPage.Name = "btnSyncPageNamesPage";
            this.btnSyncPageNamesPage.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSyncPageNamesPage_Click);
            // 
            // btnXMLPagesCommands
            // 
            this.btnXMLPagesCommands.Label = "XML Pages Commands";
            this.btnXMLPagesCommands.Name = "btnXMLPagesCommands";
            // 
            // button1
            // 
            this.button1.Label = "";
            this.button1.Name = "button1";
            // 
            // rgShapeActions
            // 
            this.rgShapeActions.Items.Add(this.btnGetShapeInfo);
            this.rgShapeActions.Items.Add(this.btnAddTextControl);
            this.rgShapeActions.Items.Add(this.btnAddIsPageName);
            this.rgShapeActions.Items.Add(this.btnAddHyperLink);
            this.rgShapeActions.Items.Add(this.btnAddColorSupport);
            this.rgShapeActions.Items.Add(this.btnMakeLinkableMaster);
            this.rgShapeActions.Items.Add(this.btnShapeEditor);
            this.rgShapeActions.Items.Add(this.btnZeroMargins);
            this.rgShapeActions.Items.Add(this.btnAddIDSupport);
            this.rgShapeActions.Items.Add(this.btnAddIDAndTextSupport);
            this.rgShapeActions.Label = "Shape Actions";
            this.rgShapeActions.Name = "rgShapeActions";
            // 
            // btnGetShapeInfo
            // 
            this.btnGetShapeInfo.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnGetShapeInfo.Image = global::SupportTools_Visio.Properties.Resources.Shape_Info_64x64;
            this.btnGetShapeInfo.Label = "Shape Info";
            this.btnGetShapeInfo.Name = "btnGetShapeInfo";
            this.btnGetShapeInfo.ScreenTip = "Get Shape Info";
            this.btnGetShapeInfo.ShowImage = true;
            this.btnGetShapeInfo.SuperTip = "Get Information from Shape Object";
            this.btnGetShapeInfo.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnGetShapeInfo_Click);
            // 
            // btnAddTextControl
            // 
            this.btnAddTextControl.Label = "+ Text Control";
            this.btnAddTextControl.Name = "btnAddTextControl";
            this.btnAddTextControl.ScreenTip = "Add Text Transform Control to Shape";
            this.btnAddTextControl.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddTextControl_Click);
            // 
            // btnAddIsPageName
            // 
            this.btnAddIsPageName.Label = "+ IsPageName";
            this.btnAddIsPageName.Name = "btnAddIsPageName";
            this.btnAddIsPageName.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddIsPageName_Click);
            // 
            // btnAddHyperLink
            // 
            this.btnAddHyperLink.Label = "+ HyperLink";
            this.btnAddHyperLink.Name = "btnAddHyperLink";
            this.btnAddHyperLink.ScreenTip = "Add HyperLink to Page with same name";
            this.btnAddHyperLink.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddHyperLink_Click);
            // 
            // btnAddColorSupport
            // 
            this.btnAddColorSupport.Label = "+ Color Support";
            this.btnAddColorSupport.Name = "btnAddColorSupport";
            this.btnAddColorSupport.ScreenTip = "Add Color Support to Shape";
            this.btnAddColorSupport.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddColorSupport_Click);
            // 
            // btnMakeLinkableMaster
            // 
            this.btnMakeLinkableMaster.Label = "Linkable Master";
            this.btnMakeLinkableMaster.Name = "btnMakeLinkableMaster";
            this.btnMakeLinkableMaster.ScreenTip = "Make Linkable Master";
            this.btnMakeLinkableMaster.SuperTip = "Make Linkable Master by adding Action Sections";
            this.btnMakeLinkableMaster.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnMakeLinkableMaster_Click);
            // 
            // btnShapeEditor
            // 
            this.btnShapeEditor.Label = "Shape Editor";
            this.btnShapeEditor.Name = "btnShapeEditor";
            this.btnShapeEditor.ScreenTip = "Launch Shape Editor";
            this.btnShapeEditor.SuperTip = "Launch Shape Editor.  Use SupportTools_Config to add behavior";
            this.btnShapeEditor.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnShapeEditor_Click);
            // 
            // btnZeroMargins
            // 
            this.btnZeroMargins.Label = "Zero Margins";
            this.btnZeroMargins.Name = "btnZeroMargins";
            this.btnZeroMargins.ScreenTip = "Zero Text Block Margins for selected Shapes";
            this.btnZeroMargins.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnZeroMargins_Click);
            // 
            // btnAddIDSupport
            // 
            this.btnAddIDSupport.Label = "+ ID Support";
            this.btnAddIDSupport.Name = "btnAddIDSupport";
            this.btnAddIDSupport.ScreenTip = "Add ID Support to Shape";
            this.btnAddIDSupport.SuperTip = "Add ID Support to Shape by adding Shape Data";
            this.btnAddIDSupport.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddIDSupport_Click);
            // 
            // btnAddIDAndTextSupport
            // 
            this.btnAddIDAndTextSupport.Label = "+ ID/Text Support";
            this.btnAddIDAndTextSupport.Name = "btnAddIDAndTextSupport";
            this.btnAddIDAndTextSupport.ScreenTip = "Add ID and Text Box suppor to shape";
            this.btnAddIDAndTextSupport.SuperTip = "Add ID and Text Box suppor to shape by adding Shape Data";
            this.btnAddIDAndTextSupport.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddIDAndTextSupport_Click);
            // 
            // rgSMARTS
            // 
            this.rgSMARTS.Items.Add(this.btnRetrive);
            this.rgSMARTS.Items.Add(this.btnWebPage);
            this.rgSMARTS.Items.Add(this.btnValidate);
            this.rgSMARTS.Items.Add(this.btnReleatedProcess);
            this.rgSMARTS.Items.Add(this.btnRelatedSystem);
            this.rgSMARTS.Items.Add(this.btnRelatedIntfrastructure);
            this.rgSMARTS.Items.Add(this.btnNavigateUp);
            this.rgSMARTS.Items.Add(this.btnNavigateDown);
            this.rgSMARTS.Items.Add(this.btnHilight);
            this.rgSMARTS.Label = "SMARTS";
            this.rgSMARTS.Name = "rgSMARTS";
            // 
            // btnRetrive
            // 
            this.btnRetrive.Label = "Retrive";
            this.btnRetrive.Name = "btnRetrive";
            this.btnRetrive.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRetrive_Click);
            // 
            // btnWebPage
            // 
            this.btnWebPage.Label = "WebPage";
            this.btnWebPage.Name = "btnWebPage";
            this.btnWebPage.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnWebPage_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.Label = "Validate";
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnValidate_Click);
            // 
            // btnReleatedProcess
            // 
            this.btnReleatedProcess.Label = "Related Process";
            this.btnReleatedProcess.Name = "btnReleatedProcess";
            this.btnReleatedProcess.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRelatedProcess_Click);
            // 
            // btnRelatedSystem
            // 
            this.btnRelatedSystem.Label = "Related System";
            this.btnRelatedSystem.Name = "btnRelatedSystem";
            this.btnRelatedSystem.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRelatedSystem_Click);
            // 
            // btnRelatedIntfrastructure
            // 
            this.btnRelatedIntfrastructure.Label = "Related Infrastructure";
            this.btnRelatedIntfrastructure.Name = "btnRelatedIntfrastructure";
            this.btnRelatedIntfrastructure.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRelatedIntfrastructure_Click);
            // 
            // btnNavigateUp
            // 
            this.btnNavigateUp.Label = "Navigate Up";
            this.btnNavigateUp.Name = "btnNavigateUp";
            this.btnNavigateUp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnNavigateUp_Click);
            // 
            // btnNavigateDown
            // 
            this.btnNavigateDown.Label = "Navigate Down";
            this.btnNavigateDown.Name = "btnNavigateDown";
            this.btnNavigateDown.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnNavigateDown_Click);
            // 
            // btnHilight
            // 
            this.btnHilight.Label = "Hilight";
            this.btnHilight.Name = "btnHilight";
            this.btnHilight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnHilight_Click);
            // 
            // grpCommands
            // 
            this.grpCommands.Items.Add(this.btnVisioCommands);
            this.grpCommands.Label = "XML Commands";
            this.grpCommands.Name = "grpCommands";
            // 
            // btnVisioCommands
            // 
            this.btnVisioCommands.Label = "Visio Command Editor";
            this.btnVisioCommands.Name = "btnVisioCommands";
            this.btnVisioCommands.ScreenTip = "Launch Shape Editor Modeless.";
            this.btnVisioCommands.SuperTip = "Launch Shape Editor.  Use SupportTools_Config to add behavior";
            this.btnVisioCommands.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnShapeEditor2_Click);
            // 
            // grpDebug
            // 
            this.grpDebug.Items.Add(this.btnDebugWindow);
            this.grpDebug.Items.Add(this.btnWatchWindow);
            this.grpDebug.Items.Add(this.chkEnableAppEvents);
            this.grpDebug.Items.Add(this.chkDisplayEvents);
            this.grpDebug.Items.Add(this.chkDisplayChattyEvents);
            this.grpDebug.Label = "Debug";
            this.grpDebug.Name = "grpDebug";
            this.grpDebug.Visible = false;
            // 
            // btnDebugWindow
            // 
            this.btnDebugWindow.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnDebugWindow.Image = global::SupportTools_Visio.Properties.Resources.Auto_Debug_System_icon;
            this.btnDebugWindow.Label = "Debug Window";
            this.btnDebugWindow.Name = "btnDebugWindow";
            this.btnDebugWindow.ShowImage = true;
            this.btnDebugWindow.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnDebugWindow_Click);
            // 
            // btnWatchWindow
            // 
            this.btnWatchWindow.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnWatchWindow.Image = global::SupportTools_Visio.Properties.Resources.WatchWindow;
            this.btnWatchWindow.Label = "Watch Window";
            this.btnWatchWindow.Name = "btnWatchWindow";
            this.btnWatchWindow.ShowImage = true;
            this.btnWatchWindow.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnWatchWindow_Click);
            // 
            // chkEnableAppEvents
            // 
            this.chkEnableAppEvents.Label = "Enable App Events";
            this.chkEnableAppEvents.Name = "chkEnableAppEvents";
            this.chkEnableAppEvents.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.chkEnableAppEvents_Click);
            // 
            // chkDisplayEvents
            // 
            this.chkDisplayEvents.Label = "Display Events";
            this.chkDisplayEvents.Name = "chkDisplayEvents";
            this.chkDisplayEvents.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.chkDisplayEvents_Click);
            // 
            // chkDisplayChattyEvents
            // 
            this.chkDisplayChattyEvents.Label = "Display Chatty Events";
            this.chkDisplayChattyEvents.Name = "chkDisplayChattyEvents";
            this.chkDisplayChattyEvents.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.chkDisplayChattyEvents_Click);
            // 
            // grpHelp
            // 
            this.grpHelp.Items.Add(this.btnAddInInfo);
            this.grpHelp.Items.Add(this.btnDeveloperMode);
            this.grpHelp.Label = "Help";
            this.grpHelp.Name = "grpHelp";
            // 
            // btnAddInInfo
            // 
            this.btnAddInInfo.Label = "AddIn Info";
            this.btnAddInInfo.Name = "btnAddInInfo";
            this.btnAddInInfo.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddInInfo_Click);
            // 
            // btnDeveloperMode
            // 
            this.btnDeveloperMode.Label = "Developer Mode";
            this.btnDeveloperMode.Name = "btnDeveloperMode";
            this.btnDeveloperMode.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnDeveloperMode_Click);
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.Visio.Drawing";
            this.Tabs.Add(this.tab1);
            this.Tabs.Add(this.tabSupportTools);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.tabSupportTools.ResumeLayout(false);
            this.tabSupportTools.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.rgDocumentActions.ResumeLayout(false);
            this.rgDocumentActions.PerformLayout();
            this.rgDisplayLayers.ResumeLayout(false);
            this.rgDisplayLayers.PerformLayout();
            this.rgPageActions.ResumeLayout(false);
            this.rgPageActions.PerformLayout();
            this.rgShapeActions.ResumeLayout(false);
            this.rgShapeActions.PerformLayout();
            this.rgSMARTS.ResumeLayout(false);
            this.rgSMARTS.PerformLayout();
            this.grpCommands.ResumeLayout(false);
            this.grpCommands.PerformLayout();
            this.grpDebug.ResumeLayout(false);
            this.grpDebug.PerformLayout();
            this.grpHelp.ResumeLayout(false);
            this.grpHelp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        private Microsoft.Office.Tools.Ribbon.RibbonTab tabSupportTools;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpCommands;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpDebug;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnDebugWindow;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnWatchWindow;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkEnableAppEvents;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkDisplayEvents;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpHelp;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddInInfo;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnDeveloperMode;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkDisplayChattyEvents;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup rgPageActions;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddTableOfContents;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup rgSMARTS;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRetrive;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnWebPage;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnValidate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnReleatedProcess;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRelatedSystem;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRelatedIntfrastructure;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNavigateUp;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNavigateDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnHilight;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnGetDocumentInfo;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnGetStencilInfo;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup rgDocumentActions;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnGetShapeInfo;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnGetPageInfo;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddHeaderAndFooter;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddDefaultLayers;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRemoveLayers;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddIsPageName;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnUpdatePageNameShapes;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddNavigationLinks;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnGetApplicationInfo;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup rgShapeActions;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddNavLinks;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddHyperLink;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddDefaultLayers_Page;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPrintPages;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPrintPage;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnDeletePages;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSavePage;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSavePages;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup rgDisplayLayers;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cmbLayers;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPageOn;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPageOff;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAllPageOn;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAllPageOff;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLayerManager;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddTextControl;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnZeroMargins;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnMakeLinkableMaster;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnShapeEditor;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddColorSupport;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddIDSupport;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddIDAndTextSupport;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnVisioCommands;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSortAllPages;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnDisplayPageNames;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSyncPageNames;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSyncPageNamesPage;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRemoveLayers_Page;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnUpdatePageNameShapesPage;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnXMLPagesCommands;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLaunchWPFCommandCockpit;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnCommandCockpitWPFWindow;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get
            {
                return this.GetRibbon<Ribbon>();
            }
        }
    }
}
