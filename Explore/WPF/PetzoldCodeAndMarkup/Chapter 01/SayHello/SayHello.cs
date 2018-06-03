//-----------------------------------------
// SayHello.cs (c) 2006 by Charles Petzold
//-----------------------------------------
using System;
using System.Windows;

namespace Petzold.SayHello
{
    class SayHello
    {
        [STAThread]
        public static void Main()
        {
            Window win = new Window();
            win.Title = "Say Hello";
            //win.Show();

            // This starts the message loop
            Application app = new Application();

            //app.Run();

            // Alternatively can pass the window to Run() which will call Show()

            app.Run(win);
        }
    }
}
