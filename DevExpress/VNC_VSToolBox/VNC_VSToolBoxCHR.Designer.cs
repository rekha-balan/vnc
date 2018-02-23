namespace VNC_VSToolBox
{
    [System.Runtime.InteropServices.Guid("ed68e9cc-da6f-4c82-b36c-091d57452aff")]
    partial class VNC_VSToolBoxCHR
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DevExpress.DXCore.PlugInCore.DXCoreEvents events;

        public VNC_VSToolBoxCHR()
        {
            // Required for Windows.Forms Class Composition Designer support
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
            this.events = new DevExpress.DXCore.PlugInCore.DXCoreEvents(this.components);
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            ((System.ComponentModel.ISupportInitialize)(this.Images16x16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.events)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.MinimumSize = new System.Drawing.Size(100, 100);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(150, 150);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // VNC_VSToolBoxCHR
            // 
            this.Controls.Add(this.elementHost1);
            this.Name = "VNC_VSToolBoxCHR";
            ((System.ComponentModel.ISupportInitialize)(this.Images16x16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.events)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region ShowWindow
        ///
        /// Displays this tool window.
        ///
        public static EnvDTE.Window ShowWindow()
        {
            return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Show(typeof(VNC_VSToolBoxCHR).GUID);
        }
        #endregion
        #region HideWindow
        ///
        /// Hides this tool window.
        ///
        public static EnvDTE.Window HideWindow()
        {
            return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Hide(typeof(VNC_VSToolBoxCHR).GUID);
        }
        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
    }
}