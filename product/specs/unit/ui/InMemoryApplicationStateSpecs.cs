using Machine.Specifications;
using solidware.financials.windows.ui;

namespace specs.unit.ui
{
    public class InMemoryApplicationStateSpecs
    {
        public class when_storing_something_in_memory
        {
            It should_be_easy_to_pull_it_out_of_memory = () =>
            {
                result.ShouldEqual(token);
            };

            Establish context = () =>
            {
                token = new TestToken();
                sut = new InMemoryApplicationState();
            };

            Because of = () =>
            {
                sut.PushIn(token);
                result = sut.PullOut<TestToken>();
            };

            static ApplicationState sut;
            static TestToken token;
            static TestToken result;
        }

        class TestToken {}
    }
}