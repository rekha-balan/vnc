using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// Based on Marius Bancila article
// https://www.codeproject.com/Articles/453778/Loading-Assemblies-from-Anywhere-into-a-New-AppDom

namespace VNC.AssemblyHelper
{


    public class AssemblyReflectionProxy : MarshalByRefObject
    {
        private string _assemblyPath;
        public string _assemblyPathLoadedFromLocation;
        MD5 _md5Hash = MD5.Create();

        public void LoadAssembly(String assemblyPath)
        {
            try
            {
                _assemblyPath = assemblyPath;

                // // ProcExplorer does not display assemblies if loaded Reflection Only
                //Assembly assembly = Assembly.ReflectionOnlyLoadFrom(assemblyPath);
                Assembly assembly = Assembly.LoadFrom(assemblyPath);

                // Hummmm.  When this is run from SupportTools_Excel the assembly.Location is different than assemblyPath???
                // Think this is because of hot swap support.  Save "real" location.

                _assemblyPathLoadedFromLocation = assembly.Location;
            }
            catch (FileNotFoundException)
            {
                // Continue Loading assemblies even if an assembly cannot be loaded in the new AppDomain.
            }
        }

        public List<MethodInformation> AllMethodInformation()
        {
            // Get all Types for current Assembly

            DirectoryInfo directory = new FileInfo(_assemblyPath).Directory;

            ResolveEventHandler resolveEventHandler = (s, e) =>
            {
                return OnReflectionOnlyResolve(e, directory);
            };

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += resolveEventHandler;

            // ProcExplorer does not display assemblies if loaded Reflection Only
            //var assembly = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPathLoadedFromLocation) == 0);
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPathLoadedFromLocation) == 0);

