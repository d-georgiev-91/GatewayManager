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
            builder.RegisterType<PeripheralDeviceDataService>()
                .As<IDataService<PeripheralDevice>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PeripheralDeviceService>()
                .As<IPeripheralDeviceService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GatewayDataService>()
                .As<IDataService<Gateway>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GatewayService>()
                .As<IGatewayService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GatewayDeviceManager>()
                .As<IGatewayDeviceManager>()
                .InstancePerLifetimeScope();
        }
    }
}