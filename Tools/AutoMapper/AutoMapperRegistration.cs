using System;
using System.Linq;
using System.Reflection;

namespace Toltech.Mvc.Tools.AutoMapper
{
    public static class AutoMapperRegistration
    {
        public static void RegisterAll(IAutoMapperRegistrationResolver resolver)
        {
            Assembly
                .GetCallingAssembly()
                .GetTypes()
                .Where(t => typeof(IAutoMapperRegistrar).IsAssignableFrom(t) && t.IsClass)
                .ToList()
                .ForEach(x => (Activator.CreateInstance(x) as IAutoMapperRegistrar).Register(resolver));
        }
    }
}
