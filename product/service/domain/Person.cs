using solidware.financials.service.domain.accounting;

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
                       income_account = DetailAccount.New(Currency.CAD),
                   };
        }

        public virtual string first_name { get; private set; }
        public virtual string last_name { get; private set; }
        public virtual Date date_of_birth { get; private set; }
        public virtual DetailAccount income_account { get; private set; }

        public virtual DetailAccount IncomeAccount()
        {
            return income_account;
        }
    }
}