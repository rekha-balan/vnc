using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

using EyeOnLife.User_Interface.Windows;
using VNC;
using SQLInformation;
using DevExpress.Xpf.Core;

namespace EyeOnLife.User_Interface.User_Controls
{

    public partial class wucDX_LoadFromXMLFile : wucDX_Base
    {

        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        #region Initialization

        public wucDX_LoadFromXMLFile()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            InitializeComponent();
            ThemeManager.ApplicationThemeName = SQLInformation.Data.Config.DefaultUITheme;
#if TRACE
            VNC.AppLog.Trace5("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif

        }

        #endregion

        private void OnSelectFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog1.FileName = "";

            if (System.Windows.Forms.DialogResult.OK == openFileDialog1.ShowDialog())
            {
                try
                {
                    string fileName = openFileDialog1.FileName;
                    var xmlData = XDocument.Load(fileName);

                    // TODO:  Add some more UI and stuff to handle environments.

                    var serverInstances = xmlData.Descendants("Instance");
                    var serverInstances2 = xmlData.Elements("Instance");

                    foreach (var instance in serverInstances)
                    {
                        string server = instance.Attribute("Server").Value;
                        string ipv4Address = instance.Attribute("IPV4Address").Value;
                        string instanceName = instance.Attribute("Name").Value;
                        string port = instance.Attribute("Port").Value;
                        string adDomain = instance.Attribute("ADDomain").Value;
                        string environment = instance.Attribute("Environment").Value;
                        string securityZone = instance.Attribute("SecurityZone").Value;

                        string validName = "";

                        // Prefer HostName over IPs and InstanceNames over Port#s

                        if (instanceName.Length > 0 || port.Length > 0)  // Have Instance Info           
                        {
                            validName = string.Format(@"{0}{1}",
                                server.Length > 0 ? server : ipv4Address,
                                instanceName.Length > 0 ? @"\" + instanceName : "," + port
                                );
                        }
                        else
                        {
                            validName = server.Length > 0 ? server : ipv4Address;
                        }

                        // TODO(crhodes): Make smarter to only add new instances.
                        var existingInstance = from inst in Common.ApplicationDataSet.Instances
                                               where inst.Name_Instance == validName
                                               select inst.Name_Instance;

                        int matchCount = existingInstance.Count();

                        if (matchCount == 0)
                        {
                            SQLInformation.Data.ApplicationDataSet.InstancesRow newInstance = Common.ApplicationDataSet.GetNewInstancesRow(validName);
                            newInstance.NetName = server;
                            newInstance.ADDomain = adDomain;
                            newInstance.Environment = environment;
                            newInstance.SecurityZone = securityZone;

                            newInstance.DefaultDatabaseExpandMask = 2047;
                            newInstance.DefaultJobServerExpandMask = 8191;

                            Common.ApplicationDataSet.Instances.AddInstancesRow(newInstance);
                        }

                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }

            }
        }


        #region Properties


        #endregion

        #region Event Handlers


        #endregion

        #region Main Function Routines


        #endregion

    }
}
