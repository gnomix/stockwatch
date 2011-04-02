using System;
using System.Windows;
using gorilla.utility;

namespace solidware.financials.windows.ui
{
    public interface RegionManager
    {
        void region<Region>(Action<Region> configure) where Region : UIElement;
        void region<Region>(Configuration<Region> configure) where Region : UIElement;
    }
}