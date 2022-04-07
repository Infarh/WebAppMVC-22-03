using Autofac;
using WebAppMVC.Services;
using WebAppMVC.Services.Interfaces;

namespace WebAppMVC.Infrastructure.Autofac;

public class ServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<SqlOrderService>()
           .As<IOrderService>()
           .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
           .Where(type => type.Namespace!.EndsWith("Services"))
           .AsImplementedInterfaces();
    }
}
