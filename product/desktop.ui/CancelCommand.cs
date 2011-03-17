namespace solidware.financials.windows.ui
{
    public class CancelCommand : UICommand<DialogPresenter>
    {
        public override void run(DialogPresenter presenter)
        {
            presenter.close();
        }
    }
}