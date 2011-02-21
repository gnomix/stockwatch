namespace desktop.ui
{
    public interface UICommandBuilder
    {
        IObservableCommand build<T>(Presenter presenter) where T : UICommand;
    }
}