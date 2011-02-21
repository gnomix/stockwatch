using System.Windows;
using desktop.ui.presenters;

namespace desktop.ui.views
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