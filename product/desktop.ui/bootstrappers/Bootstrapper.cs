using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using gorilla.infrastructure.container;
using gorilla.infrastructure.threading;
using gorilla.utility;
using solidware.financials.infrastructure;
using solidware.financials.infrastructure.eventing;
using solidware.financials.messages;
using solidware.financials.service;
using solidware.financials.service.handlers;
using solidware.financials.service.orm;
using solidware.financials.windows.ui.handlers;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.presenters.specifications;
using solidware.financials.windows.ui.views;
using utility;
using Timer = gorilla.infrastructure.threading.Timer;

namespace solidware.financials.windows.ui.bootstrappers
{
    static public class Bootstrapper
    {
        static public Window create_window()
        {
            var builder = new ContainerBuilder();

            //var shell_window = new ShellWindow();
            var shell_window = new Shell();
            builder.Register(x => shell_window).SingleInstance();
            builder.Register(x => shell_window).As<RegionManager>().SingleInstance();

            register_needs_startup(builder);

            // infrastructure
            //builder.Register<Log4NetLogFactory>().As<LogFactory>().SingleInstance();
            builder.RegisterType<DefaultMapper>().As<Mapper>().SingleInstance();
            //builder.RegisterGeneric(typeof (Mapper<,>));
            builder.RegisterType<InMemoryServiceBus>().As<ServiceBus>().SingleInstance();
            builder.RegisterGeneric(typeof (IfFamilyMemberIsSelected<>));

            register_presentation_infrastructure(builder);
            register_presenters(builder);
            register_for_message_to_listen_for(builder);
            server_registration(builder);

            builder.RegisterType<StopEssentialServices>().As<NeedsShutdown>();

            shell_window.Closed += (o, e) =>
            {
                Resolve.the<IEnumerable<NeedsShutdown>>().each(x => x.run());
            };

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
            builder.RegisterType<WPFApplicationController>().As<ApplicationController>().SingleInstance();
            builder.RegisterType<WPFDialogLauncher>().As<DialogLauncher>().SingleInstance();
            builder.RegisterType<WPFPresenterFactory>().As<PresenterFactory>().SingleInstance();
            builder.RegisterType<SynchronizedEventAggregator>().As<EventAggregator>().SingleInstance();
            //builder.Register(x => AsyncOperationManager.SynchronizationContext);
            builder.Register(x => SynchronizationContext.Current);
            builder.RegisterType<AsynchronousCommandProcessor>().As<CommandProcessor>().SingleInstance();
            //builder.Register<SynchronousCommandProcessor>().As<CommandProcessor>().SingleInstance();
            builder.RegisterType<WPFCommandBuilder>().As<UICommandBuilder>();
            builder.RegisterType<InMemoryApplicationState>().As<ApplicationState>().SingleInstance();

            builder.RegisterType<IntervalTimer>().As<Timer>().SingleInstance();
        }

        static void register_presenters(ContainerBuilder builder)
        {
            builder.RegisterType<CancelCommand>();

            builder.RegisterType<StatusBarPresenter>().SingleInstance();

            builder.RegisterType<ButtonBarPresenter>().SingleInstance();

            builder.RegisterType<AddFamilyMemberPresenter>();
            builder.RegisterType<AddFamilyMemberPresenter.SaveCommand>();

            builder.RegisterType<AddNewIncomeViewModel>();
            builder.RegisterType<AddNewIncomeViewModel.AddIncomeCommand>();
            builder.RegisterType<IfFamilyMemberIsSelected<AddNewIncomeViewModel>>();

            builder.RegisterType<TaxSummaryPresenter>();

            builder.RegisterType<DisplayCanadianTaxInformationViewModel>();

            builder.RegisterType<StockWatchPresenter>();
            builder.RegisterType<StockWatchPresenter.AddSymbolCommand>();
            builder.RegisterType<StockWatchPresenter.RefreshStockPricesCommand>();

            builder.RegisterType<AddNewStockSymbolPresenter>();
            builder.RegisterType<AddNewStockSymbolPresenter.AddCommand>();
        }

        static void register_for_message_to_listen_for(ContainerBuilder builder)
        {
            builder.RegisterType<PublishEventHandler<AddedNewFamilyMember>>().As<Handles<AddedNewFamilyMember>>();
            builder.RegisterType<PublishEventHandler<IncomeMessage>>().As<Handles<IncomeMessage>>();
            builder.RegisterType<PublishEventHandler<CurrentStockPrice>>().As<Handles<CurrentStockPrice>>();
            builder.RegisterType<PublishEventHandler<StartWatchingSymbol>>().As<Handles<StartWatchingSymbol>>();
        }

        static void server_registration(ContainerBuilder builder)
        {
            //builder.RegisterType<InMemoryDatabase>().As<PersonRepository>().SingleInstance();
            builder.RegisterType<DB4OPersonRepository>().As<PersonRepository>().SingleInstance();
            builder.RegisterType<ScopedContext>().As<Context>().SingleInstance();
            //builder.Register(x => new SimpleContext(new Hashtable())).As<Context>().SingleInstance();
            builder.RegisterType<PerThreadScopedStorage>().As<ScopedStorage>();
            builder.RegisterType<CurrentThread>().As<ApplicationThread>();
            builder.Register(x =>
            {
                return Lazy.load(() =>
                {
                    var context = Resolve.the<Context>();
                    var key = new TypedKey<Connection>();
                    return context.value_for(key);
                });
            });

            var unit_of_work_interceptor = new UnitOfWorkInterceptor(new DB40UnitOfWorkFactory(new DB4OConnectionFactory(), Lazy.load<Context>()));
            builder.RegisterProxy<Handles<FamilyMemberToAdd>, AddNewFamilyMemberHandler>(unit_of_work_interceptor);
            builder.RegisterProxy<Handles<FindAllFamily>, FindAllFamilyHandler>(unit_of_work_interceptor);
            builder.RegisterProxy<Handles<FindAllIncome>, FindAllIncomeHandler>(unit_of_work_interceptor);
            builder.RegisterProxy<Handles<AddIncomeCommandMessage>, AddIncomeCommandMessageHandler>(unit_of_work_interceptor);

            var run_in_background = new RunInBackgroundInterceptor(Lazy.load<CommandProcessor>());
            builder.RegisterProxy<Handles<StockPriceRequestQuery>, StockPriceRequestQueryHandler>(run_in_background);

            builder.RegisterType<ConfigureServiceMappings>().As<NeedStartup>();

            //builder.RegisterType<StubLookupService>().As<StockPriceLookupService>();
            builder.RegisterType<GoogleLookupService>().As<StockPriceLookupService>();

            new DB4OBootstrapper().run();
        }
    }
}