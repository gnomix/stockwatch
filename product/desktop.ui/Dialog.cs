namespace solidware.financials.windows.ui
{
    public interface Dialog<Presenter> : View<Presenter> where Presenter : DialogPresenter
    {
        void open(Presenter presenter);
        void Close();
    }
}