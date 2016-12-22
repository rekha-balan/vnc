namespace DX_AddLogging
{
    partial class PlugIn1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public PlugIn1()
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.actAddLogging = new DevExpress.CodeRush.Core.Action(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Images16x16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actAddLogging)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // actAddLogging
            // 
            this.actAddLogging.ActionName = "VNCAddLogging";
            this.actAddLogging.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.actAddLogging.ImageBackColor = System.Drawing.Color.Empty;
            this.actAddLogging.ToolbarItem.ButtonIsPressed = false;
            this.actAddLogging.ToolbarItem.Caption = null;
            this.actAddLogging.ToolbarItem.Enabled = true;
            this.actAddLogging.ToolbarItem.Image = null;
            this.actAddLogging.ToolbarItem.ToolbarName = null;
            this.actAddLogging.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actAddLogging_Execute);
            ((System.ComponentModel.ISupportInitialize)(this.Images16x16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actAddLogging)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.Action actAddLogging;
    }
}