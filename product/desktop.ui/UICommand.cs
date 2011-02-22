namespace desktop.ui
{
    public interface UICommand
    {
        void run<T>(T presenter) where T : Presenter;
    }

    public abstract class UICommand<T> : UICommand where T : class, Presenter
    {
        void UICommand.run<T1>(T1 presenter)
        {
            run(presenter as T);
        }

        public abstract void run(T presenter);
    }
}