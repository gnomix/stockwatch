using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using gorilla.utility;
using solidware.financials.infrastructure.eventing;
using solidware.financials.messages;

namespace solidware.financials.windows.ui.presenters
{
    public class SingleStockPresenter : TabPresenter, EventSubscriber<CurrentStockPrice>
    {
        string symbol_to_watch;

        public SingleStockPresenter(string symbol_to_watch)
        {
            this.symbol_to_watch = symbol_to_watch;
            Chart = new ObservableCollection<KeyValuePair<DateTime, decimal>>();
        }

        public ICollection<KeyValuePair<DateTime, decimal>> Chart { get; set; }

        public string Header
        {
            get { return symbol_to_watch; }
        }

        public void present() {}

        public void notify(CurrentStockPrice message)
        {
            if (symbol_to_watch.Equals(message.Symbol))
                Chart.Add(new KeyValuePair<DateTime, decimal>(Clock.now(), message.Price));
        }

        public class Factory
        {
            public virtual SingleStockPresenter create_for(string symbol)
            {
                throw new NotImplementedException();
            }
        }
    }
}