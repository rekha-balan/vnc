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
    public partial class wucLiveLogEditor : wucDXBase
    {
        public String UserName { get; set; }
        public IHubProxy HubProxy { get; set; }
        //private string ServerURI = "http://localhost:8095/signalr";
        public HubConnection Connection { get; set; }

        public wucLiveLogEditor()
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

            //serversGridControl.GroupBy("Environment");
            //serversGridControl.Set

            // Update the views.  First ensure a row is selected.

            //tableView1.FocusedRowHandle = 1;

            //tableView1.BestFitColumns();
            //tableView2.BestFitColumns();
            //tableView3.BestFitColumns();
            ////tableView4.BestFitColumns();
            ////tableView5.BestFitColumns();
            ////tableView6.BestFitColumns();
            //tableView7.BestFitColumns();
            //tableView8.BestFitColumns();

            //serversGridControl.GroupBy("SecurityZone");
        }



        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            HubProxy.Invoke("Send", UserName, TextBoxMessage.Text);
            TextBoxMessage.Text = String.Empty;
            TextBoxMessage.Focus();
        }

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

        private async void ConnectAsync()
        {
            Connection = new HubConnection(ServerURI.Text);
            Connection.Closed += Connection_Closed;
            HubProxy = Connection.CreateHubProxy("MyHub");
            //Handle incoming event from server: use Invoke to write to console from SignalR's thread

            HubProxy.On<string, string>("AddUserMessage", (name, message) =>
                this.Dispatcher.Invoke(() =>
                {
                    //teLogStream.Text += String.Format("{0}: {1}\n", name, message);
                    recLogStream.Text += String.Format("{0}: {1}\n", name, message);
                }
                )
            );

            HubProxy.On<string>("AddMessage", (message) =>
                this.Dispatcher.Invoke(() =>
                {
                    //teLogStream.Text += String.Format("{0}\n", message);
                    recLogStream.Text += String.Format("{0}\r", message);


                    //if (message.Contains("1002"))
                    //{
                    //    Document doc = recLogStream.Document;
                    //    DocumentPosition docPosition = doc.CaretPosition;
                    //    //recLogStream.Document.InsertRtfText(docPosition, message);
                    //    string boldLeader = @"{\rtlch\fcs1 \ab\af0\afs48 \ltrch\fcs0 \b\fs48\cf1\insrsid5995062 }";
                    //    string boldTrailer = @"{\rtlch\fcs1 \ab\af0\afs48 \ltrch\fcs0 \b\fs48\insrsid5995062 \par }";

                    //    string formattedMessage = string.Format("{0}{1}{2}\n", boldLeader, message, boldTrailer);

                    //    doc.BeginUpdate();

                    //    recLogStream.Document.AppendRtfText(formattedMessage);

                    //    doc.EndUpdate();
                    //    //recLogStream.Document.InsertRtfText(docPosition, formattedMessage);

                    //    //recLogStream.RtfText += formattedMessage;

                    //    //Range range = new Range();

                    //    //CharacterProperties cp = doc.BeginUpdateCharacters(docPosition.);

                    //    //recLogStream.RtfText += range.ToString();
                    //}
                    //else
                    //{

                    //    recLogStream.Document.AppendRtfText(string.Format("{0}\n", message));
                    //    //recLogStream.RtfText += String.Format("{0}\n", message);
                    //}
                }

                )
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
            ButtonSend.IsEnabled = true;
            TextBoxMessage.Focus();
            //teLogStream.Text += "Connected to server at " + ServerURI + "\r";
            recLogStream.Text += "Connected to server at " + ServerURI + "\r";
            //rtbLogStream.AppendText("Connected to server at " + ServerURI + "\r");
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
            dispatcher.Invoke(() => ButtonSend.IsEnabled = false);
            dispatcher.Invoke(() => StatusText.Content = "You have been disconnected.");
            dispatcher.Invoke(() => SignInPanel.Visibility = Visibility.Visible);
        }

        private void WPFClient_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Connection != null)
            {
                Connection.Stop();
                Connection.Dispose();
            }
        }

        //private void teLogStream_EditValueChanged(object sender, EditValueChangedEventArgs e)
        //{
        //    teLogStream.Focus();
        //    teLogStream.SelectionStart = teLogStream.Text.Length;
        //}

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //teLogStream.Clear();
            recLogStream.Text = "";
        }

        private void recLogStream_TextChanged(object sender, EventArgs e)
        {
            // TODO(crhodes)
            // Figure out how to scroll the RichEditControl
        }


        //private void saveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
        //}

        //private void undoButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Common.ApplicationDataSet.Instances.RejectChanges();
        //}

        //private void DXWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        //{

        //}

    }

}
