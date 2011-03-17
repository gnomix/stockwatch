using solidware.financials.infrastructure.eventing;
using solidware.financials.windows.ui.events;

namespace solidware.financials.windows.ui.presenters
{
    public class IfFamilyMemberIsSelected : UISpecification<AddNewIncomeViewModel>,
                                            EventSubscriber<SelectedFamilyMember>
    {
        public override bool is_satisfied_by(AddNewIncomeViewModel item)
        {
            return is_selected;
        }

        public void notify(SelectedFamilyMember message)
        {
            is_selected = message != null;
        }

        bool is_selected;
    }
}