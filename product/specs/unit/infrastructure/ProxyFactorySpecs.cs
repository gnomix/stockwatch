using Castle.DynamicProxy;
using gorilla.utility;
using Machine.Specifications;
using solidware.financials.infrastructure;

namespace specs.unit.infrastructure
{
    public class ProxyFactorySpecs
    {
        public class concern
        {
            Establish context = () => { sut = new CastleProxyFactory(); };

            static protected IProxyFactory sut;
        }

        public class when_creating_a_proxy_to_intercept_calls_to_a_class : concern
        {
            It should_intercept_calls_made_to_that_class = () =>
            {
                //
                interceptor.Intercepted.ShouldBeTrue();
            };

            It should_make_the_call_on_the_original_class = () =>
            {
                //
                command.result.ShouldEqual("mo");
            };

            Establish context = () => { command = new TestCommand("blah"); };

            Because of = () =>
            {
                interceptor = new TestInterceptor();
                var proxy = sut.CreateProxyFor<Command<string>>(command, interceptor);
                proxy.run("mo");
            };

            static TestCommand command;
            static TestInterceptor interceptor;
        }

        public class TestCommand : Command<string>
        {
            readonly string needAConstructur;

            public TestCommand(string needAConstructur)
            {
                this.needAConstructur = needAConstructur;
            }

            public virtual void run(string item)
            {
                result = item;
            }

            public string result { get; set; }
        }

        public class TestInterceptor : IInterceptor
        {
            public void Intercept(IInvocation invocation)
            {
                Intercepted = true;
                invocation.Proceed();
            }

            public bool Intercepted { get; set; }
        }
    }
}