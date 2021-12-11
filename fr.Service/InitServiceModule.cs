using Autofac;
using fr.Service.Generic;
using fr.Service.Interfaces;

namespace fr.Service
{
    public class InitServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterGeneric(typeof(GenericService<,>))
                .As(typeof(IGenericService<,>))
                .InstancePerLifetimeScope();
        }

        private static void RegisterType<Type>(ContainerBuilder builder)
            => builder.RegisterType<Type>().AsSelf().InstancePerLifetimeScope();
    }
}
