using desktop.ui.eventing;

namespace desktop.ui.messages.@private
{
    public class AddIncomeCommandMessage : Event
    {
        public decimal amount { get; set; }
    }
}