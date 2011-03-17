using System;
using System.Collections.Generic;
using solidware.financials.service.domain;

namespace solidware.financials.service.orm
{
    public interface PersonRepository
    {
        void save(Person person);
        Person find_by(Guid id);
        IEnumerable<Person> find_all();
    }
}