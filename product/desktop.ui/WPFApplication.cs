using System;
using System.Security.Principal;
using System.Windows;

namespace desktop.ui
{
    public class WPFApplication : Application
    {
        public WPFApplication()
        {
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}