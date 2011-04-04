using solidware.financials.windows.ui.presenters;

namespace solidware.financials.windows.ui.views
{
    public partial class SingleStockTab : Tab<SingleStockPresenter>
    {
        public SingleStockTab()
        {
            InitializeComponent();
        }

        public void bind_to(SingleStockPresenter presenter)
        {
            DataContext = presenter;
        }
    }
}