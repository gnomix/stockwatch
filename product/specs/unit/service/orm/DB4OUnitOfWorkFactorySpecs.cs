using gorilla.utility;
using Machine.Specifications;
using Rhino.Mocks;
using solidware.financials.service.orm;

namespace specs.unit.service.orm
{
    public class DB4OUnitOfWorkFactorySpecs
    {
        public abstract class concern
        {
            Establish context = () =>
            {
                factory = Create.dependency<ConnectionFactory>();
                the_context = Create.dependency<Context>();
                sut = new DB40UnitOfWorkFactory(factory, the_context);
            };

            static protected ConnectionFactory factory;
            static protected Context the_context;
            static protected UnitOfWorkFactory sut;
        }

        [Subject(typeof(DB40UnitOfWorkFactory))]
        public class when_starting_a_new_unit_of_work : concern
        {
            It should_bind_a_new_connection_to_the_current_context = () =>
            {
                the_context.AssertWasCalled(x => x.add(new TypedKey<Connection>(), connection));
            };

            It should_return_a_new_unit_of_work = () =>
            {
                result.ShouldBe(typeof(DB4OUnitOfWork));
            };

            Establish context = () =>
            {
                connection = Create.an<Connection>();
                factory.Stub(x => x.Open()).Return(connection);
                the_context.Stub(x => x.contains(new TypedKey<Connection>())).Return(false);
            };

            Because of = () =>
            {
                result = sut.create();
            };

            static Connection connection;
            static UnitOfWork result;
        }
        [Subject(typeof(DB40UnitOfWorkFactory))]
        public class when_a_unit_of_work_has_already_been_started : concern
        {
            It should_not_bind_a_new_connection_to_the_current_context = () =>
            {
                the_context.AssertWasNotCalled(x => x.add(new TypedKey<Connection>(), connection));
            };

            It should_return_an_empty_unit_of_work = () =>
            {
                result.ShouldBe(typeof(EmptyUnitOfWork));
            };

            Establish context = () =>
            {
                connection = Create.an<Connection>();
                factory.Stub(x => x.Open()).Return(connection);
                the_context.Stub(x => x.contains(new TypedKey<Connection>())).Return(true);
            };

            Because of = () =>
            {
                result = sut.create();
            };

            static Connection connection;
            static UnitOfWork result;
        }
    }
}