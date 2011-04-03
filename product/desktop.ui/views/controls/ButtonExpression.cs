using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using gorilla.utility;

namespace solidware.financials.windows.ui.views.controls
{
    public class ButtonExpression
    {
        ButtonBase button;

        public ButtonExpression(ButtonBase button)
        {
            this.button = button;
        }

        public void Text(string text)
        {
            var panel = new StackPanel
                        {
                            Orientation = Orientation.Horizontal
                        };
            var image = button.Content.downcast_to<Image>();
            button.Content = panel;
            panel.Children.Add(image);
            panel.Children.Add(new Label
                               {
                                   Content = text
                               });
        }
    }
}