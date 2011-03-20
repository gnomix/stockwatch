using Castle.DynamicProxy;

namespace solidware.financials.service.orm
{
    public class UnitOfWorkInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}