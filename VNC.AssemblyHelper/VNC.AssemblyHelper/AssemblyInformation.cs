using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace VNC.AssemblyHelper
{
    /// <summary>
    /// Wrapper around low level information about an assembly.
    /// </summary>
    public class AssemblyInformation
    {

        #region "Enums, Fields, Properties, Structures"

        private Assembly _Assembly;

        public enum LoadMethod
        {
            LoadFrom,
            LoadFile,
            ReflectionOnlyLoadFrom
        }

        public string AlgorithmIdAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyAlgorithmIdAttribute)AssemblyAlgorithmIdAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyAlgorithmIdAttribute))).AlgorithmId.ToString();

                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string CompanyAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyCompanyAttribute)AssemblyCompanyAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyCompanyAttribute))).Company;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string ConfigurationAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyConfigurationAttribute)AssemblyConfigurationAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyConfigurationAttribute))).Configuration;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string CopyRightAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyCopyrightAttribute)AssemblyCopyrightAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyCopyrightAttribute))).Copyright;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string CultureAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyCultureAttribute)AssemblyCultureAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyCultureAttribute))).Culture;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string DefaultAliasAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyDefaultAliasAttribute)AssemblyDefaultAliasAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyDefaultAliasAttribute))).DefaultAlias;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string DelaySigningAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyDelaySignAttribute)AssemblyDelaySignAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyDelaySignAttribute))).DelaySign.ToString();
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string DescriptionAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyDescriptionAttribute)AssemblyDescriptionAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyDescriptionAttribute))).Description;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string FileVersionAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyFileVersionAttribute)AssemblyFileVersionAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyFileVersionAttribute))).Version;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string InformationalVersionAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyInformationalVersionAttribute)AssemblyInformationalVersionAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyInformationalVersionAttribute))).InformationalVersion;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string KeyFileAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyKeyFileAttribute)AssemblyKeyFileAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyKeyFileAttribute))).KeyFile;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string KeyNameAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyKeyNameAttribute)AssemblyKeyNameAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyKeyNameAttribute))).KeyName;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string ProductAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyProductAttribute)AssemblyProductAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyProductAttribute))).Product;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string TitleAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyTitleAttribute)AssemblyTitleAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyTitleAttribute))).Title;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string TrademarkAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyTrademarkAttribute)AssemblyTrademarkAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyTrademarkAttribute))).Trademark;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string VersionAttribute
        {
            get
            {
                try
                {
                    return ((AssemblyVersionAttribute)AssemblyVersionAttribute.GetCustomAttribute(_Assembly, typeof(AssemblyVersionAttribute))).Version;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string Version
        {
            get
            {
                try
                {
                    AssemblyName asmbName = _Assembly.GetName();
                    return asmbName.Version.ToString();
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        #endregion



        #region "Constructors, Initialization, and Load"

        /// <summary>
        /// Initializes a new instance of the AssemblyInformation class.
        /// </summary>
        public AssemblyInformation()
        {
            // This is probably not interesting as this will be this assembly :).
            _Assembly = Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// Initializes a new instance of the AssemblyInformation class.
        /// </summary>
        public AssemblyInformation(Assembly asmbly)
        {
            _Assembly = asmbly;
        }

        /// <summary>
        /// Initializes a new instance of the AssemblyInformation class.
        /// </summary>
        public AssemblyInformation(string FullPathToAssembly)
        {

            _Assembly = Assembly.LoadFile(FullPathToAssembly); ;
        }

        #endregion

        #region "Event Handlers"


        #endregion

        #region "Main Methods"

        public Type[] GetTypes()
        {
            return _Assembly.GetTypes();
        }

        public MethodInfo[] GetMethods(Type type)
        {
            return type.GetMethods();
        }

        public static string GetAllConfigInfo()
        {
            StringBuilder info = new StringBuilder(4096);

            Type ty = typeof(MethodInfo);

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

        public override string ToString()
        {
            StringBuilder appInfo = new StringBuilder(256);

            appInfo.AppendLine(string.Format("{0} {1}", "AlgorithmIdAttribute:", AlgorithmIdAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "CompanyAttribute:", CompanyAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "ConfigurationAttribute:", ConfigurationAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "CopyRightAttribute:", CopyRightAttribute));            
            appInfo.AppendLine(string.Format("{0} {1}", "CultureAttribute:", CultureAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "DefaultAliasAttribute:", DefaultAliasAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "DelaySigningAttribute:", DelaySigningAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "DescriptionAttribute:", DescriptionAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "FileVersionAttribute:", FileVersionAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "InformationalVersionAttribute:", InformationalVersionAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "KeyFileAttribute:", KeyFileAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "KeyNameAttribute:", KeyNameAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "KeyNameAttribute:", KeyNameAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "ProductAttribute:", ProductAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "TitleAttribute:", TitleAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "TrademarkAttribute:", TrademarkAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "VersionAttribute:", VersionAttribute));
            appInfo.AppendLine(string.Format("{0} {1}", "Version:", Version));

            return appInfo.ToString();
        }

        #endregion

        #region "Utility Methods"


        #endregion

        #region "Protected Methods"


        #endregion

        #region "Private Methods"


        #endregion


    }
}
