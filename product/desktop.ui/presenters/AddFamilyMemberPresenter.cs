using System;
using gorilla.utility;

namespace desktop.ui.presenters
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

        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public IObservableCommand Save { get; set; }
        public IObservableCommand Cancel { get; set; }
        public Action close { get; set; }

        public class SaveCommand : UICommand<AddFamilyMemberPresenter>
        {
            ServiceBus bus;

            public SaveCommand(ServiceBus bus)
            {
                this.bus = bus;
            }

            public override void run(AddFamilyMemberPresenter presenter)
            {
                bus.publish<FamilyMemberToAdd>(x =>
                {
                    x.first_name = presenter.first_name;
                    x.last_name = presenter.last_name;
                    x.date_of_birth = presenter.date_of_birth;
                });
                presenter.close();
            }
        }
    }

    public class FamilyMemberToAdd
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime date_of_birth { get; set; }
    }
}