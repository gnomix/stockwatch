namespace desktop.ui
{
    public class CancelCommand : UICommand<DialogPresenter>
    {
        public override void run(DialogPresenter presenter)
        {
            presenter.close();
        }
    }
}