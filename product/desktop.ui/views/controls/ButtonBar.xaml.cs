using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.views.icons;

namespace solidware.financials.windows.ui.views.controls
{
    public partial class ButtonBar : View<ButtonBarPresenter>
    {
        public ButtonBar()
        {
            InitializeComponent();
        }

        public void AddCommand(string text, Action action, UIIcon icon)
        {
            AddCommand(text, new SimpleCommand(action), icon);
        }

        public void AddCommand(string text, ICommand command, UIIcon icon)
        {
            DockPanel.Add<Button>(x =>
            {
                x.ToIconButton(icon, command).Text(text);
                x.VerticalAlignment = VerticalAlignment.Stretch;
                x.HorizontalAlignment = HorizontalAlignment.Right;
            });
        }
    }
}