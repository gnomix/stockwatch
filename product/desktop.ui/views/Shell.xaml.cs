using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using gorilla.utility;
using solidware.financials.windows.ui.views.icons;

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
                          {ResizingPanel.GetType(), ResizingPanel},
                          {ButtonBar.GetType(), ButtonBar},
                          {TaskBarIcon.GetType(), TaskBarIcon},
                          {StockWatch.GetType(), StockWatch},
                      };
            DockManager.Loaded += (o, e) =>
            {
                if (File.Exists(settings_file)) DockManager.RestoreLayout(settings_file);
            };
            Closing += (o, e) => DockManager.SaveLayout(settings_file);
            Closing += (o, e) => TaskBarIcon.Dispose();
            Loaded += (o, e) =>
            {
                TaskBarIcon.Icon = UIIcon.Application.AsIcon();
                TaskBarIcon.Say("Welcome");
            };
        }

        public void region<Region>(Action<Region> configure) where Region : UIElement
        {
            ensure_that_the_region_exists<Region>();
            Action action = () =>
            {
                configure(regions[typeof (Region)].downcast_to<Region>());
            };
            Dispatcher.BeginInvoke(action);
        }

        public void region<Region>(Configuration<Region> configuration) where Region : UIElement
        {
            region<Region>(x => configuration.configure(x));
        }

        void ensure_that_the_region_exists<Region>()
        {
            if (!regions.ContainsKey(typeof (Region)))
                throw new Exception("Could not find region: {0}".format(typeof (Region)));
        }

        readonly IDictionary<Type, UIElement> regions;
        string settings_file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"mokhan.ca\momoney\default.ui.settings");
    }
}