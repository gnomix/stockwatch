using System;

namespace desktop.ui
{
    public class StubServiceBus : ServiceBus
    {
        public void publish<Message>() where Message : new()
        {
        }

        public void publish<Message>(Message item) where Message : new()
        {
        }

        public void publish<Message>(Action<Message> configure) where Message : new()
        {
        }
    }
}