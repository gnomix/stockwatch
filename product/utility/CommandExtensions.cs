using System;

namespace utility
{
    static public class CommandExtensions
    {
        static public utility.Command then<Command>(this utility.Command left) where Command : utility.Command, new()
        {
            return then(left, new Command());
        }

        static public Command then(this Command left, Command right)
        {
            return new ChainedCommand(left, right);
        }

        static public Command then(this Command left, Action right)
        {
            return new ChainedCommand(left, new AnonymousCommand(right));
        }

        static public Command<T> then<T>(this Command<T> left, Command<T> right)
        {
            return new ChainedCommand<T>(left, right);
        }
    }
}