namespace solidware.financials.windows.ui
{
    public interface UICommandBuilder
    {
        ObservableCommand build<Command>(Presenter presenter) where Command : UICommand;
        ObservableCommand build<Command, Specification>(Presenter presenter) where Command : UICommand where Specification : UISpecification;
    }
}