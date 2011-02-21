using System;

namespace desktop.ui
{
    public interface Command
    {
        void run();
    }

    public interface Command<T>
    {
        void run(T item);
    }

    public static class CommandExtensions
    {
        public static ui.Command then<Command>(this Command left) where Command : ui.Command, new()
        {
            return then(left, new Command());
        }

        public static Command then(this Command left, Command right)
        {
            return new ChainedCommand(left, right);
        }

        public static Command then(this Command left, Action right)
        {
            return new ChainedCommand(left, new AnonymousCommand(right));
        }

        public static Command<T> then<T>(this Command<T> left, Command<T> right)
        {
            return new ChainedCommand<T>(left, right);
        }
    }

    public class ChainedCommand : Command
    {
        readonly Command left;
        readonly Command right;

        public ChainedCommand(Command left, Command right)
        {
            this.left = left;
            this.right = right;
        }

        public void run()
        {
            left.run();
            right.run();
        }
    }

    public class AnonymousCommand : Command
    {
        readonly Action action;

        public AnonymousCommand(Action action)
        {
            this.action = action;
        }

        public void run()
        {
            action();
        }
    }

    public class ChainedCommand<T> : Command<T>
    {
        Command<T> left;
        Command<T> right;

        public ChainedCommand(Command<T> left, Command<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public void run(T item)
        {
            left.run(item);
            right.run(item);
        }
    }
}