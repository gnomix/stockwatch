using System;
using System.Collections.Generic;
using System.Windows;
using gorilla.utility;

namespace solidware.financials.windows.ui.views
{
    public partial class Shell : RegionManager
    {
        public Shell()
        {
            InitializeComponent();
            regions = new Dictionary<Type, UIElement>
                      {
                          {GetType(), this},
                          {typeof (Window), this},
                          {StatusBar.GetType(), StatusBar},
                          {Menu.GetType(), Menu},
                          {DockManager.GetType(), DockManager},
                          {Tabs.GetType(), Tabs},
                          {SelectedFamilyMember.GetType(), SelectedFamilyMember},
                      };
        }

        public void region<Region>(Action<Region> configure) where Region : UIElement
        {
            ensure_that_the_region_exists<Region>();
            configure(regions[typeof (Region)].downcast_to<Region>());
        }

        void ensure_that_the_region_exists<Region>()
        {
            if (!regions.ContainsKey(typeof (Region)))
                throw new Exception("Could not find region: {0}".format(typeof (Region)));
        }

        readonly IDictionary<Type, UIElement> regions;
    }
}