using System;

namespace desktop.ui
{
    public interface DialogPresenter : Presenter
    {
        Action close { get; set; }
    }
}