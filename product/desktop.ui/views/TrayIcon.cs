using System.Windows.Controls.Primitives;
using Hardcodet.Wpf.TaskbarNotification;
using solidware.financials.messages;
using solidware.financials.windows.ui.presenters;

namespace solidware.financials.windows.ui.views
{
    public class TrayIcon : TaskbarIcon, Announcer
    {
        public virtual void Say(string message)
        {
            ShowCustomBalloon(new Toast {DataContext = new ToastViewModel {BalloonText = message}}, PopupAnimation.Slide, 4000);
        }
    }
}