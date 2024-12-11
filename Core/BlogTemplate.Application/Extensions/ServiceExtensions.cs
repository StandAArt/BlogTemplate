using BlogTemplate.Shared.Constants;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogTemplate.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var typesWithInterfaces = assembly.GetTypes()
                .Where(type => type.IsClass && type.Name.EndsWith(GlobalConsts.Service))
                .Select(type => new
                {
                    Implementation = type,
                    Interface = type.GetInterface($"I{type.Name}")
                })
                .Where(t => t.Interface != null);

            foreach (var type in typesWithInterfaces)
            {
                services.AddScoped(type.Interface, type.Implementation);
            }

            return services;
        }
    }
}
