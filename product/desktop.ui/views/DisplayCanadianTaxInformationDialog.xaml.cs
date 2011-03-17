using System;
using System.Windows;
using solidware.financials.windows.ui.presenters;

namespace solidware.financials.windows.ui.views
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