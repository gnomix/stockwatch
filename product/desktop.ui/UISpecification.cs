using gorilla.utility;

namespace solidware.financials.windows.ui
{
    public interface UISpecification
    {
        bool is_satisfied_by<T>(T presenter) where T : Presenter;
    }

    public abstract class UISpecification<TPresenter> : UISpecification, Specification<TPresenter>
        where TPresenter : Presenter
    {
        bool UISpecification.is_satisfied_by<T>(T presenter)
        {
            return is_satisfied_by(presenter.downcast_to<TPresenter>());
        }

        public abstract bool is_satisfied_by(TPresenter presenter);
    }
}