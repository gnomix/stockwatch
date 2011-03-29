using System;
using System.Collections.Generic;
using System.Linq;
using gorilla.utility;
using solidware.financials.service.domain;
using Db4objects.Db4o.Linq;

namespace solidware.financials.service.orm
{
    public class DB4OPersonRepository : PersonRepository
    {
        Connection session;

        public DB4OPersonRepository(Connection session)
        {
            this.session = session;
        }

        public void save(Person person)
        {
            if(person.id.Equals(Id<Guid>.Default))
                person.id = new Id<Guid>(Guid.NewGuid());
            session.Store(person);
        }

        public Person find_by(Guid id)
        {
            return session.AsQueryable<Person>().Single(x => x.id == id);
        }

        public IEnumerable<Person> find_all()
        {
            return session.AsQueryable<Person>();
        }
    }
}