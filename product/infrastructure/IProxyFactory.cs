using Castle.DynamicProxy;

namespace solidware.financials.infrastructure
{
    public interface IProxyFactory
    {
        T CreateProxyFor<T>(T target, params IInterceptor[] interceptors) where T : class;
    }
}