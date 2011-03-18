using System;
using Machine.Specifications;
using Rhino.Mocks;
using solidware.financials.infrastructure;
using solidware.financials.messages;
using solidware.financials.windows.ui;
using solidware.financials.windows.ui.events;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.presenters.specifications;

namespace specs.unit.ui.presenters
{
    public class AddNewIncomeViewModelSpecs
    {
        public class concern
        {
            Establish context = () =>
            {
                command_builder = Create.dependency<UICommandBuilder>();

                sut = new AddNewIncomeViewModel(command_builder);
            };

            static protected AddNewIncomeViewModel sut;
            static protected UICommandBuilder command_builder;
        }

        public class when_clicking_the_cancel_button : concern
        {
            It should_invoke_the_cancel_command = () =>
            {
                cancel_command.received(x => x.Execute(sut));
            };

            Establish context = () =>
            {
                cancel_command = Create.an<IObservableCommand>();
                command_builder.Stub(x => x.build<CancelCommand>(sut)).Return(cancel_command);
            };

            Because of = () =>
            {
                sut.present();
                sut.Cancel.Execute(sut);
            };

            static IObservableCommand cancel_command;
        }

        public class when_clicking_the_add_button : concern
        {
            It should_invoke_the_add_command = () =>
            {
                add_command.received(x => x.Execute(sut));
            };

            Establish context = () =>
            {
                add_command = Create.an<IObservableCommand>();
                command_builder.Stub(x => x.build<AddNewIncomeViewModel.AddIncomeCommand, IfFamilyMemberIsSelected<AddNewIncomeViewModel>>(sut)).Return(add_command);
            };

            Because of = () =>
            {
                sut.present();
                sut.Add.Execute(sut);
            };

            static IObservableCommand add_command;
        }

        public class when_the_add_button_is_pressed
        {
            It should_push_a_message_to_the_service_bus = () =>
            {
                bus.received(x => x.publish(new AddIncomeCommandMessage
                                            {
                                                Amount = amount,
                                                PersonId = personId
                                            }));
            };

            It should_close_the_dialog = () =>
            {
                closed.ShouldBeTrue();
            };

            Establish context = () =>
            {
                bus = Create.dependency<ServiceBus>();
                state = Create.an<ApplicationState>();
                var person = new SelectedFamilyMember {id = personId};
                state.Stub(x => x.PullOut<SelectedFamilyMember>()).Return(person);
                presenter = Create.an<AddNewIncomeViewModel>();
                presenter.Stub(x => x.amount).Return(amount);
                presenter.Stub(x => x.close).Return(() => { closed = true; });

                sut = new AddNewIncomeViewModel.AddIncomeCommand(bus, state);
            };

            Because of = () =>
            {
                sut.run(presenter);
            };

            static AddNewIncomeViewModel.AddIncomeCommand sut;
            static ServiceBus bus;
            static AddNewIncomeViewModel presenter;
            static decimal amount = 1000m;
            static ApplicationState state;
            static Guid personId = Guid.NewGuid();
            static bool closed;
        }
    }
}