            if (assembly == null) // Don't think this will ever be true, but just in case.
            {
                // ProcExplorer does not display assemblies if loaded Reflection Only
                //assembly = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPath) == 0);
                assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPath) == 0);
            }

            List<MethodInformation> result = null;

            if (assembly != null)
            {
                result = GetMethodInformation(assembly);
            }

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= resolveEventHandler;

            return result;
        }

        public List<ValueTypeInformation> AllStructureInformation()
        {
            // Get all Types for current Assembly

            DirectoryInfo directory = new FileInfo(_assemblyPath).Directory;

            ResolveEventHandler resolveEventHandler = (s, e) =>
            {
                return OnReflectionOnlyResolve(e, directory);
            };

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += resolveEventHandler;

            // ProcExplorer does not display assemblies if loaded Reflection Only
            //var assembly = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPathLoadedFromLocation) == 0);
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPathLoadedFromLocation) == 0);

            if (assembly == null) // Don't think this will ever be true, but just in case.
            {
                // ProcExplorer does not display assemblies if loaded Reflection Only
                //assembly = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPath) == 0);
                assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPath) == 0);
            }

            List<ValueTypeInformation> result = null;

            if (assembly != null)
            {
                 result = GetStructureInformation(assembly); 
            }
            
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= resolveEventHandler;

            return result;
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

            // ProcExplorer does not display assemblies if loaded Reflection Only
            //var assembly = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPathLoadedFromLocation) == 0);
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPathLoadedFromLocation) == 0);

            if (assembly == null) // Don't think this will ever be true, but just in case.
            {
                // ProcExplorer does not display assemblies if loaded Reflection Only
                //assembly = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPath) == 0);
                assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.Location.CompareTo(_assemblyPath) == 0);
            }

            List<TypeInformation> result = null;

            if (assembly != null)
            {
                result = GetTypeInformation(assembly);
            }

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= resolveEventHandler;

            return result;
        }

        public List<MethodInformation> GetMethodInformation(Assembly assembly)
        {
            List<MethodInformation> results = new List<MethodInformation>();

            BindingFlags bindingFlags =
                 BindingFlags.Public
                 | BindingFlags.NonPublic
                 | BindingFlags.DeclaredOnly
                 | BindingFlags.Instance
                 | BindingFlags.Static;

            foreach (TypeInfo typeInfo in assembly.DefinedTypes)
            {
                Type type = assembly.GetType(typeInfo.FullName);

                foreach (MethodInfo methodInfo in type.GetMethods(bindingFlags))
                {
                    try
                    {
                        MethodInformation methodInformation = new MethodInformation();

                        string methodParameters = "";

                        bool hasRetValParameters = false;
                        bool hasOutParameters = false;
                        bool hasOptionalParameters = false;
                        bool hasByRefParameters = false;

                        methodParameters = FormatMethodParameters(methodInfo,
                        ref hasRetValParameters, ref hasOutParameters, ref hasOptionalParameters, ref hasByRefParameters);

                        methodInformation.Assembly = assembly.GetName().Name;

                        methodInformation.Type = typeInfo.FullName;

                        methodInformation.IsStatic = methodInfo.IsStatic.ToString();
                        methodInformation.IsPublic = methodInfo.IsPublic.ToString();
                        methodInformation.IsPrivate = methodInfo.IsPrivate.ToString();

                        methodInformation.ReturnType = methodInfo.ReturnType.ToString();

                        methodInformation.Method = methodInfo.Name;

                        methodInformation.Parameters = methodParameters;

                        methodInformation.RetValParameters = hasRetValParameters ? "X" : "";
                        methodInformation.OutParameters = hasOutParameters ? "X" : "";
                        methodInformation.OptionalParameters = hasOptionalParameters ? "X" : "";
                        methodInformation.ByRefParameters = hasByRefParameters ? "X" : "";

                        MethodBody methodBody;

                        try
                        {
                            methodBody = methodInfo.GetMethodBody();

                            byte[] methodBodyBytes = methodBody.GetILAsByteArray();
                            methodInformation.MD5 = GetMd5Hash(_md5Hash, methodBodyBytes);
                        }
                        catch (Exception ex)
                        {
                            methodInformation.MD5 = "???";
                        }
                        
                        results.Add(methodInformation);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }

            return results;
        }

        string GetMd5Hash(MD5 md5Hash, byte[] bytes)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(bytes);

            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private string FormatMethodParameters(MethodInfo method,
        ref bool hasRetValParameters, ref bool hasOutParameters,
        ref bool hasOptionalParameters, ref bool hasByRefParameters)
    {
        string output = "";
        bool isFirstParameter = true;

        if (method.GetParameters().Count() > 0)
        {
            foreach (ParameterInfo parameter in method.GetParameters())
            {
                output += string.Format("{0}{1} {2}",
                    isFirstParameter ? "" : ", ",
                    parameter.ParameterType, parameter.Name);

                isFirstParameter = false;

                if (parameter.IsRetval) { hasRetValParameters = true; }
                if (parameter.IsOut) { hasOutParameters = true; }
                if (parameter.IsOptional) { hasOptionalParameters = true; }
                if (parameter.ParameterType.IsByRef) { hasByRefParameters = true; }
            }
        }
        else
        {
            output = "()";
        }

        return output;
    }

    public List<ValueTypeInformation> GetStructureInformation(Assembly assembly)
        {
            List<ValueTypeInformation> results = new List<ValueTypeInformation>();

            BindingFlags bindingFlags =
                 BindingFlags.Public
                 | BindingFlags.NonPublic
                 | BindingFlags.DeclaredOnly
                 | BindingFlags.Instance
                 | BindingFlags.Static;

            foreach (TypeInfo typeInfo in assembly.DefinedTypes)
            {
                if (typeInfo.IsValueType && !typeInfo.IsPrimitive)
                {
                    Type structure = typeInfo;

                    ValueTypeInformation valueTypeInformation = new ValueTypeInformation();

                    valueTypeInformation.Assembly = typeInfo.Assembly.GetName().Name;
                    valueTypeInformation.DeclaringType = typeInfo.DeclaringType != null ? typeInfo.DeclaringType.Name : "";
                    valueTypeInformation.Type = typeInfo.Name;

                    foreach (FieldInfo field in structure.GetFields(bindingFlags))
                    {

                        valueTypeInformation.Field = field.Name;
                        valueTypeInformation.FieldType = field.FieldType.FullName;
                        valueTypeInformation.IsArray = field.FieldType.IsArray.ToString();

                        valueTypeInformation.IsEnum = field.FieldType.IsEnum.ToString();
                        valueTypeInformation.IsValueType = field.FieldType.IsValueType.ToString();

                        if (field.GetCustomAttributes().Count() > 0)
                        {
                            foreach (CustomAttributeData customAttribute in field.CustomAttributes)
                            {
                                valueTypeInformation.Attribute = customAttribute.AttributeType.Name;
                                //XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), customAttribute.Constructor.ToString());
                                valueTypeInformation.AttributeValue = customAttribute.ConstructorArguments[0].Value.ToString();
                                //XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), customAttribute.ConstructorArguments.Count.ToString());
                                //XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), customAttribute.NamedArguments.ToString());
                                valueTypeInformation.AttributeToString = customAttribute.ToString();
                            }
                        }
                        else
                        {
                            valueTypeInformation.Attribute = "";
                            valueTypeInformation.AttributeValue = "";
                            valueTypeInformation.AttributeToString = "";
                        }
                    }

                    results.Add(valueTypeInformation);
                }
            }

            return results;
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
