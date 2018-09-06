using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;
using DevExpress.XtraRichEdit;
using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit.API.Native;

using Microsoft.AspNet.SignalR.Client;


namespace VNCCodeCommandConsole.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wndDX_ExploreInstances.xaml
    /// </summary>
    public partial class wucLiveLogViewer : wucDXBase
    {
        #region Enums, Fields, Properties

        public String UserName { get; set; }

        public IHubProxy HubProxy { get; set; }

        //private string ServerURI = "http://localhost:8095/signalr";

        public HubConnection Connection { get; set; }

        #endregion

        #region Constructors

        public wucLiveLogViewer()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                
            }

            //int primaryScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //int primaryScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            //this.Width = (primaryScreenWidth * 9) / 10;
            //this.Height = (primaryScreenHeight * 9) / 10;
        }

        #endregion

        #region Initialization

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversInstancesViewSource"];
            //// Things work if this line is present.  Testing to see if it works without 6/13/2012
            //// Yup, still works.  Don't need this line as it is done in the XAML.
            //myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Instances;

            System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
            // Things work if this line is present.  Testing to see if it works without 6/13/2012
            // Yup, still works.  Don't need this line as it is done in the XAML.
            //myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Servers;

        }

        #endregion

        #region Private Methods

        private async void ConnectAsync()
        {
            Connection = new HubConnection(ServerURI.Text);
            Connection.Closed += Connection_Closed;
            Connection.Error += Connection_Error;
            Connection.Received += Connection_Received;
            Connection.Reconnected += Connection_Reconnected;
            Connection.Reconnecting += Connection_Reconnecting;
            Connection.StateChanged += Connection_StateChanged;
            HubProxy = Connection.CreateHubProxy("MyHub");

            //Handle incoming event from server: use Invoke to write to console from SignalR's thread

            HubProxy.On<string, string>("AddUserMessage", (name, message) =>
                this.Dispatcher.Invoke(() =>
                {
                    teLogStream.Text += String.Format("{0}: {1}\n", name, message);
                    //recLogStream.Text += String.Format("{0}: {1}\n", name, message);
                }
                )
            );

            HubProxy.On<string>("AddMessage", (message) =>
                this.Dispatcher.Invoke(() =>
                {
                    teLogStream.Text += String.Format("{0}\n", message);
                })
            );

            HubProxy.On<string, Int32>("AddPriorityMessage", (message, priority) =>
                this.Dispatcher.Invoke(() =>
                {
                    Boolean displayMessage = false;

                    // TODO(crhodes)
                    // Make this more clever, perhaps a bit field
                    // But this may be plenty fast enough just long :(

                    switch (priority)
                    {
                        #region Debug

                        case 1000:
                            if (ceDebug00.IsChecked == true) displayMessage = true;
                            break;

                        case 1001:
                            if (ceDebug01.IsChecked == true) displayMessage = true;
                            break;

                        case 1002:
                            if (ceDebug02.IsChecked == true) displayMessage = true;
                            break;

                        case 1003:
                            if (ceDebug03.IsChecked == true) displayMessage = true;
                            break;

                        case 1004:
                            if (ceDebug04.IsChecked == true) displayMessage = true;
                            break;

                        #endregion

                        #region Trace00 - Trace09

                        case 10000:
                            if (ceTrace00.IsChecked == true) displayMessage = true;
                            break;

                        case 10001:
                            if (ceTrace01.IsChecked == true) displayMessage = true;
                            break;

                        case 10002:
                            if (ceTrace02.IsChecked == true) displayMessage = true;
                            break;

                        case 10003:
                            if (ceTrace03.IsChecked == true) displayMessage = true;
                            break;

                        case 10004:
                            if (ceTrace04.IsChecked == true) displayMessage = true;
                            break;

                        case 10005:
                            if (ceTrace05.IsChecked == true) displayMessage = true;
                            break;

                        case 10006:
                            if (ceTrace06.IsChecked == true) displayMessage = true;
                            break;

                        case 10007:
                            if (ceTrace07.IsChecked == true) displayMessage = true;
                            break;

                        case 10008:
                            if (ceTrace08.IsChecked == true) displayMessage = true;
                            break;

                        case 10009:
                            if (ceTrace09.IsChecked == true) displayMessage = true;
                            break;

                        #endregion
                        
                        #region Trace10 - Trace19

                        case 10010:
                            if (ceTrace10.IsChecked == true) displayMessage = true;
                            break;

                        case 10011:
                            if (ceTrace11.IsChecked == true) displayMessage = true;
                            break;

                        case 10012:
                            if (ceTrace12.IsChecked == true) displayMessage = true;
                            break;

                        case 10013:
                            if (ceTrace13.IsChecked == true) displayMessage = true;
                            break;

                        case 10014:
                            if (ceTrace14.IsChecked == true) displayMessage = true;
                            break;

                        case 10015:
                            if (ceTrace15.IsChecked == true) displayMessage = true;
                            break;

                        case 10016:
                            if (ceTrace16.IsChecked == true) displayMessage = true;
                            break;

                        case 10017:
                            if (ceTrace17.IsChecked == true) displayMessage = true;
                            break;

                        case 10018:
                            if (ceTrace18.IsChecked == true) displayMessage = true;
                            break;

                        case 10019:
                            if (ceTrace19.IsChecked == true) displayMessage = true;
                            break;

                        #endregion

                        #region Trace20 - Trace29

                        case 10020:
                            if (ceTrace20.IsChecked == true) displayMessage = true;
                            break;

                        case 10021:
                            if (ceTrace21.IsChecked == true) displayMessage = true;
                            break;

                        case 10022:
                            if (ceTrace22.IsChecked == true) displayMessage = true;
                            break;

                        case 10023:
                            if (ceTrace23.IsChecked == true) displayMessage = true;
                            break;

                        case 10024:
                            if (ceTrace24.IsChecked == true) displayMessage = true;
                            break;

                        case 10025:
                            if (ceTrace25.IsChecked == true) displayMessage = true;
                            break;

                        case 10026:
                            if (ceTrace26.IsChecked == true) displayMessage = true;
                            break;

                        case 10027:
                            if (ceTrace27.IsChecked == true) displayMessage = true;
                            break;

                        case 10028:
                            if (ceTrace28.IsChecked == true) displayMessage = true;
                            break;

                        case 10029:
                            if (ceTrace29.IsChecked == true) displayMessage = true;
                            break;

                        #endregion

                        default:
                            displayMessage = true;
                            break;
                    }

                    if (displayMessage)
                    {
                        teLogStream.Text += String.Format("{0}\n", message);
                    }
                })
            );

            try
            {
                await Connection.Start();
            }
            catch (HttpRequestException)
            {
                StatusText.Content = "Unable to connect to server: Start server before connecting clients.";
                //No connection: Don't enable Send button or show chat UI
                return;
            }

            //Show chat UI; hide login UI
            SignInPanel.Visibility = Visibility.Collapsed;
            ChatPanel.Visibility = Visibility.Visible;
            btnSend.IsEnabled = true;
            btnSendPriority.IsEnabled = true;
            tbMessage.Focus();
            teLogStream.Text += "Connected to server at " + ServerURI + "\n";
            //recLogStream.Text += "Connected to server at " + ServerURI + "\r";
            //rtbLogStream.AppendText("Connected to server at " + ServerURI + "\r");
        }

        #endregion

        #region Connection Events

        void Connection_Reconnected()
        {
            var dispatcher = Application.Current.Dispatcher;

            dispatcher.Invoke(() => teLogStream.Text += "Connection_Reconnected\n");
        }

        void Connection_Reconnecting()
        {
            var dispatcher = Application.Current.Dispatcher;

            dispatcher.Invoke(() => teLogStream.Text += "Connection_Reconnecting\n");
        }

        void Connection_StateChanged(StateChange obj)
        {
            var dispatcher = Application.Current.Dispatcher;
            var message = string.Format("Connection_StateChanged {0,15} -> {1,-15}\n", obj.OldState, obj.NewState);

            dispatcher.Invoke(() => teLogStream.Text += message);
        }

        private void Connection_Received(string obj)
        {
            //var dispatcher = Application.Current.Dispatcher;

            //dispatcher.Invoke(() => teLogStream.Text += "Connection_Received");
        }

        private void Connection_Error(Exception obj)
        {
            var dispatcher = Application.Current.Dispatcher;

            var message = string.Format("Connection_Error >{0}<\n", obj.GetBaseException().ToString());
            dispatcher.Invoke(() => teLogStream.Text += message);
        }

        /// <summary>
        /// If the server is stopped, the connection will time out after 30 seconds (default), and the 
        /// Closed event will fire.
        /// </summary>
        void Connection_Closed()
        {
            //Hide chat UI; show login UI
            var dispatcher = Application.Current.Dispatcher;
            dispatcher.Invoke(() => ChatPanel.Visibility = Visibility.Collapsed);
            dispatcher.Invoke(() => btnSendPriority.IsEnabled = false);
            dispatcher.Invoke(() => btnSend.IsEnabled = false);
            dispatcher.Invoke(() => teLogStream.Text += "You have been disconnected.\n");
            dispatcher.Invoke(() => SignInPanel.Visibility = Visibility.Visible);
        }

        #endregion

        #region Event Handlers


        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            UserName = UserNameTextBox.Text;
            //Connect to server (use async method to avoid blocking UI thread)
            if (!String.IsNullOrEmpty(UserName))
            {
                StatusText.Visibility = Visibility.Visible;
                StatusText.Content = "Connecting to server...";
                ConnectAsync();
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            HubProxy.Invoke("Send", UserName, tbMessage.Text);
            tbMessage.Text = String.Empty;

            tbMessage.Focus();
        }

        private void btnSendPriority_Click(object sender, RoutedEventArgs e)
        {
            HubProxy.Invoke("SendPriority", tbMessage.Text, Int32.Parse(tbMessagePriority.Text));
            tbMessage.Text = String.Empty;
            tbMessage.Focus();
        }

        private void WPFClient_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Connection != null)
            {
                Connection.Stop();
                Connection.Dispose();
            }
        }

        private void teLogStream_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            lbLastEntry.Content = DateTime.Now.ToString("HH:mm:ss.fff");

            if (ceAutoUpdate.IsChecked == true)
            {
                teLogStream.Focus();
                teLogStream.SelectionStart = teLogStream.Text.Length;
            }
        }

        private void btnUpdateScreen_Click(object sender, RoutedEventArgs e)
        {
            teLogStream.Focus();
            teLogStream.SelectionStart = teLogStream.Text.Length;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            teLogStream.Clear();
            //recLogStream.Text = "";
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            teLogStream.SelectAll();
            teLogStream.Copy();
        }

        private void btnInfoToggle_Click(object sender, RoutedEventArgs e)
        {
            if ((String)btnInfoToggle.Content == "All Off")
            {
                ceInfo00.IsChecked = false;
                ceInfo01.IsChecked = false;
                ceInfo02.IsChecked = false;
                ceInfo03.IsChecked = false;
                ceInfo04.IsChecked = false;

                btnInfoToggle.Content = "All On";
            }
            else
            {
                ceInfo00.IsChecked = true;
                ceInfo01.IsChecked = true;
                ceInfo02.IsChecked = true;
                ceInfo03.IsChecked = true;
                ceInfo04.IsChecked = true;

                btnInfoToggle.Content = "All Off";
            }
        }

        private void btnDebugToggle_Click(object sender, RoutedEventArgs e)
        {
            if ((String)btnDebugToggle.Content == "All Off")
            {
                ceDebug00.IsChecked = false;
                ceDebug01.IsChecked = false;
                ceDebug02.IsChecked = false;
                ceDebug03.IsChecked = false;
                ceDebug04.IsChecked = false;

                btnDebugToggle.Content = "All On";
            }
            else
            {
                ceDebug00.IsChecked = true;
                ceDebug01.IsChecked = true;
                ceDebug02.IsChecked = true;
                ceDebug03.IsChecked = true;
                ceDebug04.IsChecked = true;

                btnDebugToggle.Content = "All Off";
            }
        }

        private void btnTrace00_09Toggle_Click(object sender, RoutedEventArgs e)
        {
            if ((String)btnTrace00_09Toggle.Content == "All Off")
            {
                ceTrace00.IsChecked = false;
                ceTrace01.IsChecked = false;
                ceTrace02.IsChecked = false;
                ceTrace03.IsChecked = false;
                ceTrace04.IsChecked = false;
                ceTrace05.IsChecked = false;
                ceTrace06.IsChecked = false;
                ceTrace07.IsChecked = false;
                ceTrace08.IsChecked = false;
                ceTrace09.IsChecked = false;

                btnTrace00_09Toggle.Content = "All On";
            }
            else
            {
                ceTrace00.IsChecked = true;
                ceTrace01.IsChecked = true;
                ceTrace02.IsChecked = true;
                ceTrace03.IsChecked = true;
                ceTrace04.IsChecked = true;
                ceTrace05.IsChecked = true;
                ceTrace06.IsChecked = true;
                ceTrace07.IsChecked = true;
                ceTrace08.IsChecked = true;
                ceTrace09.IsChecked = true;

                btnTrace00_09Toggle.Content = "All Off";
            }
        }

        private void btnTrace10_19Toggle_Click(object sender, RoutedEventArgs e)
        {
            if ((String)btnTrace10_19Toggle.Content == "All Off")
            {
                ceTrace10.IsChecked = false;
                ceTrace11.IsChecked = false;
                ceTrace12.IsChecked = false;
                ceTrace13.IsChecked = false;
                ceTrace14.IsChecked = false;
                ceTrace15.IsChecked = false;
                ceTrace16.IsChecked = false;
                ceTrace17.IsChecked = false;
                ceTrace18.IsChecked = false;
                ceTrace19.IsChecked = false;

                btnTrace10_19Toggle.Content = "All On";
            }
            else
            {
                ceTrace10.IsChecked = true;
                ceTrace11.IsChecked = true;
                ceTrace12.IsChecked = true;
                ceTrace13.IsChecked = true;
                ceTrace14.IsChecked = true;
                ceTrace15.IsChecked = true;
                ceTrace16.IsChecked = true;
                ceTrace17.IsChecked = true;
                ceTrace18.IsChecked = true;
                ceTrace19.IsChecked = true;

                btnTrace10_19Toggle.Content = "All Off";
            }
        }

        private void btnTrace20_29Toggle_Click(object sender, RoutedEventArgs e)
        {
            if ((String)btnTrace20_29Toggle.Content == "All Off")
            {
                ceTrace20.IsChecked = false;
                ceTrace21.IsChecked = false;
                ceTrace22.IsChecked = false;
                ceTrace23.IsChecked = false;
                ceTrace24.IsChecked = false;
                ceTrace25.IsChecked = false;
                ceTrace26.IsChecked = false;
                ceTrace27.IsChecked = false;
                ceTrace28.IsChecked = false;
                ceTrace29.IsChecked = false;

                btnTrace20_29Toggle.Content = "All On";
            }
            else
            {
                ceTrace20.IsChecked = true;
                ceTrace21.IsChecked = true;
                ceTrace22.IsChecked = true;
                ceTrace23.IsChecked = true;
                ceTrace24.IsChecked = true;
                ceTrace25.IsChecked = true;
                ceTrace26.IsChecked = true;
                ceTrace27.IsChecked = true;
                ceTrace28.IsChecked = true;
                ceTrace29.IsChecked = true;

                btnTrace20_29Toggle.Content = "All Off";
            }
        }

        private void btnToggle_Click(object sender, RoutedEventArgs e)
        {
            // TODO(crhodes)
            // Figure out way to explore all child controls inside of capture filter and set IsChecked

            if ((String)btnToggle.Content == "All Off")
            {
                ceInfo00.IsChecked = false;
                ceInfo01.IsChecked = false;
                ceInfo02.IsChecked = false;
                ceInfo03.IsChecked = false;
                ceInfo04.IsChecked = false;

                ceDebug00.IsChecked = false;
                ceDebug01.IsChecked = false;
                ceDebug02.IsChecked = false;
                ceDebug03.IsChecked = false;
                ceDebug04.IsChecked = false;

                ceTrace00.IsChecked = false;
                ceTrace01.IsChecked = false;
                ceTrace02.IsChecked = false;
                ceTrace03.IsChecked = false;
                ceTrace04.IsChecked = false;
                ceTrace05.IsChecked = false;
                ceTrace06.IsChecked = false;
                ceTrace07.IsChecked = false;
                ceTrace08.IsChecked = false;
                ceTrace09.IsChecked = false;

                ceTrace10.IsChecked = false;
                ceTrace11.IsChecked = false;
                ceTrace12.IsChecked = false;
                ceTrace13.IsChecked = false;
                ceTrace14.IsChecked = false;
                ceTrace15.IsChecked = false;
                ceTrace16.IsChecked = false;
                ceTrace17.IsChecked = false;
                ceTrace18.IsChecked = false;
                ceTrace19.IsChecked = false;

                ceTrace20.IsChecked = false;
                ceTrace21.IsChecked = false;
                ceTrace22.IsChecked = false;
                ceTrace23.IsChecked = false;
                ceTrace24.IsChecked = false;
                ceTrace25.IsChecked = false;
                ceTrace26.IsChecked = false;
                ceTrace27.IsChecked = false;
                ceTrace28.IsChecked = false;
                ceTrace29.IsChecked = false;

                btnInfoToggle.Content = "All On";
                btnDebugToggle.Content = "All On";
                btnTrace00_09Toggle.Content = "All On";
                btnTrace10_19Toggle.Content = "All On";
                btnTrace20_29Toggle.Content = "All On";
                btnToggle.Content = "All On";
            }
            else
            {
                ceInfo00.IsChecked = true;
                ceInfo01.IsChecked = true;
                ceInfo02.IsChecked = true;
                ceInfo03.IsChecked = true;
                ceInfo04.IsChecked = true;

                ceDebug00.IsChecked = true;
                ceDebug01.IsChecked = true;
                ceDebug02.IsChecked = true;
                ceDebug03.IsChecked = true;
                ceDebug04.IsChecked = true;

                ceTrace00.IsChecked = true;
                ceTrace01.IsChecked = true;
                ceTrace02.IsChecked = true;
                ceTrace03.IsChecked = true;
                ceTrace04.IsChecked = true;
                ceTrace05.IsChecked = true;
                ceTrace06.IsChecked = true;
                ceTrace07.IsChecked = true;
                ceTrace08.IsChecked = true;
                ceTrace09.IsChecked = true;

                ceTrace10.IsChecked = true;
                ceTrace11.IsChecked = true;
                ceTrace12.IsChecked = true;
                ceTrace13.IsChecked = true;
                ceTrace14.IsChecked = true;
                ceTrace15.IsChecked = true;
                ceTrace16.IsChecked = true;
                ceTrace17.IsChecked = true;
                ceTrace18.IsChecked = true;
                ceTrace19.IsChecked = true;

                ceTrace20.IsChecked = true;
                ceTrace21.IsChecked = true;
                ceTrace22.IsChecked = true;
                ceTrace23.IsChecked = true;
                ceTrace24.IsChecked = true;
                ceTrace25.IsChecked = true;
                ceTrace26.IsChecked = true;
                ceTrace27.IsChecked = true;
                ceTrace28.IsChecked = true;
                ceTrace29.IsChecked = true;

                btnInfoToggle.Content = "All Off";
                btnDebugToggle.Content = "All Off";
                btnTrace00_09Toggle.Content = "All Off";
                btnTrace10_19Toggle.Content = "All Off";
                btnTrace20_29Toggle.Content = "All Off";
                btnToggle.Content = "All Off";
            }
        }

        #endregion
    }

}
