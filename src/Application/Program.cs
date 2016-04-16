namespace Application
{
    using System;
    using Catalog.Scanning;
    
    // Imports https://github.com/dotnet/cli/issues/2499#issuecomment-210308526
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var assemblyCatalog =
                new DependencyContextAssemblyCatalog();
                
            var assemblies = assemblyCatalog.GetAssemblies();
            
            foreach (var item in assemblies)
            {
                Console.WriteLine($"{item.GetName().Name}");
            }
            
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
