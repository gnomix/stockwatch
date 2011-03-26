using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.views.icons;

namespace solidware.financials.windows.ui.views
{
    public partial class SelectedFamilyMemberRegion : View<SelectedFamilyMemberPresenter>
    {
        public SelectedFamilyMemberRegion()
        {
            InitializeComponent();
        }

        public void AddCommand(string text, Action action)
        {
            DockPanel.Add<Button>(x =>
            {
                x.VerticalAlignment = VerticalAlignment.Stretch;
                x.Content = text;
                x.Click += action.ToRoutedHandler();
                x.Height = 30;
                x.Margin = new Thickness(5, 0, 5, 0);
                x.HorizontalAlignment = HorizontalAlignment.Right;
            });
        }

        public void AddCommand(string text, Action action, UIIcon icon)
        {
            DockPanel.Add<Button>(x =>
            {
                x.ToIconButton(icon, new SimpleCommand(action)).Text(text);
                x.VerticalAlignment = VerticalAlignment.Stretch;
                x.Height = 30;
                x.Margin = new Thickness(5, 0, 5, 0);
                x.HorizontalAlignment = HorizontalAlignment.Right;
            });
        }

        public void AddCommand(string text, ICommand command, UIIcon icon)
        {
            DockPanel.Add<Button>(x =>
            {
                x.ToIconButton(icon, command).Text(text);
                x.VerticalAlignment = VerticalAlignment.Stretch;
                x.Height = 30;
                x.Margin = new Thickness(5, 0, 5, 0);
                x.HorizontalAlignment = HorizontalAlignment.Right;
            });
        }
    }
}