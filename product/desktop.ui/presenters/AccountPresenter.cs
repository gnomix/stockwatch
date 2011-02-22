namespace desktop.ui.presenters
{
    public class AccountPresenter : TabPresenter
    {
        UICommandBuilder ui_builder;

        public AccountPresenter(UICommandBuilder ui_builder)
        {
            this.ui_builder = ui_builder;
        }

        public void present()
        {
            import = ui_builder.build<ImportTransactionCommand>(this);
        }

        public SelectedAccountDetails SelectedAccount { get; set; }

        public IObservableCommand import { get; set; }

        public string Header
        {
            get { return "Accounts"; }
        }

        public class ImportTransactionCommand : UICommand<AccountPresenter>
        {
            ApplicationController controller;

            public override void run(AccountPresenter presenter)
            {
                //controller.launch_dialog<ImportTransactionsPresenter, ImportTransactionDialog>(presenter.SelectedAccount);
            }
        }
    }

    public class SelectedAccountDetails {}
}