using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace SignalRServerHub
{
    /// <summary>
    /// WPF host for a SignalR server. The host can stop and start the SignalR
    /// server, report errors when trying to start the server on a URI where a
    /// server is already being hosted, and monitor when clients connect and disconnect. 
    /// The hub used in this server is a simple echo service, and has the same 
    /// functionality as the other hubs in the SignalR Getting Started tutorials.
    /// For simplicity, MVVM will not be used for this sample.
    /// </summary>
    public partial class MainWindow : Window
    {
        public IDisposable SignalR { get; set; }
        string ServerURI = "http://localhost:8095";

        public MainWindow()
        {
            InitializeComponent();

            VNC.AssemblyHelper.AssemblyInformation info = new VNC.AssemblyHelper.AssemblyInformation(System.Reflection.Assembly.GetExecutingAssembly());

            Title = Title + " " +  info.InformationalVersionAttribute;
        }

        
        /// <summary>
        /// Calls the StartServer method with Task.Run to not
        /// block the UI thread. 
        /// </summary>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            WriteToConsole("Starting server...");
            ServerURI = tbServerURI.Text;
            ButtonStart.IsEnabled = false;            
            Task.Run(() => StartServer());
        }

        /// <summary>
        /// Stops the server and closes the form. Restart functionality omitted
        /// for clarity.
        /// </summary>
        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            SignalR.Dispose();
            Close();
        }

        /// <summary>
        /// Starts the server and checks for error thrown when another server is already 
        /// running. This method is called asynchronously from Button_Start.
        /// </summary>
        private void StartServer()
        {
            try
            {
                SignalR = WebApp.Start(ServerURI);
            }
            catch (TargetInvocationException ex)
            {
                WriteToConsole(ex.ToString());
                WriteToConsole("A server is already running at " + ServerURI);
                this.Dispatcher.Invoke(() => ButtonStart.IsEnabled = true);
                return;
            }
            catch (Exception ex)
            {
                WriteToConsole(ex.ToString());
            }

            this.Dispatcher.Invoke(() => ButtonStop.IsEnabled = true);
            WriteToConsole("Server started at " + ServerURI);
        }

        ///This method adds a line to the RichTextBoxConsole control, using Dispatcher.Invoke if used
        /// from a SignalR hub thread rather than the UI thread.
        public void WriteToConsole(String message)
        {
            if (!(RichTextBoxConsole.CheckAccess()))
            {
                this.Dispatcher.Invoke(() =>
                    WriteToConsole(message)
                );
                return;
            }

            RichTextBoxConsole.AppendText(message + "\r");
        }

        private void tbServerURI_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ServerURI = tbServerURI.Text;
        }
    }

    /// <summary>
    /// Used by OWIN's startup process. 
    /// </summary>
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }

    /// <summary>
    /// Echoes messages sent using the Send message by calling the
    /// addMessage method on the client. Also reports to the console
    /// when clients connect and disconnect.
    /// </summary>
    public class MyHub : Hub
    {
        public void Send(string name, string message)
        {
            try
            {
                Clients.All.addUserMessage(name, message);
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                    ((MainWindow)Application.Current.MainWindow).WriteToConsole(ex.ToString()));
            }
            
        }

        public void Send(string message)
        {
            try
            {
                Clients.All.addMessage(message);
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                    ((MainWindow)Application.Current.MainWindow).WriteToConsole(ex.ToString()));
            }          
        }

        public void SendPriority(string message, Int32 priority)
        {
            try
            {
                Clients.All.addPriorityMessage(message, priority);               
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                    ((MainWindow)Application.Current.MainWindow).WriteToConsole(ex.ToString()));
            }
        }

        public override Task OnConnected()
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            Application.Current.Dispatcher.Invoke(() => 
                ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client connected: " + Context.ConnectionId));

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client Reconnected: " + Context.ConnectionId));

            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client disconnected: " + Context.ConnectionId));


            return base.OnDisconnected(stopCalled);
        }
    }
}
