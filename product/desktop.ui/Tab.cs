namespace solidware.financials.windows.ui
{
    public interface Tab<Presenter> : View<Presenter> where Presenter : TabPresenter
    {
        void bind_to(Presenter presenter);
    }
}