namespace desktop.ui
{
    public interface Tab<Presenter> : View<Presenter> where Presenter : TabPresenter {}
}