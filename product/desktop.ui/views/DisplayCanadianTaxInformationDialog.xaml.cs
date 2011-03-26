using System;
using solidware.financials.windows.ui.views.icons;

namespace solidware.financials.windows.ui.views
{
    public partial class DisplayCanadianTaxInformationDialog
    {
        public DisplayCanadianTaxInformationDialog()
        {
            InitializeComponent();
            Icon = UIIcon.Help.BitmapFrame();
            browser.Navigate(new Uri("http://www.cra-arc.gc.ca/tx/ndvdls/fq/txrts-eng.html"));
        }
    }
}