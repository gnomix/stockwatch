using System.Threading;
using utility;

namespace infrastructure.threading
{
    public interface ISynchronizationContext : Command<Command> {}

    public class SynchronizedContext : ISynchronizationContext
    {
        readonly SynchronizationContext context;

        public SynchronizedContext(SynchronizationContext context)
        {
            this.context = context;
        }

        public void run_against(Command item)
        {
            context.Post(x => item.run(), new object());
            //context.Send(x => item.run(), new object());
        }
    }
}