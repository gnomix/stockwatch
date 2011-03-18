using gorilla.utility;
using Machine.Specifications;
using Rhino.Mocks;
using solidware.financials.windows.ui;
using solidware.financials.windows.ui.events;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.presenters.specifications;

namespace specs.unit.ui.presenters.specifications
{
    public class IfFamilyMemberIsSelectedSpecs
    {
        public abstract class concern
        {
            Establish context = () =>
            {
                state = Create.dependency<ApplicationState>();
                sut = new IfFamilyMemberIsSelected<Presenter>(state);
            };

            static protected Specification<AddNewIncomeViewModel> sut;
            static protected ApplicationState state;
        }

        public class when_a_family_member_has_been_selected : concern
        {
            It should_return_true = () =>
            {
                sut.is_satisfied_by(null).ShouldBeTrue();
            };

            Establish context = () =>
            {
                state.Stub(x => x.HasBeenPushedIn<SelectedFamilyMember>()).Return(true);
            };
        }

        public class when_a_family_member_has_not_been_selected : concern
        {
            It should_return_false = () =>
            {
                sut.is_satisfied_by(null).ShouldBeFalse();
            };
        }
    }
}