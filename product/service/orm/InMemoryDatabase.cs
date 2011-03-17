using System;
using System.Collections.Generic;
using gorilla.utility;
using System.Linq;
using solidware.financials.service.domain;

namespace solidware.financials.service.orm
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