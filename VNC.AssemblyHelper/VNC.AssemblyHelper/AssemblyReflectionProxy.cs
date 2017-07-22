using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// Based on Marius Bancila article
// https://www.codeproject.com/Articles/453778/Loading-Assemblies-from-Anywhere-into-a-New-AppDom

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

        public List<TypeInformation> GetTypeInformation(Assembly assembly)
        {
            List<TypeInformation> results = new List<TypeInformation>();

            foreach (TypeInfo typeInfo in assembly.DefinedTypes)
            {
                {
                    TypeInformation typeInformation = new TypeInformation();

                    //insertAt.ClearOffsets();

                    //if (sourceName != "")
                    //{
                    //    // Only the Master sheets display source and application
                    //    sourceName);
                    //    applicationName);
                    //}

                    typeInformation.Assembly = typeInfo.Assembly.GetName().Name;

                    Type type = typeInfo;
                    typeInformation.FullName = type.FullName;
                    typeInformation.DeclaringType = typeInfo.DeclaringType != null ? typeInfo.DeclaringType.Name : "";
                    typeInformation.Name = typeInfo.Name;

                    typeInformation.IsPublic = typeInfo.IsPublic.ToString();
                    typeInformation.IsNotPublic = typeInfo.IsNotPublic.ToString();

                    typeInformation.IsValueType = typeInfo.IsValueType.ToString();

                    typeInformation.IsPrimitive = typeInfo.IsPrimitive.ToString();
                    typeInformation.IsEnum = typeInfo.IsEnum.ToString();
                    typeInformation.IsInterface = typeInfo.IsInterface.ToString();

                    typeInformation.IsClass = typeInfo.IsClass.ToString();
                    typeInformation.IsAbstract = typeInfo.IsAbstract.ToString();

                    typeInformation.IsSealed = typeInfo.IsSealed.ToString();
                    typeInformation.IsNested = typeInfo.IsNested.ToString();
                    typeInformation.IsNestedPublic = typeInfo.IsNestedPublic.ToString();
                    typeInformation.IsNestedPrivate = typeInfo.IsNestedPrivate.ToString();

                    typeInformation.HasElementType = typeInfo.HasElementType.ToString();
                    typeInformation.IsArray = typeInfo.IsArray.ToString();
                    typeInformation.IsByRef = typeInfo.IsByRef.ToString();
                    typeInformation.IsPointer = typeInfo.IsPointer.ToString();

                    results.Add(typeInformation);

                }
            }

            return results;
        }

        public List<TypeInformation> AllTypeInformation()
        {

            // Get all Types for current Assembly

            DirectoryInfo directory = new FileInfo(_assemblyPath).Directory;

            ResolveEventHandler resolveEventHandler = (s, e) =>
            {
                return OnReflectionOnlyResolve(e, directory);
            };

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += resolveEventHandler;

            var assembly = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPath) == 0);

            List<TypeInformation> result = GetTypeInformation(assembly);


            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= resolveEventHandler;

            return result;
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
