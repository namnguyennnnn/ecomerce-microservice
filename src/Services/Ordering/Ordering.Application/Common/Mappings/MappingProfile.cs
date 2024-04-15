using AutoMapper;
using System.Reflection;

namespace Ordering.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromTypes = typeof(IMapFrom<>);

            const string mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == mapFromTypes;

            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces().Any(HasInterface))
                .ToList();
            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod(mappingMethodName, argumentTypes);
                if (methodInfo != null)
                    methodInfo.Invoke(instance, new object[] { this });
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();
                    if (interfaces.Count == 0)
                        continue;
                    foreach (var mapInterface in interfaces)
                    {
                        var method = mapInterface.GetMethod(mappingMethodName, argumentTypes);
                        if (method != null)
                            method.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
