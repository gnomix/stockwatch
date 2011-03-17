using System.Windows;
using solidware.financials.windows.ui.presenters;

namespace solidware.financials.windows.ui.views
{
    public partial class AddNewIncomeDialog : Dialog<AddNewIncomeViewModel>
    {
        public AddNewIncomeDialog()
        {
            InitializeComponent();
        }

        public void open()
        {
            Owner = Application.Current.MainWindow;
            ShowDialog();
        }
    }
}