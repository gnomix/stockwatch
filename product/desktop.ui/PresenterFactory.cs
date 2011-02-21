namespace desktop.ui
{
    public interface PresenterFactory
    {
        T create<T>() where T : Presenter;
    }
}