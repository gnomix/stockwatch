namespace solidware.financials.windows.ui
{
    public interface ApplicationState
    {
        Token PullOut<Token>();
        void PushIn<Token>(Token token);
        bool HasBeenPushedIn<Token>();
    }
}