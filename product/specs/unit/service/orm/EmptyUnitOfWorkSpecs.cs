using Machine.Specifications;
using solidware.financials.service.orm;

namespace specs.unit.service.orm
{
    public class EmptyUnitOfWorkSpecs
    {
        [Subject(typeof(EmptyUnitOfWork))]
        public class When_disposing_an_empty_unit_of_work
        {
            It should_not_do_anything = () =>
            {
                new EmptyUnitOfWork().Dispose();
            };
        }
        [Subject(typeof(EmptyUnitOfWork))]
        public class When_committing_an_empty_unit_of_work
        {
            It should_not_do_anything = () =>
            {
                new EmptyUnitOfWork().commit();
            };
        }
    }
}