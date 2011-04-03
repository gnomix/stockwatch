using gorilla.infrastructure.container;

namespace solidware.financials.windows.ui.presenters
{
    public class WPFCommandBuilder : UICommandBuilder
    {
        DependencyRegistry container;

        public WPFCommandBuilder(DependencyRegistry container)
        {
            this.container = container;
        }

        public ObservableCommand build<Command>(Presenter presenter) where Command : UICommand
        {
            var command = container.get_a<Command>();
            return new SimpleCommand(() =>
            {
                command.run(presenter);
            });
        }

        public ObservableCommand build<Command, Specification>(Presenter presenter) where Command : UICommand where Specification : UISpecification
        {
            var command = container.get_a<Command>();
            var specification = container.get_a<Specification>();
            return new SimpleCommand(() =>
            {
                command.run(presenter);
            }, () => specification.is_satisfied_by(presenter) );
        }
    }
}