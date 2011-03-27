using solidware.financials.infrastructure;
using solidware.financials.messages;
using solidware.financials.service.orm;

namespace solidware.financials.service.handlers
{
    public class AddIncomeCommandMessageHandler : Handles<AddIncomeCommandMessage>
    {
        PersonRepository family;
        ServiceBus bus;

        public AddIncomeCommandMessageHandler(PersonRepository family, ServiceBus bus)
        {
            this.family = family;
            this.bus = bus;
        }

        public void handle(AddIncomeCommandMessage item)
        {
            family.find_by(item.PersonId).IncomeAccount().deposit(item.Amount);
            bus.publish<IncomeMessage>(x =>
            {
                x.Amount = item.Amount;
                x.PersonId = item.PersonId;
            });
        }
    }
}