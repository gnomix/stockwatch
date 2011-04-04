using solidware.financials.windows.ui.views;
using solidware.financials.windows.ui.views.controls;

namespace solidware.financials.windows.ui.presenters
{
    public class StockViewModel : Presenter
    {
        UICommandBuilder builder;

        public StockViewModel(string symbol, UICommandBuilder builder)
        {
            this.builder = builder;
            Symbol = symbol;
            Price = Money.Null;
            AdditionalInformation = new SimpleCommand(() => {});
        }

        public virtual string Symbol { get; set; }
        public Observable<Money> Price { get; set; }
        public ObservableCommand AdditionalInformation { get; set; }

        public void present()
        {
            AdditionalInformation = builder.build<MoreCommand>(this);
        }

        public bool is_for(string symbol)
        {
            return Symbol.Equals(symbol);
        }

        public void change_price_to(decimal price)
        {
            Price.Value = price;
        }

        public class MoreCommand : UICommand<StockViewModel>
        {
            readonly ApplicationController controller;
            readonly SingleStockPresenter.Factory factory;

            public MoreCommand(ApplicationController controller, SingleStockPresenter.Factory factory)
            {
                this.controller = controller;
                this.factory = factory;
            }

            public override void run(StockViewModel presenter)
            {
                controller
                    .load_tab<SingleStockPresenter, SingleStockTab>(factory.create_for(presenter.Symbol));
            }
        }
    }
}