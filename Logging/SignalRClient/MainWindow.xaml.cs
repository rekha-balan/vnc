﻿using System;
using System.Net.Http;
using System.Threading;
using System.Windows;
using Microsoft.AspNet.SignalR.Client;

namespace SignalRClient
{   
    /// <summary>
    /// SignalR client hosted in a WPF application. The client
    /// lets the user pick a user name, connect to the server asynchronously
    /// to not block the UI thread, and send chat messages to all connected 
    /// clients whether they are hosted in WinForms, WPF, or a web application.
    /// For simplicity, MVVM will not be used for this sample.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// This name is simply added to sent messages to identify the user; this 
        /// sample does not include authentication.
        /// </summary>
        public String UserName { get; set; }
        public IHubProxy HubProxy { get; set; }
        const string ServerURI = "http://localhost:8095/signalr";
        public HubConnection Connection { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            VNC.AssemblyHelper.AssemblyInformation info = new VNC.AssemblyHelper.AssemblyInformation(System.Reflection.Assembly.GetExecutingAssembly());

            Title = Title + " " + info.InformationalVersionAttribute;
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            HubProxy.Invoke("Send", UserName, TextBoxMessage.Text);
            TextBoxMessage.Text = String.Empty;
            TextBoxMessage.Focus();
        }

        private void btnSendAnoymous_Click(object sender, RoutedEventArgs e)
        {
            HubProxy.Invoke("Send", TextBoxMessage.Text);
            TextBoxMessage.Text = String.Empty;
            TextBoxMessage.Focus();
        }

        /// <summary>
        /// Creates and connects the hub connection and hub proxy. This method
        /// is called asynchronously from SignInButton_Click.
        /// </summary>
        private async void ConnectAsync()
        {
            Connection = new HubConnection(ServerURI);
            Connection.Closed += Connection_Closed;
            Connection.Error += Connection_Error;
            //Connection.Received += Connection_Received;
            Connection.Reconnected += Connection_Reconnected;
            Connection.Reconnecting += Connection_Reconnecting;
            Connection.StateChanged += Connection_StateChanged;

            HubProxy = Connection.CreateHubProxy("MyHub");
            //Handle incoming event from server: use Invoke to write to console from SignalR's thread
            HubProxy.On<string, string>("AddUserMessage", (name, message) =>
                this.Dispatcher.Invoke(() =>
                    ListBoxConsole.Items.Add(String.Format("{0}: {1}", name, message))
                )
            );

            HubProxy.On<string>("AddMessage", (message) =>
                this.Dispatcher.Invoke(() =>
                    ListBoxConsole.Items.Add(String.Format("{0}", message))
                )
            );

            try
            {
                await Connection.Start();
            }
            catch (HttpRequestException)
            {
                StatusText.Items.Add("Unable to connect to server: Start server before connecting clients.");
                //No connection: Don't enable Send button or show chat UI
                return;
            }
            catch (Exception ex)
            {
                StatusText.Items.Add(ex.ToString());
                return;
            }

            //Show chat UI; hide login UI
            SignInPanel.Visibility = Visibility.Collapsed;
            ChatPanel.Visibility = Visibility.Visible;
            ButtonSend.IsEnabled = true;
            ButtonSendAnoymous.IsEnabled = true;
            TextBoxMessage.Focus();
            ListBoxEvents.Items.Add("Connected to server at " + ServerURI);
        }

        #region Connection Events

        void Connection_Reconnected()
        {
            var dispatcher = Application.Current.Dispatcher;

            var message = string.Format("Connection_Reconnected");
            Dispatcher.Invoke(() => ListBoxEvents.Items.Add(message));
        }

        void Connection_Reconnecting()
        {
            var dispatcher = Application.Current.Dispatcher;

            var message = string.Format("Connection_Reconnecting");
            Dispatcher.Invoke(() => ListBoxEvents.Items.Add(message));
        }

        void Connection_StateChanged(StateChange obj)
        {
            var dispatcher = Application.Current.Dispatcher;
            
            var message = string.Format("Connection_StateChanged {0,15} -> {1,-15}", obj.OldState, obj.NewState);
            Dispatcher.Invoke(() => ListBoxEvents.Items.Add(message));
        }

        private void Connection_Received(string obj)
        {
            var dispatcher = Application.Current.Dispatcher;

            var message = string.Format("Connection_Received");

            Dispatcher.Invoke(() => ListBoxEvents.Items.Add(message));
        }

        private void Connection_Error(Exception obj)
        {
            var dispatcher = Application.Current.Dispatcher;

            var message = string.Format("Connection_Error >{0}<", obj.GetBaseException().ToString());
            Dispatcher.Invoke(() => ListBoxEvents.Items.Add(message));
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
            Dispatcher.Invoke(() => ListBoxEvents.Items.Add(String.Format("{0}", "Connection_Closed")));
            dispatcher.Invoke(() => SignInPanel.Visibility = Visibility.Visible);
        }

        #endregion

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            UserName = UserNameTextBox.Text;
            //Connect to server (use async method to avoid blocking UI thread)
            if (!String.IsNullOrEmpty(UserName))
            {     
                StatusText.Visibility = Visibility.Visible;
                StatusText.Items.Add("Connecting to server...");
                ConnectAsync();
            }
        }

        private void WPFClient_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Connection != null)
            {
                Connection.Stop();
                Connection.Dispose();
            }
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            long startTicks = VNC.AppLog.Info("Enter","SignalRClient");

            Thread.Sleep(int.Parse(tbDelayMS.Text));

            VNC.AppLog.Info("Exit", "SignalRClient", startTicks);
        }

        private void Debug_Click(object sender, RoutedEventArgs e)
        {
            VNC.AppLog.Debug("Debug", "SignalRClient");
        }

        private void Debug1_Click(object sender, RoutedEventArgs e)
        {
            VNC.AppLog.Debug1("Debug", "SignalRClient");
        }

        private void Debug2_Click(object sender, RoutedEventArgs e)
        {
            VNC.AppLog.Debug2("Debug2", "SignalRClient");
        }

        private void Debug3_Click(object sender, RoutedEventArgs e)
        {
            VNC.AppLog.Debug3("Debug3", "SignalRClient");
        }

        private void Debug4_Click(object sender, RoutedEventArgs e)
        {
            VNC.AppLog.Debug4("Debug4", "SignalRClient");
        }

        private void Trace_Click(object sender, RoutedEventArgs e)
        {
            VNC.AppLog.Trace("Trace", "SignalRClient");
        }

        private void Trace1_Click(object sender, RoutedEventArgs e)
        {
            VNC.AppLog.Trace1("Trace1", "SignalRClient");
        }

        private void Trace2_Click(object sender, RoutedEventArgs e)
        {
            VNC.AppLog.Trace2("Trace2", "SignalRClient");
        }

        private void Trace3_Click(object sender, RoutedEventArgs e)
        {
            VNC.AppLog.Trace3("Trace3", "SignalRClient");
        }

        private void Trace4_Click(object sender, RoutedEventArgs e)
        {
            VNC.AppLog.Trace4("Trace4", "SignalRClient");
        }
    }
}
