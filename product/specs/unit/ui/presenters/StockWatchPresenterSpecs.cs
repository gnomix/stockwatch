using System;
using gorilla.infrastructure.threading;
using Machine.Specifications;
using Rhino.Mocks;
using solidware.financials.windows.ui;
using solidware.financials.windows.ui.presenters;

namespace specs.unit.ui.presenters
{
    public class StockWatchPresenterSpecs
    {
        [Subject(typeof(StockWatchPresenter))]
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
    }
}