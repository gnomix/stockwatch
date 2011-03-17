using solidware.financials.infrastructure.eventing;

namespace solidware.financials.messages
{
    public class AddIncomeCommandMessage : Event
    {
        public decimal amount { get; set; }
    }
}