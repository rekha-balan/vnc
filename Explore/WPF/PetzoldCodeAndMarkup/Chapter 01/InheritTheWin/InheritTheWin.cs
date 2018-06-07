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

            double DPI = 1.0 / 96.0;
            sb.AppendFormat("SystemParameters\n");
            //sb.Append(GetSystemParameters());
            sb.AppendFormat("  FullPrimaryScreenHeight      ({0}) ({1})\n",
                SystemParameters.FullPrimaryScreenHeight, SystemParameters.FullPrimaryScreenHeight * DPI);
            sb.AppendFormat("  FullPrimaryScreenWidth       ({0}) ({1})\n", 
                SystemParameters.FullPrimaryScreenWidth, SystemParameters.FullPrimaryScreenWidth * DPI);
            sb.AppendFormat("  MaximizedPrimaryScreenHeight ({0}) ({1})\n",
                SystemParameters.MaximizedPrimaryScreenHeight, SystemParameters.MaximizedPrimaryScreenHeight * DPI);
            sb.AppendFormat("  MaximizedPrimaryScreenWidth  ({0}) ({1})\n", 
                SystemParameters.MaximizedPrimaryScreenWidth, SystemParameters.MaximizedPrimaryScreenWidth * DPI);
            sb.AppendFormat("  MinimizedWindowHeight        ({0}) ({1})\n", 
                SystemParameters.MinimizedWindowHeight, SystemParameters.MinimizedWindowHeight * DPI);
            sb.AppendFormat("  MinimizedWindowWidth         ({0}) ({1})\n", 
                SystemParameters.MinimizedWindowWidth, SystemParameters.MinimizedWindowWidth * DPI);
            sb.AppendFormat("  MinimumWindowHeight          ({0}) ({1})\n", 
                SystemParameters.MinimumWindowHeight, SystemParameters.MinimumWindowHeight * DPI);
            sb.AppendFormat("  MinimumWindowWidth           ({0}) ({1})\n", 
                SystemParameters.MinimumWindowWidth, SystemParameters.MinimumWindowWidth * DPI);
            sb.AppendLine();
            sb.AppendFormat("  WorkArea.Top                 ({0})\n", SystemParameters.WorkArea.Top);
            sb.AppendFormat("  WorkArea.Right               ({0})\n", SystemParameters.WorkArea.Right);
            sb.AppendFormat("  WorkArea.Bottom              ({0})\n", SystemParameters.WorkArea.Bottom);
            sb.AppendFormat("  WorkArea.Left                ({0})\n", SystemParameters.WorkArea.Left);
            sb.AppendFormat("  WorkArea.Width               ({0})\n", SystemParameters.WorkArea.Width);
            sb.AppendFormat("  WorkArea.Height              ({0})\n", SystemParameters.WorkArea.Height);
            sb.AppendFormat("  WorkArea.TopLeft             ({0},{1})\n", SystemParameters.WorkArea.TopLeft.X, SystemParameters.WorkArea.TopLeft.Y);
            sb.AppendFormat("  WorkArea.BottomRight         ({0},{1})\n", SystemParameters.WorkArea.BottomRight.X, SystemParameters.WorkArea.BottomRight.Y);

            // This should be 6" * 4"

            Width = 576;
            Height = 384;

            // Default is 1/96" per pixel

            //Width = 100 * Math.PI;
            //Height = 100 * Math.E;
            //Left = 500;
            //Top = 100;

            // Put in lower Right corner

            Left = SystemParameters.PrimaryScreenWidth - Width;
            Top = SystemParameters.PrimaryScreenHeight - Height;

            // Put in lower Right corner of work area

            Left = SystemParameters.WorkArea.Width - Width;
            Top = SystemParameters.WorkArea.Height - Height;

            // Put in Upper Right corner

            Left = SystemParameters.WorkArea.Width - Width;
            Top = 0;

            // Put in Upper Right corner of work area

            //Left = SystemParameters.WorkArea.Width - Width;
            Top = SystemParameters.WorkArea.Top;

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