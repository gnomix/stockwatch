using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using solidware.financials.windows.ui.views.icons;

namespace solidware.financials.windows.ui.views
{
    public partial class Toast
    {
        bool isClosing;

        public Toast()
        {
            InitializeComponent();
            closeImage.apply_icon(UIIcon.Close);
            infoImage.apply_icon(UIIcon.Info);
            TrayIcon.AddBalloonClosingHandler(this, OnBalloonClosing);
        }

        void OnBalloonClosing(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            isClosing = true;
        }

        void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TrayIcon.GetParentTaskbarIcon(this).CloseBalloon();
        }

        void grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isClosing) return;
            TrayIcon.GetParentTaskbarIcon(this).ResetBalloonCloseTimer();
        }

        void OnFadeOutCompleted(object sender, EventArgs e)
        {
            ((Popup) Parent).IsOpen = false;
        }
    }
}