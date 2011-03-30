﻿using solidware.financials.windows.ui.presenters;

namespace solidware.financials.windows.ui.views
{
    public partial class TaxSummaryTab : Tab<TaxSummaryPresenter>
    {
        public TaxSummaryTab()
        {
            InitializeComponent();
        }

        public void bind_to(TaxSummaryPresenter presenter)
        {
            DataContext = presenter;
        }
    }
}