namespace desktop.ui
{
    public interface UICommandBuilder
    {
        IObservableCommand build<Command>(Presenter presenter) where Command : UICommand;
        IObservableCommand build<Command, Specification>(Presenter presenter) where Command : UICommand where Specification : UISpecification;
    }
}