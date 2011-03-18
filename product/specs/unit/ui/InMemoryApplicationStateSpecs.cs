using Machine.Specifications;
using solidware.financials.windows.ui;

namespace specs.unit.ui
{
    public class InMemoryApplicationStateSpecs
    {
        public abstract class concern
        {
            Establish context = () =>
            {
                sut = new InMemoryApplicationState();
            };

            static protected ApplicationState sut;
        }

        public class when_storing_something_in_memory : concern
        {
            It should_be_easy_to_pull_it_out_of_memory = () =>
            {
                result.ShouldEqual(token);
            };

            Because of = () =>
            {
                token = new TestToken();
                sut.PushIn(token);
                result = sut.PullOut<TestToken>();
            };

            static TestToken token;
            static TestToken result;
        }

        public class when_checking_if_a_token_has_been_added : concern
        {
            It should_tell_you_when_it_has = () =>
            {
                sut.PushIn(new TestToken());
                sut.HasBeenPushedIn<TestToken>().ShouldBeTrue();
            };

            It should_tell_you_when_it_has_NOT = () =>
            {
                sut.HasBeenPushedIn<string>().ShouldBeFalse();
            };
        }

        class TestToken {}
    }
}