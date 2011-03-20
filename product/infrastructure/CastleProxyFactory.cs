using Castle.DynamicProxy;

namespace solidware.financials.infrastructure
{
    public class CastleProxyFactory : IProxyFactory
    {
        ProxyGenerator generator = new ProxyGenerator();

        public Proxy CreateProxyFor<Proxy>(Proxy clazz, params IInterceptor[] interceptors) where Proxy : class
        {
            return generator.CreateClassProxyWithTarget(clazz, interceptors);
        }
    }
}