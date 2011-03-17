using gorilla.infrastructure.container;

namespace solidware.financials.windows.ui
{
    public class WPFPresenterFactory : PresenterFactory
    {
        DependencyRegistry container;

        public WPFPresenterFactory(DependencyRegistry container)
        {
            this.container = container;
        }

        public T create<T>() where T : Presenter
        {
            return container.get_a<T>();
        }
    }
}