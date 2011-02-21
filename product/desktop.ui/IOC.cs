using Autofac;

namespace desktop.ui
{
    public static class IOC
    {
        static IContainer container;

        public static void BindTo(IContainer container)
        {
            IOC.container = container;
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}