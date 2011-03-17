using desktop.ui.eventing;
using desktop.ui.events;

namespace desktop.ui.presenters
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