namespace solidware.financials.windows.ui
{
    public interface PresenterFactory
    {
        T create<T>() where T : Presenter;
    }
}