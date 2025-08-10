using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HeroFinder.ComponentLibrary.ViewModels;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all ViewModels that inherit from ViewModelBase in the specified assemblies.
    /// This method will automatically register all current and future ViewModels.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="assemblies">Assemblies to scan for ViewModels. If null, scans all loaded assemblies.</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddViewModels(this IServiceCollection services, params Assembly[]? assemblies)
    {
        // If no assemblies specified, scan all loaded assemblies
        if (assemblies == null || assemblies.Length == 0)
        {
            assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && a.FullName != null && 
                           (a.FullName.StartsWith("HeroFinder") || 
                            a.GetReferencedAssemblies().Any(ra => ra.Name?.StartsWith("HeroFinder") == true)))
                .ToArray();
        }

        var viewModelBaseType = typeof(ViewModelBase);
        
        foreach (var assembly in assemblies)
        {
            try
            {
                var viewModelTypes = assembly.GetTypes()
                    .Where(type => 
                        type.IsClass && 
                        !type.IsAbstract && 
                        viewModelBaseType.IsAssignableFrom(type) &&
                        type != viewModelBaseType)
                    .ToList();

                foreach (var viewModelType in viewModelTypes)
                {
                    services.AddScoped(viewModelType);
                    Console.WriteLine($"Registered ViewModel: {viewModelType.Name}");
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Handle cases where some types in the assembly can't be loaded
                var loadableTypes = ex.Types.Where(t => t != null).Cast<Type>();
                var viewModelTypes = loadableTypes
                    .Where(type => 
                        type.IsClass && 
                        !type.IsAbstract && 
                        viewModelBaseType.IsAssignableFrom(type) &&
                        type != viewModelBaseType)
                    .ToList();

                foreach (var viewModelType in viewModelTypes)
                {
                    services.AddScoped(viewModelType);
                    Console.WriteLine($"Registered ViewModel: {viewModelType.Name}");
                }
            }
        }

        return services;
    }
}