using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using gorilla.utility;
using solidware.financials.windows.ui.views.icons;

namespace solidware.financials.windows.ui.views
{
    static public class MenuItemExtensions
    {
        static public MenuItem add(this MenuItem item, string header, Action action)
        {
            var menu_item = new MenuItem {Header = header, Command = new SimpleCommand(action)};
            item.Items.Add(menu_item);
            return menu_item;
        }

        static public MenuItem parent(this MenuItem item)
        {
            return item.Parent.downcast_to<MenuItem>();
        }

        static public MenuItem apply_icon(this MenuItem item, UIIcon icon)
        {
            var image = new Image();
            image.Width = 16;
            image.Height = 16;
            image.apply_icon(icon);
            item.Icon = image;
            return item;
        }

        static public void apply_icon(this Image image, UIIcon icon)
        {
            image.Tag = icon;

            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = icon.ImageStream();
            source.EndInit();
            image.Source = source;
        }
    }
}