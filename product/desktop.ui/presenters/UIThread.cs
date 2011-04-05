using System;
using System.Windows;

namespace solidware.financials.windows.ui.presenters
{
    static public class UIThread
    {
        static public void Run(Action action)
        {
            if (Application.Current != null)
                Application.Current.Dispatcher.BeginInvoke(action);
            else
                action();
        }
    }
}