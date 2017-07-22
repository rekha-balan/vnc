using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Security.Policy;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

// From Sacha Barber
// https://www.codeproject.com/Articles/42312/Loading-Assemblies-in-Separate-Directories-Into-a

namespace ConsoleApplication1
{

    /// Loads an assembly into a new AppDomain and obtains all the
    /// namespaces in the loaded Assembly, which are returned as a 
    /// List. The new AppDomain is then Unloaded.
    /// 
    /// This class creates a new instance of a AssemblyLoader
    /// which does the actual ReflectionOnly loading 
    /// of the Assembly into the new AppDomain.

    public class SeparateAppDomainAssemblyLoader
    {
        #region Public Methods

        public List<String> LoadAssembly(FileInfo assemblyLocation)
        {
            List<String > namespaces = new List<String > ();

            if (string.IsNullOrEmpty(assemblyLocation.Directory.FullName))
            {
                throw new InvalidOperationException("Directory can't be null or empty.");
            }

            if (!Directory.Exists(assemblyLocation.Directory.FullName))
            {
                throw new InvalidOperationException(
                   string.Format(CultureInfo.CurrentCulture,
                   "Directory not found {0}",
                   assemblyLocation.Directory.FullName));
            }

            AppDomain childDomain = BuildChildDomain(AppDomain.CurrentDomain);

            try
            {
                Type loaderType = typeof(AssemblyLoader);

                if (loaderType.Assembly != null)
                {
                    var loader =
                        (AssemblyLoader)childDomain.
                            CreateInstanceFrom(loaderType.Assembly.Location, loaderType.FullName).Unwrap();

                    loader.LoadAssembly(assemblyLocation.FullName);
                    namespaces = loader.GetNamespaces(assemblyLocation.Directory.FullName);
                }

                return namespaces;
            }
            finally
            {
                AppDomain.Unload(childDomain);
            }
        }
        #endregion

        #region Private Methods

        private AppDomain BuildChildDomain(AppDomain parentDomain)
        {
            Evidence evidence = new Evidence(parentDomain.Evidence);
            AppDomainSetup setup = parentDomain.SetupInformation;
            return AppDomain.CreateDomain("DiscoveryRegion", evidence, setup);
        }
        #endregion

        class AssemblyLoader : MarshalByRefObject
        {
            #region Private/Internal Methods

            internal List<String> GetNamespaces(string path)
            {

                List< String > namespaces = new List<String > ();

                DirectoryInfo directory = new DirectoryInfo(path);

                ResolveEventHandler resolveEventHandler = (s, e) => 
                {
                        return OnReflectionOnlyResolve(e, directory);
                };

                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += resolveEventHandler;

                Assembly reflectionOnlyAssembly = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().First();

                foreach (Type type in reflectionOnlyAssembly.GetTypes())
                {
                    if (!namespaces.Contains(type.Namespace))
                        namespaces.Add(type.Namespace);
                }

                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= resolveEventHandler;
                return namespaces;

            }

            private Assembly OnReflectionOnlyResolve(ResolveEventArgs args, DirectoryInfo directory)
            {

                Assembly loadedAssembly =
                    AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies()
                        .FirstOrDefault(asm => string.Equals(asm.FullName, args.Name, StringComparison.OrdinalIgnoreCase));

                if (loadedAssembly != null)
                {
                    return loadedAssembly;
                }

                AssemblyName assemblyName = new AssemblyName(args.Name);
                string dependentAssemblyFilename = Path.Combine(directory.FullName, assemblyName.Name + ".dll");

                if (File.Exists(dependentAssemblyFilename))
                {
                    return Assembly.ReflectionOnlyLoadFrom(dependentAssemblyFilename);
                }

                return Assembly.ReflectionOnlyLoad(args.Name);
            }

            internal void LoadAssembly(String assemblyPath)
            {
                try
                {
                    Assembly.ReflectionOnlyLoadFrom(assemblyPath);
                }
                catch (FileNotFoundException)
                {
                    /* Continue loading assemblies even if an assembly
                     * can not be loaded in the new AppDomain. */
                }
            }

            #endregion
        }
    }
}
