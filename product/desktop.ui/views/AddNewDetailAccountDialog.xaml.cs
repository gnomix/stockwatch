using System.Windows;
using desktop.ui.presenters;

namespace desktop.ui.views
{
    public partial class AddNewDetailAccountDialog : Dialog<AddNewDetailAccountPresenter>
    {
        public AddNewDetailAccountDialog()
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