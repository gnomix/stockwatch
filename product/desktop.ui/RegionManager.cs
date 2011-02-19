using System;
using System.Windows;

namespace desktop.ui
{
    public interface IRegionManager
    {
        void Region<Control>(Action<Control> configure) where Control : UIElement;
    }
}