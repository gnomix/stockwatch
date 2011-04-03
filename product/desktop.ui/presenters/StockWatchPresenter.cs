using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using gorilla.infrastructure.threading;
using gorilla.utility;
using solidware.financials.infrastructure;
using solidware.financials.messages;
using solidware.financials.windows.ui.views.dialogs;

namespace solidware.financials.windows.ui.presenters
{
    public class StockWatchPresenter : Presenter, TimerClient
    {
        UICommandBuilder builder;
        Timer timer;
        ObservableCommand refresh_command;

        public StockWatchPresenter(UICommandBuilder builder, Timer timer)
        {
            Stocks = new ObservableCollection<StockViewModel>
                     {
                         new StockViewModel {Symbol = "ARX.TO", Price = 25.00m.ToObservable()},
                         new StockViewModel {Symbol = "TD.TO", Price = 85.00m.ToObservable()},
                     };
            this.builder = builder;
            this.timer = timer;
        }

        public virtual IEnumerable<StockViewModel> Stocks { get; set; }
        public ObservableCommand AddSymbol { get; set; }

        public void present()
        {
            timer.start_notifying(this, new TimeSpan(0, 1, 0));
            AddSymbol = builder.build<AddSymbolCommand>(this);
            refresh_command = builder.build<RefreshStockPricesCommand>(this);
        }

        public void notify()
        {
            refresh_command.Execute(this);
        }

        public class AddSymbolCommand : UICommand<StockWatchPresenter>
        {
            DialogLauncher launcher;

            public AddSymbolCommand(DialogLauncher launcher)
            {
                this.launcher = launcher;
            }

            public override void run(StockWatchPresenter presenter)
            {
                launcher.launch<AddNewStockSymbolPresenter, AddNewStockSymbolDialog>();
            }
        }

        public class RefreshStockPricesCommand : UICommand<StockWatchPresenter>
        {
            ServiceBus bus;

            public RefreshStockPricesCommand(ServiceBus bus)
            {
                this.bus = bus;
            }

            public override void run(StockWatchPresenter presenter)
            {
                presenter.Stocks.each(x => bus.publish(new StockPriceRequestQuery {Symbol = x.Symbol}));
            }
        }
    }
}