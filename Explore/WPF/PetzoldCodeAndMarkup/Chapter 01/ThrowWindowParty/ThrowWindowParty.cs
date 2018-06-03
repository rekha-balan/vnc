//-------------------------------------------------
// ThreeWindowParty.cs (c) 2005 by Charles Petzold
//-------------------------------------------------
using System;
using System.Windows;
using System.Windows.Input;

namespace Petzold.ThrowWindowParty
{
    class ThrowWindowParty: Application
    {
        [STAThread]
        public static void Main()
        {
            ThrowWindowParty app = new ThrowWindowParty();
            // By default program ends after all windows closed (OnLastWindowClose)

            // Change behavior to
            //app.ShutdownMode = ShutdownMode.OnExplicitShutdown; // Must call Shutdown
            //app.ShutdownMode = ShutdownMode.OnLastWindowClose;
            //app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.Run();


        }
        protected override void OnStartup(StartupEventArgs args)
        {
            Window winMain = new Window();
            winMain.Title = "Main Window";
            winMain.MouseDown += WindowOnMouseDown;
            winMain.Show();

            for (int i = 0; i < 4; i++)
            {
                Window win = new Window();
                win.Title = "Extra Window No. " + (i + 1);
                // By default all windows show in taskbar.
                if (i % 2 == 0)
                {
                    win.ShowInTaskbar = false;
                }

                // By default the first window is the MainWindow
                // Can change to be any window
                //MainWindow = win;

                // Windows can have owners, default - none
                // This makes them ModeLess dialog boxes
                win.Owner = winMain;
                win.Show();
            }
        }
        void WindowOnMouseDown(object sender, MouseButtonEventArgs args)
        {
            if (args.ChangedButton == MouseButton.Right)
            {
                //app.ShutdownMode = ShutdownMode.OnExplicitShutdown; // Must call Shutdown
                // Explicitly call Shutdown so app will terminate
                
                Shutdown();
            }

            Window win = new Window();
            win.Title = "Modal Dialog Box";
            win.ShowDialog();
        }
    }
}

