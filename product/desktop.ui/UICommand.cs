using gorilla.utility;

namespace desktop.ui
{
    public interface UICommand
    {
        void run<T>(T presenter) where T : Presenter;
    }

    public abstract class UICommand<TPresenter> : UICommand where TPresenter : Presenter
    {
        void UICommand.run<T1>(T1 presenter)
        {
            run(presenter.downcast_to<TPresenter>());
        }

        public abstract void run(TPresenter presenter);
    }
}