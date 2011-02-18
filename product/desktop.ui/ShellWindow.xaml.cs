using System;
using System.Collections.Generic;
using System.Windows;

namespace desktop.ui
{
    public partial class ShellWindow : RegionManager
    {
        private IDictionary<Type, UIElement> regions;

        public ShellWindow()
        {
            InitializeComponent();
            regions = new Dictionary<Type, UIElement>
                          {
                              {GetType(), this},
                              {typeof (Window), this},
                          };
        }

        public void region<Control>(Action<Control> configure) where Control : UIElement
        {
            ensure_that_the_region_exists<Control>();
            configure(regions[typeof (Control)].downcast_to<Control>());
        }

        private void ensure_that_the_region_exists<Region>()
        {
            if (!regions.ContainsKey(typeof (Region)))
                throw new Exception("Could not find region: {0}".format(typeof (Region)));
        }
    }
}