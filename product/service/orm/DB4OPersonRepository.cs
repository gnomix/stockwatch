using System;
using System.Collections.Generic;
using System.Linq;
using Db4objects.Db4o;
using solidware.financials.service.domain;

namespace solidware.financials.service.orm
{
    public class DB4OPersonRepository : PersonRepository
    {
        IObjectContainer session;

        public DB4OPersonRepository(IObjectContainer session)
        {
            this.session = session;
        }

        public void save(Person person)
        {
            session.Store(person);
        }

        public Person find_by(Guid id)
        {
            return session.Query<Person>().Single(x => x.id == id);
        }

        public IEnumerable<Person> find_all()
        {
            return session.Query<Person>();
        }
    }
}