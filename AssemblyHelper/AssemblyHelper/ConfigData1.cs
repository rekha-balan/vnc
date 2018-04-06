using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
//using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace AssemblyHelper
{
    /// <summary>
    /// ConfigData1
    /// 
    /// This class holds all configuration data read from the configuration information passed
    /// in the Reload() method when the initial UserControl is loaded.
    /// 
    /// To use this class, 
    /// 1. Add code in the Reload() method of the initial UserControl to store the ConfigData1
    ///    using the RawXML property.  This is done automatically in ucBase.
    /// 2. Add properties to surface the configuration data.  Mark as static.
    ///    Initialize backing store variables!
    /// </summary>
    /// <remarks>Place only static items in this class.  This Class cannot be instantiated.</remarks>
    internal static class ConfigData1
    {
        public static string GetAllConfigInfo()
        {
            StringBuilder info = new StringBuilder(4096);

            Type ty = typeof(ConfigData1);

            object[] methodArgs = new object[1];

            MethodInfo[] methods = ty.GetMethods();

            foreach (MethodInfo method in methods)
            {
                //Common.WriteToDebugWindow(string.Format("Name: {0} ReturnType: {1}", method.Name, method.ReturnType));

                if (method.Name.StartsWith("get"))
                {
                    string value;

                    switch (method.ReturnType.ToString())
                    {
                        case "System.Boolean":
                            try
                            {
                                value = ((Boolean)method.Invoke(ty, null)).ToString();
                            }
                            catch (Exception)
                            {
                                value = "<NULL>";
                            }
                            break;

                        case "System.String":
                            try
                            {
                                value = (string)method.Invoke(ty, null);
                            }
                            catch (Exception)
                            {
                                value = "<NULL>";
                            }

                            break;

                        case "System.Int32":
                            try
                            {
                                value = ((Int32)method.Invoke(ty, null)).ToString();
                            }
                            catch (Exception)
                            {
                                value = "<NULL>";
                            }
                            break;

                        default:
                            value = "Unknown Return Type:" + method.ReturnType.ToString();
                            break;
                    }

                    // Display get_X() as ReturnType Config.X >Value<
                    info.AppendLine(string.Format("{0,16} {1,50} >{2}<",
                        method.ReturnType, "ConfigData1." + method.Name.Substring(4), value));
                }
            }

            return info.ToString();
        }
        
        
    }
}

