using Autofac;
using GatewayManager.Data;
using GatewayManager.DataModels;
using GatewayManager.Services;

namespace GatewayManager.Web
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GatewayDataService>()
                .As<IDataService<Gateway>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GatewayService>()
                .As<IGatewayService>()
                .InstancePerLifetimeScope();
        }
    }
}