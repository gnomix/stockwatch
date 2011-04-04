using System;
using gorilla.utility;
using Machine.Specifications;
using solidware.financials.messages;
using solidware.financials.windows.ui.presenters;

namespace specs.unit.ui.presenters
{
    public class SingleStockPresenterSpecs
    {
        [Subject(typeof (SingleStockPresenter))]
        public abstract class concern
        {
            Establish context = () =>
            {
                Clock.change_time_provider_to(() => now);
                sut = new SingleStockPresenter("ARX.TO");
            };

            Cleanup clean = () =>
            {
                Clock.reset();
            };

            static protected SingleStockPresenter sut;
            static protected DateTime now = new DateTime(2011, 01, 01);
        }

        public class when_a_new_price_is_received_for_the_stock_this_chart_is_for : concern
        {
            Establish context = () =>
            {
                current_price = new CurrentStockPrice
                                {
                                    Symbol = "ARX.TO",
                                    Price = 29.00m
                                };
            };

            Because of = () =>
            {
                sut.present();
                sut.notify(current_price);
            };

            It should_update_the_chart_with_the_new_data_point = () =>
            {
                sut.Chart.should_contain(x => x.Key.Equals(now) && x.Value.Equals(29.00m));
            };

            static CurrentStockPrice current_price;
        }

        public class when_a_new_price_is_received_for_another_stock : concern
        {
            Establish context = () =>
            {
                current_price = new CurrentStockPrice
                                {
                                    Symbol = "TD.TO",
                                    Price = 90.00m
                                };
            };

            Because of = () =>
            {
                sut.present();
                sut.notify(current_price);
            };

            It should_not_update_the_chart = () =>
            {
                sut.Chart.Count.should_be_equal_to(0);
            };

            static CurrentStockPrice current_price;
        }

        public class when_displaying_the_header_for_this_tab : concern
        {
            It should_describe_the_stock_that_it_is_for = () =>
            {
                sut.Header.should_be_equal_to("ARX.TO");
            };
        }
    }
}