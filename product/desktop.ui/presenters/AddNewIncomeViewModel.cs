using System;
using solidware.financials.infrastructure;
using solidware.financials.messages;
using solidware.financials.windows.ui.events;
using solidware.financials.windows.ui.presenters.specifications;

namespace solidware.financials.windows.ui.presenters
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
            Add = builder.build<AddIncomeCommand, IfFamilyMemberIsSelected<AddNewIncomeViewModel>>(this);
            Cancel = builder.build<CancelCommand>(this);
        }

        public virtual decimal amount { get; set; }
        public IObservableCommand Add { get; set; }
        public IObservableCommand Cancel { get; set; }
        public virtual Action close { get; set; }

        public class AddIncomeCommand : UICommand<AddNewIncomeViewModel>
        {
            ServiceBus bus;
            ApplicationState applicationState;

            public AddIncomeCommand(ServiceBus bus, ApplicationState applicationState)
            {
                this.bus = bus;
                this.applicationState = applicationState;
            }

            public override void run(AddNewIncomeViewModel presenter)
            {
                bus.publish(new AddIncomeCommandMessage
                            {
                                Amount = presenter.amount,
                                PersonId = applicationState.PullOut<SelectedFamilyMember>().id
                            });
                presenter.close();
            }
        }
    }
}