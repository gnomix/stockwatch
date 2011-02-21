using System;
using System.Threading;
using utility;

namespace infrastructure.threading
{
    public interface ISynchronizedCommand : Command<Action>, Command<Command> {}

    public class SynchronizedCommand : ISynchronizedCommand
    {
        readonly SynchronizationContext context;

        public SynchronizedCommand(SynchronizationContext context)
        {
            this.context = context;
        }

        public void run_against(Action item)
        {
            context.Post(x => item(), new object());
        }

        public void run_against(Command item)
        {
            run_against(item.run);
        }
    }
}