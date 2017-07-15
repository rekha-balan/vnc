using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VNC.AssemblyHelper
{

    public class AssemblyReflectionProxy : MarshalByRefObject
    {
        private string _assemblyPath;

        public void LoadAssembly(String assemblyPath)
        {
            try
            {
                _assemblyPath = assemblyPath;
                Assembly.ReflectionOnlyLoadFrom(assemblyPath);
            }
            catch (FileNotFoundException)
            {
                // Continue Loading assemblies even if an assembly cannot be loaded in the new AppDomain.
            }
        }

        public TResult Reflect<TResult>(Func<Assembly, TResult> func)
        {
            DirectoryInfo directory = new FileInfo(_assemblyPath).Directory;
            ResolveEventHandler resolveEventHandler = (s, e) =>
            {
                return OnReflectionOnlyResolve(e, directory);
            };

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += resolveEventHandler;

            var assembly = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPath) == 0);

            var result = func(assembly);

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= resolveEventHandler;

            return result;
        }

        private Assembly OnReflectionOnlyResolve(ResolveEventArgs args, DirectoryInfo directory)
        {
            Assembly loadedAssembly =
                AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(asm => string.Equals(asm.FullName, args.Name, StringComparison.OrdinalIgnoreCase));

            if (loadedAssembly != null)
            {
                return loadedAssembly;
            }

            AssemblyName assemblyName = new AssemblyName(args.Name);
            String dependentAssemblyFileName = Path.Combine(directory.FullName, assemblyName.Name + ".dll");

            if (File.Exists(dependentAssemblyFileName))
            {
                return Assembly.ReflectionOnlyLoadFrom(dependentAssemblyFileName);
            }

            return Assembly.ReflectionOnlyLoad(args.Name);
        }
    }
}
