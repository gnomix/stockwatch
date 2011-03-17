namespace solidware.financials.windows.ui
{
    public interface Dialog<TPresenter> : View<TPresenter> where TPresenter : DialogPresenter
    {
        void open();
        void Close();
    }
}