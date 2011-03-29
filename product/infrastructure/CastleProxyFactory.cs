using Castle.DynamicProxy;

namespace solidware.financials.infrastructure
{
    public class CastleProxyFactory : IProxyFactory
    {
        ProxyGenerator generator = new ProxyGenerator();

        public Interface CreateProxyFor<Interface>(Interface target, params IInterceptor[] interceptors)
            where Interface : class
        {
            return generator.CreateInterfaceProxyWithTarget(target, interceptors);
        }
    }
}