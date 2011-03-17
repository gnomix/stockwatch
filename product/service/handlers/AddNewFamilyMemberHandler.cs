using desktop.ui.handlers.domain;
using desktop.ui.handlers.orm;
using desktop.ui.messages.@private;
using desktop.ui.presenters;

namespace desktop.ui.handlers
{
    public class AddNewFamilyMemberHandler : Handles<FamilyMemberToAdd>
    {
        PersonRepository people;
        ServiceBus bus;

        public AddNewFamilyMemberHandler(PersonRepository people, ServiceBus bus)
        {
            this.people = people;
            this.bus = bus;
        }

        public void handle(FamilyMemberToAdd item)
        {
            var person = Person.New(item.first_name, item.last_name, item.date_of_birth);
            people.save(person);
            bus.publish<AddedNewFamilyMember>(x =>
            {
                x.id = person.id;
                x.first_name = person.first_name;
                x.last_name = person.last_name;
            });
        }
    }
}