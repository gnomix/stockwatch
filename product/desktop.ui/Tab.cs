namespace solidware.financials.windows.ui
{
    public interface Tab<Presenter> : View<Presenter> where Presenter : TabPresenter {}
}