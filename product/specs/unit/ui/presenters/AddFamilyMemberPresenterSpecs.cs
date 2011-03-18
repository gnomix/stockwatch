using System;
using Machine.Specifications;
using Rhino.Mocks;
using solidware.financials.infrastructure;
using solidware.financials.messages;
using solidware.financials.windows.ui;
using solidware.financials.windows.ui.presenters;

namespace specs.unit.ui.presenters
{
    public class AddFamilyMemberPresenterSpecs
    {
        public abstract class concern
        {
            Establish context = () =>
            {
                command_builder = Create.dependency<UICommandBuilder>();
                sut = new AddFamilyMemberPresenter(command_builder);
            };

            static protected UICommandBuilder command_builder;
            static protected AddFamilyMemberPresenter sut;
        }

        public class when_clicking_the_cancel_button : concern
        {
            It should_invoke_the_cancel_command = () =>
            {
                cancel.received(x => x.Execute(sut));
            };

            Establish context = () =>
            {
                cancel = Create.an<IObservableCommand>();
                command_builder.Stub(x => x.build<CancelCommand>(sut)).Return(cancel);
            };

            Because of = () =>
            {
                sut.present();
                sut.Cancel.Execute(sut);
            };

            static IObservableCommand cancel;
        }

        public class when_clicking_the_save_button : concern
        {
            It should_invoke_the_save_command = () =>
            {
                save.received(x => x.Execute(sut));
            };

            Establish context = () =>
            {
                save = Create.an<IObservableCommand>();
                command_builder.Stub(x => x.build<AddFamilyMemberPresenter.SaveCommand>(sut)).Return(save);
            };

            Because of = () =>
            {
                sut.present();
                sut.Save.Execute(sut);
            };

            static IObservableCommand save;
        }

        public class SaveCommandSpecs
        {
            public class when_clicked
            {
                It should_publish_a_message_on_the_bus = () =>
                {
                    bus.received(x => x.publish(new FamilyMemberToAdd
                                                {
                                                    first_name = "mo",
                                                    last_name = "khan",
                                                    date_of_birth = new DateTime(1984, 04, 28),
                                                }));
                };

                It should_close_the_dialog = () =>
                {
                    closed.ShouldBeTrue();
                };

                Establish context = () =>
                {
                    bus = Create.dependency<ServiceBus>();
                    presenter = Create.an<AddFamilyMemberPresenter>();
                    presenter.Stub(x => x.first_name).Return("mo");
                    presenter.Stub(x => x.last_name).Return("khan");
                    presenter.Stub(x => x.date_of_birth).Return(new DateTime(1984, 04, 28));
                    presenter.Stub(x => x.close).Return(() => { closed = true; });
                    sut = new AddFamilyMemberPresenter.SaveCommand(bus);
                };

                Because of = () =>
                {
                    sut.run(presenter);
                };

                static ServiceBus bus;
                static AddFamilyMemberPresenter.SaveCommand sut;
                static AddFamilyMemberPresenter presenter;
                static bool closed;
            }
        }
    }
}