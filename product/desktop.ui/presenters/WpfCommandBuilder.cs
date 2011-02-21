using infrastructure.container;

namespace desktop.ui.presenters
{
    public class WPFCommandBuilder : UICommandBuilder
    {
        DependencyRegistry container;

        public WPFCommandBuilder(DependencyRegistry container)
        {
            this.container = container;
        }

        public IObservableCommand build<Command>(Presenter presenter) where Command : UICommand
        {
            var command = container.get_a<Command>();
            return new SimpleCommand(() =>
            {
                command.run(presenter);
            });
        }
    }
}