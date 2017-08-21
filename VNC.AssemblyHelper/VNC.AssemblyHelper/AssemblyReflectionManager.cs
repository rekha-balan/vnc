using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Based on Marius Bancila article
// https://www.codeproject.com/Articles/453778/Loading-Assemblies-from-Anywhere-into-a-New-AppDom

namespace VNC.AssemblyHelper
{

    public class AssemblyReflectionManager : IDisposable
    {
        Dictionary<string, AppDomain> _mapDomains = new Dictionary<string, AppDomain>();
        Dictionary<string, AppDomain> _loadedAssemblies = new Dictionary<string, AppDomain>();
        Dictionary<string, AssemblyReflectionProxy> _proxies = new Dictionary<string, AssemblyReflectionProxy>();

        private AppDomain CreateChildDomain(AppDomain parentDomain, string domainName)
        {
            Evidence evidence = new Evidence(parentDomain.Evidence);
            AppDomainSetup setup = parentDomain.SetupInformation;

            return AppDomain.CreateDomain(domainName, evidence, setup);
        }

        public Boolean LoadAssembly(String assemblyPath, String domainName)
        {
            // If the assembly file does not exist then fail

            if (! File.Exists(assemblyPath))
            {
                {
                    return false;
                }
            }

            // If the assembly was already Loaded then fail

            if (_loadedAssemblies.ContainsKey(assemblyPath))
            {
                {
                    return false;
                }
            }

            // Check if the app domain exists; if not, create a new one

            AppDomain appDomain = null;

            if (_mapDomains.ContainsKey(domainName))
            {
                {
                    appDomain = _mapDomains[domainName];
                }
            }
            else
            {
                appDomain = CreateChildDomain(AppDomain.CurrentDomain, domainName);
                _mapDomains[domainName] = appDomain;
            }

            // Load the assembly in the specified app domain

            try
            {
                Type proxyType = typeof(AssemblyReflectionProxy);

                if (proxyType.Assembly != null)
                {
                    {
                        var proxy = (AssemblyReflectionProxy)appDomain.CreateInstanceFrom(
                            proxyType.Assembly.Location,
                            proxyType.FullName).Unwrap();

                        proxy.LoadAssembly(assemblyPath);

                        _loadedAssemblies[assemblyPath] = appDomain;
                        _proxies[assemblyPath] = proxy;

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // 
            }

            return false;
        }

        public bool UnloadAssembly(String assemblyPath)
        {
            if (! File.Exists(assemblyPath))
            {
                {
                    return false;
                }
            }

            // Check if the assembly is found in the internal dictionaries

            if (_loadedAssemblies.ContainsKey(assemblyPath) && _proxies.ContainsKey(assemblyPath))
            {
                {
                    // Check if there are more assemblies loaded in the same app domain
                    // In this case fail

                    AppDomain appDomain = _loadedAssemblies[assemblyPath];
                    int count = _loadedAssemblies.Values.Count(a => a == appDomain);

                    if (count != 1)
                    {
                        return false;
                    }

                    try
                    {
                        // Remove app domain from the dictionary and unloaded it from the process

                        _mapDomains.Remove(appDomain.FriendlyName);
                        AppDomain.Unload(appDomain);

                        // Remove the assembly from the dictionaries

                        _loadedAssemblies.Remove(assemblyPath);
                        _proxies.Remove(assemblyPath);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        // 
                    }
                }
            }

            return false;
        }

        public bool UnloadDomain(String domainName)
        {
            // Check the app domain name is valid

            if (string.IsNullOrEmpty(domainName))
            {
                {
                    return false;
                }
            }

            // Check we have an instance of the domain

            if (_mapDomains.ContainsKey(domainName))
            {
                {
                    try
                    {
                        var appDomain = _mapDomains[domainName];

                        // Check the assemblies that are loaded in this app domain

                        var assemblies = new List<String>();

                        foreach (var kvp in _loadedAssemblies)
                        {
                            if (kvp.Value == appDomain)
                            {
                                assemblies.Add(kvp.Key);
                            }
                        }

                        // Remove these assemblies from the internal dictionaries

                        foreach (var assemblyName in assemblies)
                        {
                            _loadedAssemblies.Remove(assemblyName);
                            _proxies.Remove(assemblyName);
                        }

                        // Remove the app domain from the dictionary

                        _mapDomains.Remove(domainName);

                        // Unload the app domain

                        AppDomain.Unload(appDomain);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        // 
                    }
                }
            }

            return false;
        }

        public List<MethodInformation> GetMethodInformation(String assemblyPath)
        {
            List<MethodInformation> results = new List<MethodInformation>();

            // Check if the assembly is found in the internal dictionaries

            if (_loadedAssemblies.ContainsKey(assemblyPath) && _proxies.ContainsKey(assemblyPath))
            {
                {
                    {
                        results = _proxies[assemblyPath].AllMethodInformation();
                    }
                }
            }

            return results;
        }

        public List<TypeInformation> GetTypeInformation(String assemblyPath)
        {
            List<TypeInformation> results = new List<TypeInformation>();

            // Check if the assembly is found in the internal dictionaries

            if (_loadedAssemblies.ContainsKey(assemblyPath) && _proxies.ContainsKey(assemblyPath))
            {
                {
                    {
                        results = _proxies[assemblyPath].AllTypeInformation();
                    }
                }
            }

            return results;
        }

        public List<ValueTypeInformation> GetStructureInformation(String assemblyPath)
        {
            List<ValueTypeInformation> results = new List<ValueTypeInformation>();

            // Check if the assembly is found in the internal dictionaries

            if (_loadedAssemblies.ContainsKey(assemblyPath) && _proxies.ContainsKey(assemblyPath))
            {
                {
                    {
                        results = _proxies[assemblyPath].AllStructureInformation();
                    }
                }
            }

            return results;
        }

        public TResult Reflect<TResult>(String assemblyPath, Func<Assembly, TResult> func)
        {
            // Check if the assembly is found in the internal dictionaries

            if (_loadedAssemblies.ContainsKey(assemblyPath) && _proxies.ContainsKey(assemblyPath))
            {
                {
                    return _proxies[assemblyPath].Reflect(func);
                }
            }

            return default(TResult);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (! disposedValue)
            {
                {
                    if (disposing)
                    {
                        foreach (var appDomain in _mapDomains.Values)
                        {
                            AppDomain.Unload(appDomain);
                        }

                        _loadedAssemblies.Clear();
                        _proxies.Clear();
                        _mapDomains.Clear();
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                    // TODO: set large fields to null.

                    disposedValue = true;
                }
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~AssemblyReflectionManager()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
