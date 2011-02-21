using System.Threading;
using System.Windows.Threading;
using Autofac;

namespace desktop.ui
{
    public class ConfigureContainerCommand : Command
    {
        public void run()
        {
            var builder = new ContainerBuilder();
            var window = new ShellWindow();

            builder.Register(x => window).As<IRegionManager>().As<ShellWindow>().SingleInstance();
            builder.RegisterType<WPFApplicationController>().As<IApplicationController>().SingleInstance();
            //builder.RegisterAssemblyTypes(GetType().Assembly);
            SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());
            builder.Register(x => SynchronizationContext.Current);

            IOC.BindTo(builder.Build());
        }
    }
}