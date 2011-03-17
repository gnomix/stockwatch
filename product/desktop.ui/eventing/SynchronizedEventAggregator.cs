using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using gorilla.utility;

namespace desktop.ui.eventing
{
    public class SynchronizedEventAggregator : EventAggregator
    {
        readonly SynchronizationContext context;
        readonly HashSet<object> subscribers = new HashSet<object>();
        readonly object mutex = new object();

        public SynchronizedEventAggregator(SynchronizationContext context)
        {
            this.context = context;
        }

        public void subscribe_to<Event>(EventSubscriber<Event> subscriber) where Event : eventing.Event
        {
            subscribe(subscriber);
        }

        public void subscribe<Listener>(Listener subscriber) 
        {
            within_lock(() => subscribers.Add(subscriber));
        }

        public void publish<Event>(Event the_event_to_broadcast) where Event : eventing.Event
        {
            var current_subscribers = subscribers.ToList();
            process(() => current_subscribers.call_on_each<EventSubscriber<Event>>(x => x.notify(the_event_to_broadcast)));
        }

        public void publish<T>(Action<T> call) where T : class
        {
            var current_subscribers = subscribers.ToList();
            process(() => current_subscribers.each(x => x.call_on(call)));
        }

        public void publish<Event>() where Event : eventing.Event, new()
        {
            publish(new Event());
        }

        void within_lock(Action action)
        {
            lock (mutex) action();
        }

        void process(Action action)
        {
            context.Send(x => action(), new object());
        }
    }
}