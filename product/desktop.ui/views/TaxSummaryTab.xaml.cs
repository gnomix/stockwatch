using System;
using desktop.ui.presenters;

namespace desktop.ui.views
{
    public partial class TaxSummaryTab : Tab<TaxSummaryPresenter>
    {
        public TaxSummaryTab()
        {
            InitializeComponent();
            browser.Navigate(new Uri("http://www.cra-arc.gc.ca/tx/ndvdls/fq/txrts-eng.html"));
        }
    }
}