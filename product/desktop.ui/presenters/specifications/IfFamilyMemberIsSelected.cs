using solidware.financials.windows.ui.events;

namespace solidware.financials.windows.ui.presenters.specifications
{
    public class IfFamilyMemberIsSelected<T> : UISpecification<T> where T : Presenter
    {
        ApplicationState state;

        public IfFamilyMemberIsSelected(ApplicationState state)
        {
            this.state = state;
        }

        public override bool is_satisfied_by(T item)
        {
            return state.HasBeenPushedIn<SelectedFamilyMember>();
        }
    }
}