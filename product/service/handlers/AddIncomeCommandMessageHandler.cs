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
            var person = family.find_by(item.PersonId);
            person.IncomeAccount().deposit(item.Amount);
            family.save(person);
            bus.publish<IncomeMessage>(x =>
            {
                x.Amount = item.Amount;
                x.PersonId = item.PersonId;
                x.Date = item.Date;
            });
        }
    }
}