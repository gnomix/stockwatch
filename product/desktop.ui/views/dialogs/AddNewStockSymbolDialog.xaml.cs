using System;
using solidware.financials.windows.ui.presenters;

namespace solidware.financials.windows.ui.views.dialogs
{
    public partial class AddNewStockSymbolDialog : Dialog<AddNewStockSymbolPresenter>
    {
        public AddNewStockSymbolDialog()
        {
            InitializeComponent();
        }

        public void open(AddNewStockSymbolPresenter presenter)
        {
            throw new NotImplementedException();
        }
    }
}