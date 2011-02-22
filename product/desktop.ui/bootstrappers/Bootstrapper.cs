using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;
using Autofac;
using desktop.ui.eventing;
using desktop.ui.handlers;
using desktop.ui.presenters;
using desktop.ui.views;
using infrastructure.container;
using infrastructure.threading;
using utility;

namespace desktop.ui.bootstrappers
{
    public static class ClientBootstrapper
    {
        public static ShellWindow create_window()
        {
            var builder = new ContainerBuilder();

            var shell_window = new ShellWindow();
            builder.Register(x => shell_window).SingleInstance();
            builder.Register(x => shell_window).As<RegionManager>().SingleInstance();

            register_needs_startup(builder);

            // infrastructure
            //builder.Register<Log4NetLogFactory>().As<LogFactory>().SingleInstance();
            builder.RegisterType<DefaultMapper>().As<Mapper>().SingleInstance();
            //builder.RegisterGeneric(typeof (Mapper<,>));
            builder.RegisterType<InMemoryServiceBus>().As<ServiceBus>().SingleInstance();

            register_presentation_infrastructure(builder);
            register_presenters(builder);
            register_for_message_to_listen_for(builder);

            shell_window.Closed += (o, e) => Resolve.the<CommandProcessor>().stop();
            shell_window.Closed += (o, e) => Resolve.the<IEnumerable<NeedsShutdown>>();

            Resolve.initialize_with(new AutofacDependencyRegistry(builder));
            Resolve.the<IEnumerable<NeedStartup>>().each(x => x.run());
            Resolve.the<CommandProcessor>().run();
            return shell_window;
        }

        static void register_needs_startup(ContainerBuilder builder)
        {
            builder.RegisterType<ComposeShell>().As<NeedStartup>();
            builder.RegisterType<ConfigureMappings>().As<NeedStartup>();
        }

        static void register_presentation_infrastructure(ContainerBuilder builder)
        {
            SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());
            builder.RegisterType<WpfApplicationController>().As<ApplicationController>().SingleInstance();
            builder.RegisterType<WPFPresenterFactory>().As<PresenterFactory>().SingleInstance();
            builder.RegisterType<SynchronizedEventAggregator>().As<EventAggregator>().SingleInstance();
            //builder.Register(x => AsyncOperationManager.SynchronizationContext);
            builder.Register(x => SynchronizationContext.Current);
            builder.RegisterType<AsynchronousCommandProcessor>().As<CommandProcessor>().SingleInstance();
            //builder.Register<SynchronousCommandProcessor>().As<CommandProcessor>().SingleInstance();
            builder.RegisterType<WPFCommandBuilder>().As<UICommandBuilder>();
        }

        static void register_presenters(ContainerBuilder builder)
        {
            builder.RegisterType<CancelCommand>();

            builder.RegisterType<StatusBarPresenter>().SingleInstance();

            builder.RegisterType<SelectedFamilyMemberPresenter>().SingleInstance();

            builder.RegisterType<AddFamilyMemberPresenter>();
            builder.RegisterType<AddFamilyMemberPresenter.SaveCommand>();

            builder.RegisterType<AccountPresenter>();
            builder.RegisterType<AccountPresenter.ImportTransactionCommand>();

            builder.RegisterType<AddNewDetailAccountPresenter>();
            builder.RegisterType<AddNewDetailAccountPresenter.CreateNewAccount>();

            builder.RegisterType<AddNewIncomeViewModel>();
            builder.RegisterType<AddNewIncomeViewModel.AddIncomeCommand>();

            builder.RegisterType<TaxSummaryPresenter>();
        }

        static void register_for_message_to_listen_for(ContainerBuilder builder)
        {
            builder.RegisterType<PublishEventHandler<AddedNewFamilyMember>>().As<Handles<AddedNewFamilyMember>>();
        }
    }
}