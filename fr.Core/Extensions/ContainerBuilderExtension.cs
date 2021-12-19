using Autofac;

namespace fr.Core.Extensions
{
    public static class ContainerBuilderExtension
    {
        public static void RegisterType<TInterface, TInstance>(this ContainerBuilder builder)
            where TInstance : class, TInterface
            => builder.RegisterType<TInstance>().As<TInterface>().InstancePerLifetimeScope();
    }
}
