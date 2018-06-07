using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

using VNC;
using SQLInformation;

using EyeOnLife.User_Interface.Windows;

namespace EyeOnLife.User_Interface.User_Controls
{
    public partial class Add_Instance : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        SQLInformation.ExpandMask.DatabaseExpandSetting databaseExpandSetting = null;
        SQLInformation.ExpandMask.InstanceExpandSetting instanceExpandSetting = null;
        SQLInformation.ExpandMask.JobServerExpandSetting jobServerExpandSetting = null;

        public static readonly DependencyProperty StatusMessage1Property = DependencyProperty.Register("StatusMessage1", typeof(string), typeof(Add_Instance), new UIPropertyMetadata("", new PropertyChangedCallback(OnStatusMessage1Changed), new CoerceValueCallback(OnCoerceStatusMessage1)));

        private static object OnCoerceStatusMessage1(DependencyObject o, object value)
        {
            Add_Instance add_Instance = o as Add_Instance;
            if (add_Instance != null)
                return add_Instance.OnCoerceStatusMessage1((string)value);
            else
                return value;
        }

        private static void OnStatusMessage1Changed(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Add_Instance add_Instance = o as Add_Instance;
            if (add_Instance != null)
                add_Instance.OnStatusMessage1Changed((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual string OnCoerceStatusMessage1(string value)
        {
            // TODO: Keep the proposed value within the desired range.
            return value;
        }

        protected virtual void OnStatusMessage1Changed(string oldValue, string newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        public string StatusMessage1
        {
            // IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
            get
            {
                return (string)GetValue(StatusMessage1Property);
            }
            set
            {
                SetValue(StatusMessage1Property, value);
            }
        }
        
        public static readonly DependencyProperty ControlsAreEnabledProperty = DependencyProperty.Register("ControlsAreEnabled", typeof(bool), typeof(Add_Instance), new UIPropertyMetadata(false, new PropertyChangedCallback(OnControlsAreEnabledChanged), new CoerceValueCallback(OnCoerceControlsAreEnabled)));

        private static object OnCoerceControlsAreEnabled(DependencyObject o, object value)
        {
            Add_Instance add_Instance = o as Add_Instance;

            if (add_Instance != null)
                return add_Instance.OnCoerceControlsAreEnabled((bool)value);
            else
                return value;
        }

        private static void OnControlsAreEnabledChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Add_Instance add_Instance = o as Add_Instance;
            if (add_Instance != null)
                add_Instance.OnControlsAreEnabledChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual bool OnCoerceControlsAreEnabled(bool value)
        {
            // TODO: Keep the proposed value within the desired range.
            return value;
        }

        protected virtual void OnControlsAreEnabledChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        public bool ControlsAreEnabled
        {
            // IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
            get
            {
                return (bool)GetValue(ControlsAreEnabledProperty);
            }
            set
            {
                SetValue(ControlsAreEnabledProperty, value);
            }
        }
        

        #region Constructors
        
        public Add_Instance()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialization

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    SQLInformation.Data.ApplicationDataSet applicationDataSet = ((SQLInformation.Data.ApplicationDataSet)(this.FindResource("applicationDataSet")));
                    applicationDataSet = EyeOnLife.Common.ApplicationDataSet;

                    ((CollectionViewSource)this.Resources["instancesViewSource"]).Source =
                        EyeOnLife.Common.ApplicationDataSet.Instances;

                    // TODO(crhodes): Figureout why can't do this in Xaml.  Seems basic.

                    cbe_NetName.ItemsSource = applicationDataSet.Servers.OrderBy(name => name.NetName);

                    cbe_ADDomain.ItemsSource = applicationDataSet.LKUP_ADDomains;
                    cbe_Environment.ItemsSource = applicationDataSet.LKUP_Environments;
                    cbe_SecurityZone.ItemsSource = applicationDataSet.LKUP_SecurityZones;

                    //textEdit_ID.Text = Guid.NewGuid().ToString();

                    Initialize_ExpandMasks();
                    LogUsage(this.GetType());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Event Handlers

        private void cbe_ProcessNewValue(DependencyObject sender, DevExpress.Xpf.Editors.ProcessNewValueEventArgs e)
        {
            if (AddNewServer())
            {
                cbe_NetName.ItemsSource = Common.ApplicationDataSet.Servers.OrderBy(name => name.NetName); ;
            }
            else
            {
                MessageBox.Show("Could not add new server");
            }
            
        }

        //private void OnCreateSMOLogin(object sender, RoutedEventArgs e)
        //{
        //    string message = "";

        //    SQLInformation.SMO.Actions.Instance.CreateSQLMonitorLogin(te_Name_Instance.Text, out message);
        //}

        private bool InstanceAlreadyExists(string instanceName)
        {
            int matchCount = Common.ApplicationDataSet.Instances.Where(inst => inst.Name_Instance == instanceName).Count();

            return matchCount > 0 ? true : false;
        }

        private bool InstanceIsNotAccessible(string instanceName, string serverName, int instancePortNumber)
        {
            bool result = false; // Instance is accessible

            try
            {
                Helpers.TelnetInterface.TelnetConnection tc = new Helpers.TelnetInterface.TelnetConnection(serverName, instancePortNumber);
                //tb_TelnetResult.Text = string.Format("Successfully connected to {0} on port {1}", server, port);
            }
            catch (Exception ex)
            {
                result = true;
                //tb_TelnetResult.Text = ex.Message;
            }

            return result;
        }

        private void OnCreateNewInstance(object sender, RoutedEventArgs e)
        {
            int instanceExpandMask = 0;
            int databaseExpandMask = 0;
            int jobServerExpandMask = 0;


            Window parentWindow = Window.GetWindow(this);

            string newInstanceFullName = null;

            newInstanceFullName = string.Format("{0}{1}{2}", cbe_NetName.Text, (te_Name_Instance.Text.Length > 0 ? @"\" : ""), te_Name_Instance.Text);
            string serverName = tb_Name_Server.Text;
            int portNumber = 0;

            int.TryParse(te_PortNumber.Text, out portNumber);


            if (InstanceIsNotAccessible(newInstanceFullName, serverName, portNumber))
            {
                MessageBox.Show(string.Format("Instance {0} not accessible.", newInstanceFullName));
                return;
            }

            if (InstanceAlreadyExists(newInstanceFullName))
            {
            	MessageBox.Show(string.Format("Entry for Instance {0} already exists.", newInstanceFullName));
                InitializeComponent();
                return;
            }

            try
            {
                foreach (var item in cbe_IsMonitored.SelectedItems)
                {
                    instanceExpandMask += ((KeyValuePair<string, int>)item).Value;
                }

                foreach (var item in cbe_DefaultDatabaseExpandMask.SelectedItems)
                {
                    databaseExpandMask += ((KeyValuePair<string, int>)item).Value;
                }

                foreach (var item in cbe_DefaultJobServerExpandMask.SelectedItems)
                {
                    jobServerExpandMask += ((KeyValuePair<string, int>)item).Value;
                }

                ExpandMask.InstanceExpandSetting instanceSetting = new ExpandMask.InstanceExpandSetting(instanceExpandMask);

                SQLInformation.Data.ApplicationDataSet.InstancesRow newInstance = Common.ApplicationDataSet.Instances.NewInstancesRow();

                newInstance.ID = Guid.NewGuid();
                newInstance.Name_Instance = newInstanceFullName;
                newInstance.Port = portNumber;

                newInstance.NetName = cbe_NetName.Text;
                newInstance.ADDomain = cbe_ADDomain.Text;
                newInstance.Environment = cbe_Environment.Text;
                newInstance.SecurityZone = cbe_SecurityZone.Text;

                newInstance.IsMonitored = instanceSetting.IsMonitored;

                newInstance.ExpandContent = instanceSetting.ExpandContent;
                newInstance.ExpandJobServer = instanceSetting.ExpandJobServer;
                newInstance.ExpandStorage = instanceSetting.ExpandStorage;
                newInstance.ExpandLinkedServers = instanceSetting.ExpandLinkedServers;
                newInstance.ExpandLogins = instanceSetting.ExpandLogins;
                newInstance.ExpandServerRoles = instanceSetting.ExpandServerRoles;
                newInstance.ExpandTriggers = instanceSetting.ExpandTriggers;

                newInstance.DefaultDatabaseExpandMask = databaseExpandMask;
                newInstance.DefaultJobServerExpandMask = jobServerExpandMask;

                Common.ApplicationDataSet.Instances.AddInstancesRow(newInstance);
                Common.ApplicationDataSet.Instances_Update();
                //Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
                
                string progressMessage = string.Format("Instance: {0}  Added successfully", newInstance.Name_Instance);

                //StatusMessage1 = progressMessage;
                tb_ProgressStatus1.Text = progressMessage;
                //tb_ProgressStatus1.Dispatcher.BeginInvoke((Action)(() => tb_ProgressStatus1.Text = progressMessage), System.Windows.Threading.DispatcherPriority.Normal);

                //tb_ProgressStatus1.Dispatcher.BeginInvoke(new Action(() => tb_ProgressStatus1.Text = progressMessage), System.Windows.Threading.DispatcherPriority.Normal);

                //parentWindow.Dispatcher.BeginInvoke((Action)(() => tb_ProgressStatus1.Text = progressMessage), System.Windows.Threading.DispatcherPriority.Normal);
                string progressMessage2 = "Attempting to create SMO Crawler Login.  This may take a while";


                tb_ProgressStatus2.Dispatcher.BeginInvoke((Action)(() => tb_ProgressStatus2.Text = progressMessage2), System.Windows.Threading.DispatcherPriority.Normal);

                string message = "";

                if (SQLInformation.SMO.Actions.Instance.CreateSQLMonitorLogin(newInstance.Name_Instance, out message))
                {
                    tb_ProgressStatus2.Dispatcher.InvokeAsync((Action)(() => tb_ProgressStatus2.Text = "SMO Crawler Logon created successfully"), System.Windows.Threading.DispatcherPriority.Normal);
                }
                else
                {
                    MessageBox.Show(string.Format(
                        "SMO Crawler Logon not created:\n{0}\nVerify current user ({1}) has sufficient permission to create a new Login in instance.",
                        message, Common.CurrentUser.Identity.Name));
                }

                InitializeControls();
                
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
            }
        }

        private bool CanPingServer(string netName)
        {
            bool result = false;

            var ping = new Ping();

            var pingOptions = new PingOptions();

            pingOptions.DontFragment = true;
            pingOptions.Ttl = 128;

            try
            {
                if (IPStatus.Success == ping.Send(netName).Status)
                {
            	    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                //MessageBox.Show(string.Format("Exception Thrown: {0} {1}", ex.ToString(), ex.InnerException.ToString()), "Ping Results");
            }

            return result;
        }

        private bool IsValidNetName(string netName)
        {
            return CanPingServer(netName);
        }

        private void cbe_NetName_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            //te_Name_Instance.Text = cbe_NetName.Text;
            te_Name_Instance.Text = "";

            if (IsValidNetName(cbe_NetName.Text))
            {
                ControlsAreEnabled = true;
                tb_Name_Server.Text = cbe_NetName.Text;
            }
            else
            {
                ControlsAreEnabled = false;
                tb_Name_Server.Text = "";
            }
        }


        #endregion

        #region Main Function Routines

        private bool AddNewServer()
        {
            bool result = false;

            string controlTag = "User_Controls.Add_Server";

            string typeName = string.Format("EyeOnLife.User_Interface.{0}", controlTag);

            var win = new UserControlHost(typeName, typeName);
            result = (bool)win.ShowDialog();
            var after = (bool)win.DialogResult;


            return result;
        }

        private void Initialize_ExpandMasks()
        {
            instanceExpandSetting = new SQLInformation.ExpandMask.InstanceExpandSetting(0);
            databaseExpandSetting = new SQLInformation.ExpandMask.DatabaseExpandSetting(0);
            jobServerExpandSetting = new ExpandMask.JobServerExpandSetting(0);

            cbe_IsMonitored.ItemsSource = SQLInformation.ExpandMask.InstanceExpandSetting.OptionValues;
            cbe_DefaultDatabaseExpandMask.ItemsSource = SQLInformation.ExpandMask.DatabaseExpandSetting.OptionValues;
            cbe_DefaultJobServerExpandMask.ItemsSource = SQLInformation.ExpandMask.JobServerExpandSetting.OptionValues;
        }


        private void InitializeControls()
        {
            te_PortNumber.Text = "0";

            cbe_NetName.UnselectAllItems();

            te_Name_Instance.Clear();
            tb_NamedInstanceSlash.Visibility = Visibility.Hidden;

            cbe_ADDomain.UnselectAllItems();
            cbe_Environment.UnselectAllItems();
            cbe_SecurityZone.UnselectAllItems();

            cbe_IsMonitored.UnselectAllItems();
            cbe_DefaultDatabaseExpandMask.UnselectAllItems();
            cbe_DefaultJobServerExpandMask.UnselectAllItems();

            txNotes.Clear();

            ControlsAreEnabled = false;
        }

        #endregion

        private void OnEditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e)
        {
            tb_NamedInstanceSlash.Visibility = Visibility.Visible;
        }

    }
}
