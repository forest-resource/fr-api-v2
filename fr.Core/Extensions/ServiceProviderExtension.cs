using fr.Core.Exceptions;
using System;

namespace fr.Core.Extensions
{
    public static class ServiceProviderExtension
    {
        public static T GetService<T>(this IServiceProvider sp)
        {
            var serviceType = typeof(T);
            return (T)GetService(sp, serviceType);
        }

        public static object GetService(this IServiceProvider sp, Type type)
        {
            var service = sp.GetService(type);
            if (service == null)
                throw new AppException($"{type.Name} not found");
            return service;
        }
    }
}
