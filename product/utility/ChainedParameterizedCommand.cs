namespace utility
{
    public class ChainedCommand<T> : Command<T>
    {
        Command<T> left;
        Command<T> right;

        public ChainedCommand(Command<T> left, Command<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public void run_against(T item)
        {
            left.run_against(item);
            right.run_against(item);
        }
    }
}