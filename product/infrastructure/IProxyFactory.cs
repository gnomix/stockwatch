using Castle.DynamicProxy;

namespace solidware.financials.infrastructure
{
    public interface IProxyFactory
    {
        T CreateProxyFor<T>(T clazz, params IInterceptor[] interceptors) where T : class;
    }
}