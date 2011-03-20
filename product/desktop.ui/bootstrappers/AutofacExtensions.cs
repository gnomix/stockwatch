using Autofac;
using Autofac.Builder;
using Castle.DynamicProxy;
using solidware.financials.infrastructure;

namespace solidware.financials.windows.ui.bootstrappers
{
    static public class AutofacExtensions
    {
        static readonly IProxyFactory factory = new CastleProxyFactory();

        static public IRegistrationBuilder<Interface, SimpleActivatorData, SingleRegistrationStyle> RegisterProxy
            <Interface, Implementation>(
            this ContainerBuilder builder, params IInterceptor[] interceptors) where Implementation : Interface where Interface : class
        {
            builder.RegisterType<Implementation>();
            return builder.Register(x => factory.CreateProxyFor<Interface>(x.Resolve<Implementation>(), interceptors)).As<Interface>();
        }
    }
}