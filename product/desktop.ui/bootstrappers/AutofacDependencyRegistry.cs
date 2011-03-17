using System.Collections.Generic;
using Autofac;
using gorilla.infrastructure.container;

namespace solidware.financials.windows.ui.bootstrappers
{
    public class AutofacDependencyRegistry : DependencyRegistry
    {
        readonly IContainer container;

        public AutofacDependencyRegistry(ContainerBuilder builder)
        {
            builder.Register(x => this).As<DependencyRegistry>().SingleInstance();
            container = builder.Build();
        }

        public Contract get_a<Contract>()
        {
            return container.Resolve<Contract>();
        }

        public IEnumerable<Contract> get_all<Contract>()
        {
            return container.Resolve<IEnumerable<Contract>>();
        }
    }
}