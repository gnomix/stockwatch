using gorilla.utility;

namespace solidware.financials.service.domain
{
    public class Person : Entity
    {
        static public Person New(string first_name, string last_name, Date date_of_birth)
        {
            return new Person
                   {
                       first_name = first_name,
                       last_name = last_name,
                       date_of_birth = date_of_birth,
                   };
        }

        public virtual string first_name { get; set; }
        public virtual string last_name { get; set; }
        public virtual Date date_of_birth { get; set; }
    }
}