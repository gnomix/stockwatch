using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;
using Autofac;
using gorilla.infrastructure.container;
using gorilla.infrastructure.threading;
using gorilla.utility;
using solidware.financials.infrastructure;
using solidware.financials.infrastructure.eventing;
using solidware.financials.messages;
using solidware.financials.service.handlers;
using solidware.financials.service.orm;
using solidware.financials.windows.ui.handlers;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.views;

namespace solidware.financials.windows.ui.bootstrappers
{
    public static class Bootstrapper
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
            server_registration(builder);

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
            builder.RegisterType<WireUpSubscribers>().As<NeedStartup>();
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


            builder.RegisterType<AddNewIncomeViewModel>();
            builder.RegisterType<AddNewIncomeViewModel.AddIncomeCommand>();
            builder.RegisterType<IfFamilyMemberIsSelected>().SingleInstance();

            builder.RegisterType<TaxSummaryPresenter>();
            
            builder.RegisterType<DisplayCanadianTaxInformationViewModel>();
        }

        static void register_for_message_to_listen_for(ContainerBuilder builder)
        {
            builder.RegisterType<PublishEventHandler<AddedNewFamilyMember>>().As<Handles<AddedNewFamilyMember>>();
            builder.RegisterType<PublishEventHandler<AddIncomeCommandMessage>>().As<Handles<AddIncomeCommandMessage>>();
            
        }

        static void server_registration(ContainerBuilder builder)
        {
            builder.RegisterType<AddNewFamilyMemberHandler>().As<Handles<FamilyMemberToAdd>>();
            builder.RegisterType<FindAllFamilyHandler>().As<Handles<FindAllFamily>>();
            builder.RegisterType<InMemoryDatabase>().As<PersonRepository>().SingleInstance();
        }
    }
}