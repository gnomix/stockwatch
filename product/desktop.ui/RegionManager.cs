using System;
using System.Windows;

namespace solidware.financials.windows.ui
{
    public interface RegionManager
    {
        void region<Control>(Action<Control> configure) where Control : UIElement;
    }
}