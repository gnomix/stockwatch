using Castle.DynamicProxy;
using Machine.Specifications;
using Rhino.Mocks;
using solidware.financials.service.orm;

namespace specs.unit.service.orm
{
    public class UnitOfWorkInterceptorSpecs
    {
        public abstract class concern
        {
            Establish context = () =>
            {
                unit_of_work_factory = Create.dependency<UnitOfWorkFactory>();
                sut = new UnitOfWorkInterceptor(unit_of_work_factory);
            };

            static protected UnitOfWorkInterceptor sut;
            static protected UnitOfWorkFactory unit_of_work_factory;
        }

        [Subject(typeof(UnitOfWorkInterceptor))]
        public class when_starting_a_new_unit_of_work : concern
        {
            It should_proceed_with_the_invocation = () =>
            {
                invocation.AssertWasCalled(x => x.Proceed());
            };

            It should_commit_the_unit_of_work = () =>
            {
                unit_of_work.AssertWasCalled(x => x.commit());
                unit_of_work.AssertWasCalled(x => x.Dispose());
            };

            Establish context = () =>
            {
                invocation = Create.an<IInvocation>();
                unit_of_work = Create.an<UnitOfWork>();
                unit_of_work_factory.Stub(x => x.create()).Return(unit_of_work);
            };

            Because of = () => { sut.Intercept(invocation); };

            static IInvocation invocation;
            static UnitOfWork unit_of_work;
        }
    }
}