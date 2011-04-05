using Machine.Specifications;
using Rhino.Mocks;
using solidware.financials.infrastructure;
using solidware.financials.messages;
using solidware.financials.windows.ui;
using solidware.financials.windows.ui.presenters;

namespace specs.unit.ui.presenters
{
    public class AddNewStockSymbolPresenterSpecs
    {
        public abstract class concern
        {
            Establish context = () =>
            {
                builder = Create.dependency<UICommandBuilder>();
                sut = new AddNewStockSymbolPresenter(builder);
            };

            static protected AddNewStockSymbolPresenter sut;
            static protected UICommandBuilder builder;
        }

        public class when_loading_the_dialog : concern
        {
            Establish context = () =>
            {
                add_command = Create.an<ObservableCommand>();
                cancel_command = Create.an<ObservableCommand>();
                builder.is_told_to(x => x.build<AddNewStockSymbolPresenter.AddCommand>(sut)).it_will_return(add_command);
                builder.is_told_to(x => x.build<CancelCommand>(sut)).it_will_return(cancel_command);
            };

            Because of = () =>
            {
                sut.present();
            };

            It should_build_the_add_command = () =>
            {
                sut.Add.should_be_equal_to(add_command);
            };

            It should_build_the_cancel_command = () =>
            {
                sut.Cancel.should_be_equal_to(cancel_command);
            };

            static ObservableCommand add_command;
            static ObservableCommand cancel_command;
        }

        public class when_a_blank_symbol_is_entered : concern
        {
            Because of = () =>
            {
                sut.present();
                result = sut[x => x.Symbol];
            };

            It should_display_an_error = () =>
            {
                result.should_be_equal_to("Please specify a symbol.");
            };

            static string result;
        }

        public class AddCommandSpecs
        {
            public abstract class concern_for_add_command
            {
                Establish context = () =>
                {
                    bus = Create.dependency<ServiceBus>();
                    sut = new AddNewStockSymbolPresenter.AddCommand(bus);
                };

                static protected ServiceBus bus;
                static protected AddNewStockSymbolPresenter.AddCommand sut;
            }

            public class when_adding_a_new_symbol_to_watch : concern_for_add_command
            {
                Establish context = () =>
                {
                    presenter = Create.an<AddNewStockSymbolPresenter>();
                    presenter.is_told_to(x => x.Symbol).it_will_return("TD.TO");
                    presenter.Stub(x => x.close).Return(() =>
                    {
                        closed = true;
                    });
                };

                Because of = () =>
                {
                    sut.run(presenter);
                };

                It should_publish_a_message_to_save_that_symbol = () =>
                {
                    bus.received(x => x.publish(new StartWatchingSymbol {Symbol = "TD.TO"}));
                };

                It should_close_the_dialog = () =>
                {
                    closed.should_be_true();
                };

                static AddNewStockSymbolPresenter presenter;
                static bool closed;
            }
        }
    }
}