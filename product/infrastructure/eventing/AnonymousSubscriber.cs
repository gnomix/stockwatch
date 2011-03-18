using System;

namespace solidware.financials.infrastructure.eventing
{
    public class AnonymousSubscriber<T> : EventSubscriber<T> where T : Event
    {
        Action<T> action;

        public AnonymousSubscriber(Action<T> action)
        {
            this.action = action;
        }

        public void notify(T message)
        {
            action(message);
        }
    }
}