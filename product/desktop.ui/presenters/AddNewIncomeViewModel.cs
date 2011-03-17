using System;
using desktop.ui.messages.@private;

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
            Add = builder.build<AddIncomeCommand, IfFamilyMemberIsSelected>(this);
            Cancel = builder.build<CancelCommand>(this);
        }

        public decimal amount { get; set; }
        public IObservableCommand Add { get; set; }
        public IObservableCommand Cancel { get; set; }
        public Action close { get; set; }

        public class AddIncomeCommand : UICommand<AddNewIncomeViewModel>
        {
            ServiceBus bus;

            public AddIncomeCommand(ServiceBus bus)
            {
                this.bus = bus;
            }

            public override void run(AddNewIncomeViewModel presenter)
            {
                bus.publish<AddIncomeCommandMessage>(x => { x.amount = presenter.amount; });
                presenter.close();
            }
        }
    }
}