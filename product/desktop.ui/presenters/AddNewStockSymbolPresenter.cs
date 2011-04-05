using System;
using gorilla.utility;
using solidware.financials.infrastructure;
using solidware.financials.messages;
using solidware.financials.windows.ui.presenters.validation;
using solidware.financials.windows.ui.views.controls;
using utility;

namespace solidware.financials.windows.ui.presenters
{
    public class AddNewStockSymbolPresenter : DialogPresenter
    {
        public AddNewStockSymbolPresenter(UICommandBuilder builder)
        {
            this.builder = builder;
            Symbol = new ObservableProperty<string>();
        }

        public ObservableCommand Add { get; set; }
        public ObservableCommand Cancel { get; set; }
        public virtual Observable<string> Symbol { get; set; }
        public virtual Action close { get; set; }

        public void present()
        {
            Add = builder.build<AddCommand, IsValid>(this);
            Cancel = builder.build<CancelCommand>(this);

            Symbol.Notify(Add);
            Symbol.Register<Error>(x => x.is_blank(), () => "Please specify a symbol.");
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
                bus.publish(new StartWatchingSymbol {Symbol = presenter.Symbol.Value.ToUpperInvariant()});
                presenter.close();
            }
        }

        public class IsValid : UISpecification<AddNewStockSymbolPresenter>
        {
            public override bool is_satisfied_by(AddNewStockSymbolPresenter presenter)
            {
                return presenter.Symbol.not(x => x.Value.is_blank());
            }
        }
    }
}