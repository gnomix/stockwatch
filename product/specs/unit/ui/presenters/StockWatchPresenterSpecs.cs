using System;
using System.Linq;
using gorilla.infrastructure.threading;
using Machine.Specifications;
using Rhino.Mocks;
using solidware.financials.infrastructure;
using solidware.financials.messages;
using solidware.financials.windows.ui;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.views.dialogs;

namespace specs.unit.ui.presenters
{
    public class StockWatchPresenterSpecs
    {
        [Subject(typeof (StockWatchPresenter))]
        public abstract class concern
        {
            Establish context = () =>
            {
                builder = Create.dependency<UICommandBuilder>();
                timer = Create.dependency<Timer>();
                sut = new StockWatchPresenter(builder, timer);
            };

            static protected StockWatchPresenter sut;
            static protected UICommandBuilder builder;
            static protected Timer timer;
        }

        public class when_loading_the_region : concern
        {
            Establish context = () =>
            {
                add_command = Create.an<ObservableCommand>();
                builder.Stub(x => x.build<StockWatchPresenter.AddSymbolCommand>(sut)).Return(add_command);
            };

            Because of = () =>
            {
                sut.present();
            };

            It should_start_a_timer_to_periodically_fetch_stock_prices = () =>
            {
                timer.received(x => x.start_notifying(sut, new TimeSpan(0, 1, 0)));
            };

            It should_build_the_add_symbol_command = () =>
            {
                sut.AddSymbol.should_be_equal_to(add_command);
            };

            static ObservableCommand add_command;
        }

        public class when_one_minute_has_elapsed : concern
        {
            Establish context = () =>
            {
                refresh_command = Create.an<ObservableCommand>();
                builder.Stub(x => x.build<StockWatchPresenter.RefreshStockPricesCommand>(sut)).Return(refresh_command);
            };

            Because of = () =>
            {
                sut.present();
                sut.notify();
            };

            It should_refresh_the_stock_prices = () =>
            {
                refresh_command.received(x => x.Execute(sut));
            };

            static ObservableCommand refresh_command;
        }

        public class when_a_stock_price_changes : concern
        {
            Establish context = () =>
            {
                sut.Stocks.Add(new StockViewModel
                               {
                                   Symbol = "ARX.TO",
                                   Price = 20.00m.ToObservable()
                               });
            };

            Because of = () =>
            {
                sut.notify(new CurrentStockPrice {Symbol = "ARX.TO", Price = 25.50m});
            };

            It should_display_the_new_price = () =>
            {
                sut.Stocks.Last().Price.Value.should_be_equal_to(25.50m);
            };
        }

        public class AddSymbolCommandSpecs
        {
            public class when_a_user_wants_to_watch_a_new_symbol
            {
                Establish context = () =>
                {
                    presenter = Create.an<StockWatchPresenter>();
                    controller = Create.dependency<DialogLauncher>();
                    sut = new StockWatchPresenter.AddSymbolCommand(controller);
                };

                Because of = () =>
                {
                    sut.run(presenter);
                };

                It should_launch_the_add_stock_symbol_dialog = () =>
                {
                    controller.received(x => x.launch<AddNewStockSymbolPresenter, AddNewStockSymbolDialog>());
                };

                static DialogLauncher controller;
                static StockWatchPresenter.AddSymbolCommand sut;
                static StockWatchPresenter presenter;
            }
        }

        public class RefreshStockPricesCommandSpecs
        {
            public class concern
            {
                Establish context = () =>
                {
                    bus = Create.dependency<ServiceBus>();

                    sut = new StockWatchPresenter.RefreshStockPricesCommand(bus);
                };

                static protected ServiceBus bus;
                static protected StockWatchPresenter.RefreshStockPricesCommand sut;
            }

            public class when_refreshing_the_stock_prices : concern
            {
                Establish context = () =>
                {
                    presenter = Create.an<StockWatchPresenter>();
                    presenter.is_told_to(x => x.Stocks).it_will_return(new StockViewModel {Symbol = "ARX.TO"});
                };

                Because of = () =>
                {
                    sut.run(presenter);
                };

                It should_fetch_the_stock_price_for_each_symbol = () =>
                {
                    bus.was_told_to(x => x.publish(new StockPriceRequestQuery {Symbol = "ARX.TO"}));
                };

                static StockWatchPresenter presenter;
            }
        }
    }
}