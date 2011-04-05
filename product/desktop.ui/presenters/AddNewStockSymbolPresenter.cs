using System;
using System.Linq.Expressions;
using gorilla.utility;
using solidware.financials.infrastructure;
using solidware.financials.messages;
using solidware.financials.windows.ui.presenters.validation;

namespace solidware.financials.windows.ui.presenters
{
    public class AddNewStockSymbolPresenter : DialogPresenter, INotification<AddNewStockSymbolPresenter>
    {
        public AddNewStockSymbolPresenter(UICommandBuilder builder)
        {
            this.builder = builder;
            Notification = new Notification<AddNewStockSymbolPresenter>();
        }

        public ObservableCommand Add { get; set; }
        public ObservableCommand Cancel { get; set; }
        public virtual string Symbol { get; set; }
        public virtual Action close { get; set; }
        public Notification<AddNewStockSymbolPresenter> Notification { get; set; }

        public void present()
        {
            Add = builder.build<AddCommand>(this);
            Cancel = builder.build<CancelCommand>(this);

            Notification.Register<Error>(x => x.Symbol, () => Symbol.is_blank(), () => "Please specify a symbol.");
        }

        public string this[string property]
        {
            get { return Notification[property]; }
        }

        public string this[Expression<Func<AddNewStockSymbolPresenter, object>> property]
        {
            get { return Notification[property]; }
        }

        public string Error
        {
            get { return Notification.Error; }
        }

        UICommandBuilder builder;

        public class AddCommand : UICommand<AddNewStockSymbolPresenter>
        {
            ServiceBus bus;

            public AddCommand(ServiceBus bus)
            {
                this.bus = bus;
            }

            public override void run(AddNewStockSymbolPresenter presenter)
            {
                bus.publish(new StartWatchingSymbol {Symbol = presenter.Symbol.ToUpperInvariant()});
                presenter.close();
            }
        }
    }
}