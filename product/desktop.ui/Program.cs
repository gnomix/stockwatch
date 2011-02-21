using System;
using System.Security.Principal;
using System.Windows;
using System.Windows.Threading;
using desktop.ui.bootstrappers;
using desktop.ui.views;
using infrastructure.logging;

namespace desktop.ui
{
    static public class Program
    {
        [STAThread]
        static public void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (o, e) =>
            {
                (e.ExceptionObject as Exception).add_to_log();
            };
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            Dispatcher.CurrentDispatcher.UnhandledException += (o, e) =>
            {
                e.Exception.add_to_log();
                new ErrorWindow {DataContext = e.Exception}.ShowDialog();
                e.Handled = true;
            };
            new WPFApplication
            {
                ShutdownMode = ShutdownMode.OnMainWindowClose,
            }.Run(ClientBootstrapper.create_window());
        }
    }
}