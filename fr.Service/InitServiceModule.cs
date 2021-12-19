using Autofac;
using fr.Service.Account;
using fr.Service.Generic;
using fr.Service.Interfaces;
using System;

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

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.IsAssignableTo<IGeneratorService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        }
    }
}
