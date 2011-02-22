using System;
using System.Collections.Generic;
using desktop.ui.handlers.domain;
using utility;
using System.Linq;

namespace desktop.ui.handlers.orm
{
    public class InMemoryDatabase : PersonRepository
    {
        HashSet<Person> people = new HashSet<Person>();

        public void save(Person person)
        {
            person.id = new Id<Guid>(Guid.NewGuid());
            people.Add(person);
        }

        public Person find_by(Guid id)
        {
            return people.Single(x => x.id == id);
        }

        public IEnumerable<Person> find_all()
        {
            return people.all();
        }
    }
}