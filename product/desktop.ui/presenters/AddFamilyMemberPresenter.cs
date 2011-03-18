using System;
using gorilla.utility;
using solidware.financials.infrastructure;
using solidware.financials.messages;

namespace solidware.financials.windows.ui.presenters
{
    public class AddFamilyMemberPresenter : DialogPresenter
    {
        UICommandBuilder ui_builder;

        public AddFamilyMemberPresenter(UICommandBuilder ui_builder)
        {
            this.ui_builder = ui_builder;
        }

        public void present()
        {
            Save = ui_builder.build<SaveCommand>(this);
            Cancel = ui_builder.build<CancelCommand>(this);
            date_of_birth = Clock.today();
        }

        public virtual string first_name { get; set; }
        public virtual string last_name { get; set; }
        public virtual DateTime date_of_birth { get; set; }
        public IObservableCommand Save { get; set; }
        public IObservableCommand Cancel { get; set; }
        public virtual Action close { get; set; }

        public class SaveCommand : UICommand<AddFamilyMemberPresenter>
        {
            ServiceBus bus;

            public SaveCommand(ServiceBus bus)
            {
                this.bus = bus;
            }

            public override void run(AddFamilyMemberPresenter presenter)
            {
                bus.publish(new FamilyMemberToAdd
                {
                    first_name = presenter.first_name,
                    last_name = presenter.last_name,
                    date_of_birth = presenter.date_of_birth,
                });
                presenter.close();
            }
        }
    }
}