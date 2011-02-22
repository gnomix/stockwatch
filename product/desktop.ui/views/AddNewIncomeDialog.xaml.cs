using System.Windows;
using desktop.ui.presenters;

namespace desktop.ui.views
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