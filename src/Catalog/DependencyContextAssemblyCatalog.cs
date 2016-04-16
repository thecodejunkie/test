namespace Catalog.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyModel;
    
    public class DependencyContextAssemblyCatalog
    {
        private static readonly Assembly NancyAssembly = typeof(DependencyContextAssemblyCatalog).GetTypeInfo().Assembly;
        private readonly DependencyContext dependencyContext;

        public DependencyContextAssemblyCatalog()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            
            this.dependencyContext = DependencyContext.Load(entryAssembly);
        }

        public IReadOnlyCollection<Assembly> GetAssemblies()
        {
            var results = new HashSet<Assembly>
            {
                typeof (DependencyContextAssemblyCatalog).GetTypeInfo().Assembly
            };

            foreach (var library in this.dependencyContext.RuntimeLibraries)
            {
                if (IsReferencingNancy(library))
                {
                    foreach (var assembly in library.Assemblies)
                    {
                        results.Add(Assembly.Load(assembly.Name));
                    }
                }
            }
            
            return results.ToArray();
        }
        
        private static Assembly SafeLoadAssembly(AssemblyName assemblyName)
        {
            try
            {
                return Assembly.Load(assemblyName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static bool IsReferencingNancy(Library library)
        {
            return library.Dependencies.Any(dependency => dependency.Name.Equals(NancyAssembly.GetName().Name));
        }
    }
}