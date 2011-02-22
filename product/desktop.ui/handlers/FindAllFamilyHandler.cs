using desktop.ui.handlers.domain;
using desktop.ui.handlers.orm;
using desktop.ui.presenters;
using utility;

namespace desktop.ui.handlers
{
    public class FindAllFamilyHandler : Handles<FindAllFamily>
    {
        PersonRepository people;
        Mapper mapper;
        ServiceBus bus;

        public FindAllFamilyHandler(PersonRepository people, Mapper mapper, ServiceBus bus)
        {
            this.people = people;
            this.bus = bus;
            this.mapper = mapper;
        }

        public void handle(FindAllFamily item)
        {
            people
                .find_all()
                .map_all_using<Person, AddedNewFamilyMember>(mapper)
                .each(x => bus.publish(x));
        }
    }
}