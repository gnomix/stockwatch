using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using gorilla.utility;

namespace solidware.financials.windows.ui.views
{
    public class ButtonExpression
    {
        readonly ButtonBase button;

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

        public ButtonExpression SmallerImages()
        {
            var image = button.Content as Image ?? button.Content.downcast_to<StackPanel>().Children[0].downcast_to<Image>();
            image.Width = image.Height = 16;

            return this;
        }

        public ButtonExpression DoesNotAcceptTab()
        {
            button.TabIndex = int.MaxValue;
            return this;
        }
    }
}