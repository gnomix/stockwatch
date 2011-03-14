using System;
using System.Windows;
using desktop.ui.presenters;

namespace desktop.ui.views
{
    public partial class DisplayCanadianTaxInformationDialog : Dialog<DisplayCanadianTaxInformationViewModel>
    {
        public DisplayCanadianTaxInformationDialog()
        {
            InitializeComponent();
            browser.Navigate(new Uri("http://www.cra-arc.gc.ca/tx/ndvdls/fq/txrts-eng.html"));
        }

        public void open()
        {
            Owner = Application.Current.MainWindow;
            ShowDialog();
        }
    }
}