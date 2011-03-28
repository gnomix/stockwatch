using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;
using solidware.financials.windows.ui.views.icons;

namespace solidware.financials.windows.ui.views
{
    public partial class FancyBalloon
    {
        bool isClosing;

        public FancyBalloon()
        {
            InitializeComponent();
            closeImage.apply_icon(UIIcon.Close);
            infoImage.apply_icon(UIIcon.Info);
            TaskbarIcon.AddBalloonClosingHandler(this, OnBalloonClosing);
        }

        static public readonly DependencyProperty BalloonTextProperty = DependencyProperty.Register("BalloonText", typeof (string), typeof (FancyBalloon), new FrameworkPropertyMetadata(""));

        public string BalloonText
        {
            get { return (string) GetValue(BalloonTextProperty); }
            set { SetValue(BalloonTextProperty, value); }
        }

        void OnBalloonClosing(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            isClosing = true;
        }

        void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TaskbarIcon.GetParentTaskbarIcon(this).CloseBalloon();
        }

        void grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isClosing) return;
            TaskbarIcon.GetParentTaskbarIcon(this).ResetBalloonCloseTimer();
        }

        void OnFadeOutCompleted(object sender, EventArgs e)
        {
            ((Popup) Parent).IsOpen = false;
        }
    }
}