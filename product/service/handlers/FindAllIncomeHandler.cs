using System.Linq;
using gorilla.utility;
using solidware.financials.infrastructure;
using solidware.financials.messages;
using solidware.financials.service.orm;

namespace solidware.financials.service.handlers
{
    public class FindAllIncomeHandler : Handles<FindAllIncome>
    {
        PersonRepository people;
        Mapper mapper;
        ServiceBus bus;

        public FindAllIncomeHandler(PersonRepository people, Mapper mapper, ServiceBus bus)
        {
            this.people = people;
            this.mapper = mapper;
            this.bus = bus;
        }

        public void handle(FindAllIncome item)
        {
            people
                .find_all()
                .Select(x => new IncomeMessage
                             {
                                 PersonId = x.id,
                                 Amount = x.income_account.balance(),
                             })
                .each(x => bus.publish(x));
        }
    }
}