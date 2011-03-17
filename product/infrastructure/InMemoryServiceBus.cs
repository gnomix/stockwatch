using System;
using gorilla.infrastructure.container;
using gorilla.utility;

namespace solidware.financials.infrastructure
{
    public class InMemoryServiceBus : ServiceBus
    {
        DependencyRegistry registry;

        public InMemoryServiceBus(DependencyRegistry registry)
        {
            this.registry = registry;
        }

        public void publish<Message>() where Message : new()
        {
            publish(new Message());
        }

        public void publish<Message>(Message item) where Message : new()
        {
            registry.get_all<Handles<Message>>().each(x => x.handle(item));
        }

        public void publish<Message>(Action<Message> configure) where Message : new()
        {
            var message = new Message();
            configure(message);
            publish(message);
        }
    }
}