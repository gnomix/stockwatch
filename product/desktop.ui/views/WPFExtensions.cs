using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using solidware.financials.windows.ui.views.icons;

namespace solidware.financials.windows.ui.views
{
    static public class WPFExtensions
    {
        static public T Add<T>(this Panel panel, Action<T> action) where T : UIElement, new()
        {
            var element = panel.Add<T>();
            action(element);
            return element;
        }

        static public T Add<T>(this Panel panel) where T : UIElement, new()
        {
            var t = new T();
            panel.Children.Add(t);
            return t;
        }

        static public StackPanel Horizontal(this StackPanel panel)
        {
            panel.Orientation = Orientation.Horizontal;
            return panel;
        }

        static public ButtonExpression ToIconButton(this ButtonBase button, UIIcon icon, ICommand command)
        {
            button.Command = command;
            return button.ToIconButton(icon);
        }

        static public ButtonExpression ToIconButton(this ButtonBase button, UIIcon icon)
        {
            button.ClickMode = ClickMode.Release;
            var image = button.Content as Image;
            if (image == null)
            {
                image = new Image();
                button.Content = image;
                image.Width = 25;
                image.Height = 25;
            }
            image.apply_icon(icon);
            return new ButtonExpression(button);
        }

        static public RoutedEventHandler ToRoutedHandler(this Action action)
        {
            return (s, e) => action();
        }
    }
}