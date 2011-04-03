using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using gorilla.infrastructure.threading;

namespace solidware.financials.windows.ui.presenters
{
    public class StockWatchPresenter : Presenter, TimerClient
    {
        UICommandBuilder builder;
        readonly Timer timer;
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

        public IEnumerable<StockViewModel> Stocks { get; set; }
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
            ApplicationController controller;

            public AddSymbolCommand(ApplicationController controller)
            {
                this.controller = controller;
            }

            public override void run(StockWatchPresenter presenter) {}
        }

        public class RefreshStockPricesCommand : UICommand<StockWatchPresenter>
        {
            public override void run(StockWatchPresenter presenter)
            {
                throw new NotImplementedException();
            }
        }
    }
}