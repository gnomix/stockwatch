using System;

namespace solidware.financials.windows.ui
{
    public interface DialogPresenter : Presenter
    {
        Action close { get; set; }
    }
}