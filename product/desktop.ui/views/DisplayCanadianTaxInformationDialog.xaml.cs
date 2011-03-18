using System;

namespace solidware.financials.windows.ui.views
{
    public partial class DisplayCanadianTaxInformationDialog
    {
        public DisplayCanadianTaxInformationDialog()
        {
            InitializeComponent();
            browser.Navigate(new Uri("http://www.cra-arc.gc.ca/tx/ndvdls/fq/txrts-eng.html"));
        }

    }
}