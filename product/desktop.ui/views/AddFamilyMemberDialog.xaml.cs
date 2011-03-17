using System.Windows;
using solidware.financials.windows.ui.presenters;

namespace solidware.financials.windows.ui.views
{
    public partial class AddFamilyMemberDialog : Dialog<AddFamilyMemberPresenter>
    {
        public AddFamilyMemberDialog()
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