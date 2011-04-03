using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using gorilla.infrastructure.threading;
using gorilla.utility;
using Machine.Specifications;
using solidware.financials.windows.ui;

namespace specs.unit.ui
{
    public class RunInBackgroundInterceptorSpecs
    {
        public abstract class concern
        {
            Establish context = () =>
            {
                processor = new TestCommandProcessor();
                sut = new RunInBackgroundInterceptor(processor);
            };

            static protected RunInBackgroundInterceptor sut;
            static protected TestCommandProcessor processor;
        }

        public class when_running_all_actions_against_a_proxy_on_a_background_thread : concern
        {
            Establish context = () =>
            {
                invocation = Create.an<IInvocation>();
            };

            Because of = () =>
            {
                sut.Intercept(invocation);
            };

            It should_push_a_command_on_the_background_thread_to_invoke_the_call = () =>
            {
                invocation.was_not_told_to(x => x.Proceed());
                processor.actions.Count.should_be_equal_to(1);
                processor.actions.First().Invoke();
                invocation.received(x => x.Proceed());
            };

            static IInvocation invocation;
        }
    }

    public class TestCommandProcessor : CommandProcessor
    {
        public List<Action> actions = new List<Action>();

        public void run() {}

        public void add(Action command)
        {
            actions.add(command);
        }

        public void add(Command command_to_process)
        {
            add(command_to_process.run);
        }

        public void stop() {}
    }
}