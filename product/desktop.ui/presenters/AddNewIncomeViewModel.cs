using System;

namespace desktop.ui.presenters
{
    public class AddNewIncomeViewModel : DialogPresenter
    {
        UICommandBuilder builder;

        public AddNewIncomeViewModel(UICommandBuilder builder)
        {
            this.builder = builder;
        }

        public void present()
        {
            Add = builder.build<AddIncomeCommand>(this);
            Cancel = builder.build<CancelCommand>(this);
        }

        public decimal amount { get; set; }
        public IObservableCommand Add { get; set; }
        public IObservableCommand Cancel { get; set; }
        public Action close { get; set; }

        public class AddIncomeCommand : UICommand<AddNewIncomeViewModel>
        {
            public override void run(AddNewIncomeViewModel presenter)
            {
                Console.Out.WriteLine(presenter.amount);
                presenter.close();
            }
        }
    }
}