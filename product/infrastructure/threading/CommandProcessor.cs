using System;
using utility;

namespace infrastructure.threading
{
    public interface CommandProcessor : Command
    {
        void add(Action command);
        void add(Command command_to_process);
        void stop();
    }
}