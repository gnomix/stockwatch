namespace desktop.ui
{
    public class CancelCommand : UICommand<DialogPresenter>
    {
        protected override void run(DialogPresenter presenter)
        {
            presenter.close();
        }
    }
}