using Machine.Specifications;
using solidware.financials.windows.ui;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.views;

namespace specs.unit.ui.presenters
{
    public class StockViewModelSpecs
    {
        public abstract class concern
        {
            Establish context = () =>
            {
                builder = Create.dependency<UICommandBuilder>();
                sut = new StockViewModel("ARX.TO", builder);
            };

            static protected StockViewModel sut;
            static protected UICommandBuilder builder;
        }

        public class when_someone_clicks_on_the_additional_info_button : concern
        {
            Establish context = () =>
            {
                more_command = Create.an<ObservableCommand>();
                builder.is_told_to(x => x.build<StockViewModel.MoreCommand>(sut)).it_will_return(more_command);
            };

            Because of = () =>
            {
                sut.present();
            };

            It should_execute_the_additional_info_command = () =>
            {
                sut.AdditionalInformation.should_be_equal_to(more_command);
            };

            static ObservableCommand more_command;
        }

        public class MoreCommandSpecs
        {
            public abstract class concern_for_more_command
            {
                Establish context = () =>
                {
                    controller = Create.dependency<ApplicationController>();
                    factory = Create.dependency<SingleStockPresenter.Factory>();
                    sut = new StockViewModel.MoreCommand(controller, factory);
                };

                static protected StockViewModel.MoreCommand sut;
                static protected ApplicationController controller;
                static protected SingleStockPresenter.Factory factory;
            }

            public class when_wanting_to_see_more_info_on_a_stock : concern_for_more_command
            {
                Establish context = () =>
                {
                    presenter = Create.an<StockViewModel>();
                    tab = Create.an<SingleStockPresenter>();

                    presenter.is_told_to(x => x.Symbol).it_will_return("ARX.TO");
                    factory.is_told_to(x => x.create_for("ARX.TO")).it_will_return(tab);
                };

                Because of = () =>
                {
                    sut.run(presenter);
                };

                It should_display_a_tab_for_the_stock = () =>
                {
                    controller.received(x => x.load_tab<SingleStockPresenter, SingleStockTab>(tab));
                };

                static StockViewModel presenter;
                static SingleStockPresenter tab;
            }
        }
    }
}