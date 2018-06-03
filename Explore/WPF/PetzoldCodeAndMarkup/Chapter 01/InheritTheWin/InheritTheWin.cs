//----------------------------------------------
// InheritTheWin.cs (c) 2005 by Charles Petzold
//----------------------------------------------
using System;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Petzold.InheritTheWin
{
    class InheritTheWin : Window
    {
        [STAThread]
        public static void Main()
        {
            //Application app = new Application();
            //app.Run(new InheritTheWin());
            new Application().Run(new InheritTheWin());
        }
        public InheritTheWin()
        {
            Title = "Inherit the Win";

            StringBuilder sb = new StringBuilder();

            // TODO(crhodes)
            // Redo this with reflection and dump all SystemParameters


            sb.AppendFormat("SystemParameters\n");
            sb.Append(GetSystemParameters());
            //sb.AppendFormat("  FullPrimaryScreenHeight ({0})", SystemParameters.FullPrimaryScreenHeight);
            //sb.AppendFormat("  FullPrimaryScreenWidth ({0})", SystemParameters.FullPrimaryScreenWidth);
            //sb.AppendFormat("  MaximizedPrimaryScreenHeight ({0})", SystemParameters.MaximizedPrimaryScreenHeight);
            //sb.AppendFormat("  MaximizedPrimaryScreenWidth ({0})", SystemParameters.MaximizedPrimaryScreenWidth);
            //sb.AppendFormat("  MinimizedWindowHeight ({0})", SystemParameters.MinimizedWindowHeight);
            //sb.AppendFormat("  MinimizedWindowWidth ({0})", SystemParameters.MinimizedWindowWidth);
            //sb.AppendFormat("  MinimumWindowHeight ({0})", SystemParameters.MinimumWindowHeight);
            //sb.AppendFormat("  MinimumWindowWidth ({0})", SystemParameters.MinimumWindowWidth);


            // This should be 3" * 2"

            Width = 288;
            Height = 192;

            // Default is 1/96" per pixel

            //Width = 100 * Math.PI;
            //Height = 100 * Math.E;
            //Left = 500;
            //Top = 100;
            Content = sb.ToString();
        }

        string GetSystemParameters()
        {
            PropertyInfo[] properties = typeof(SystemParameters).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);



            StringBuilder result = new StringBuilder();

            foreach (PropertyInfo p in properties)
            {
                // Only work with strings
                //if (p.PropertyType != typeof(string)) { continue; }

                // If not writable then cannot null it; if not readable then cannot check it's value
                //if (!p.CanWrite || !p.CanRead) { continue; }

                result.AppendFormat("  {0} - ({1})\n", p.Name, p.GetValue(null, null));

                //MethodInfo mget = p.GetGetMethod(false);
                //MethodInfo mset = p.GetSetMethod(false);

                //// Get and set methods have to be public
                //if (mget == null) { continue; }
                //if (mset == null) { continue; }

                //foreach (T item in list)
                //{
                //    if (string.IsNullOrEmpty((string)p.GetValue(item, null)))
                //    {
                //        p.SetValue(item, replacement, null);
                //    }
                //}
            }

            return result.ToString();
        }
    }
}