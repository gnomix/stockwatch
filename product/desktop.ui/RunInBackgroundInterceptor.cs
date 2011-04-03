using Castle.DynamicProxy;
using gorilla.infrastructure.threading;

namespace solidware.financials.windows.ui
{
    public class RunInBackgroundInterceptor : IInterceptor
    {
        CommandProcessor command_processor;

        public RunInBackgroundInterceptor(CommandProcessor command_processor)
        {
            this.command_processor = command_processor;
        }

        public void Intercept(IInvocation invocation)
        {
            command_processor.add(() =>
            {
                invocation.Proceed();
            });
        }
    }
}