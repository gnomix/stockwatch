using gorilla.utility;
using Machine.Specifications;
using Rhino.Mocks;
using solidware.financials.service.orm;

namespace specs.unit.service.orm
{
    public class DB4OUnitOfWorkSpecs
    {
        public class concern
        {
            Establish context = () =>
            {
                session = Create.dependency<Connection>();
                the_context = Create.dependency<Context>();
                sut = new DB4OUnitOfWork(session, the_context);
            };

            static protected DB4OUnitOfWork sut;
            static protected Connection session;
            static Context the_context;
        }

        [Subject(typeof(DB4OUnitOfWork))]
        public class when_disposing_a_unit_of_work_that_has_not_been_committed : concern
        {
            It should_roll_back_the_transaction = () =>
            {
                session.AssertWasCalled(x => x.Rollback());
            };
            It should_dispose_the_transaction = () =>
            {
                session.AssertWasCalled(x => x.Dispose());
            };

            Because of = () =>
            {
                sut.Dispose();
            };
        }
        [Subject(typeof(DB4OUnitOfWork))]
        public class when_disposing_a_unit_of_work_that_has_been_committed : concern
        {
            It should_not_roll_back_the_transaction = () =>
            {
                session.AssertWasNotCalled(x => x.Rollback());
            };
            It should_dispose_the_transaction = () =>
            {
                session.AssertWasCalled(x => x.Dispose());
            };

            Because of = () =>
            {
                sut.commit();
                sut.Dispose();
            };
        }
        [Subject(typeof(DB4OUnitOfWork))]
        public class when_committing_a_unit_of_work : concern
        {
            It should_commit_the_transaction = () =>
            {
                session.AssertWasCalled(x => x.Commit());
            };

            Because of = () =>
            {
                sut.commit();
            };
        }
    }
}