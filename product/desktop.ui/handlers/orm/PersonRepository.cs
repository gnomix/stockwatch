using System;
using System.Collections.Generic;
using desktop.ui.handlers.domain;

namespace desktop.ui.handlers.orm
{
    public interface PersonRepository
    {
        void save(Person person);
        Person find_by(Guid id);
        IEnumerable<Person> find_all();
    }
}