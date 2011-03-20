using Castle.DynamicProxy;

namespace solidware.financials.service.orm
{
    public class UnitOfWorkInterceptor : IInterceptor
    {
        readonly UnitOfWorkFactory unit_of_work_factory;

        public UnitOfWorkInterceptor(UnitOfWorkFactory unit_of_work_factory)
        {
            this.unit_of_work_factory = unit_of_work_factory;
        }

        public void Intercept(IInvocation invocation)
        {
            using (var unit_of_work = unit_of_work_factory.create())
            {
                invocation.Proceed();
                unit_of_work.commit();
            }
        }
    }
}