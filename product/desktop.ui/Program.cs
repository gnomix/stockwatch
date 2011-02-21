using System;
using System.Security.Principal;
using System.Windows.Threading;

namespace desktop.ui
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (o, e) => { };
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            Dispatcher.CurrentDispatcher.UnhandledException += (o, e) => { };

            new ConfigureContainerCommand()
                .then(() => { Console.Out.WriteLine("starting"); })
                .then(() => { IOC.Resolve<IApplicationController>().start(); })
                .run();
            
            new WPFApplication().Run(IOC.Resolve<ShellWindow>());
        }
    }
}