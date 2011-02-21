using System;
using System.Windows;

namespace desktop.ui
{
    public interface RegionManager
    {
        void region<Control>(Action<Control> configure) where Control : UIElement;
    }
}