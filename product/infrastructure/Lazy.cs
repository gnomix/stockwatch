using System;
using Castle.DynamicProxy;
using gorilla.infrastructure.container;
using gorilla.utility;

namespace solidware.financials.infrastructure
{
    static public class Lazy
    {
        static public T load<T>() where T : class
        {
            return load(Resolve.the<T>);
        }

        static public T load<T>(Func<T> get_the_implementation) where T : class
        {
            return create_proxy_for<T>(create_interceptor_for(get_the_implementation));
        }

        static IInterceptor create_interceptor_for<T>(Func<T> get_the_implementation) where T : class
        {
            return new LazyLoadedInterceptor<T>(get_the_implementation.memorize());
        }

        static T create_proxy_for<T>(IInterceptor interceptor) where T : class
        {
            return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(interceptor);
        }

        class LazyLoadedInterceptor<T> : IInterceptor
        {
            readonly Func<T> get_the_implementation;

            public LazyLoadedInterceptor(Func<T> get_the_implementation)
            {
                this.get_the_implementation = get_the_implementation;
            }

            public void Intercept(IInvocation invocation)
            {
                var method = invocation.GetConcreteMethodInvocationTarget();
                if( null== method)
                {
                    invocation.ReturnValue = invocation.Method.Invoke(get_the_implementation(), invocation.Arguments);
                }
                else
                {
                    invocation.ReturnValue = method.Invoke(get_the_implementation(), invocation.Arguments);
                }
            }
        }
    }
}