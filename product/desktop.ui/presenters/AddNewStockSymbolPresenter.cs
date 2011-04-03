using System;
using solidware.financials.infrastructure;
using solidware.financials.messages;

namespace solidware.financials.windows.ui.presenters
{
    public class AddNewStockSymbolPresenter : DialogPresenter
    {
        UICommandBuilder builder;

        public AddNewStockSymbolPresenter(UICommandBuilder builder)
        {
            this.builder = builder;
        }

        public ObservableCommand Add { get; set; }
        public ObservableCommand Cancel { get; set; }
        public virtual string Symbol { get; set; }
        public virtual Action close { get; set; }

        public void present()
        {
            Add = builder.build<AddCommand>(this);
            Cancel = builder.build<CancelCommand>(this);
        }

        public class AddCommand : UICommand<AddNewStockSymbolPresenter>
        {
            ServiceBus bus;

            public AddCommand(ServiceBus bus)
            {
                this.bus = bus;
            }

            public override void run(AddNewStockSymbolPresenter presenter)
            {
                bus.publish(new StartWatchingSymbol {Symbol = presenter.Symbol});
                presenter.close();
            }
        }
    }
}