using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace desktop.ui.views
{
    public class ImageButton : System.Windows.Controls.Button
    {
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageButton));

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string),typeof (ImageButton));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set{SetValue(ImageSourceProperty, value);}
        }

        public string Label
        {
            get { return (string) GetValue(LabelProperty); }
            set{ SetValue(LabelProperty, value);}
        }

        private void Configure()
        {
            var binding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ImageButton), 1),
                Path = new PropertyPath("IsEnabled")
            };
            var dataTrigger = new DataTrigger { Binding = binding, Value = false, Setters = { new Setter(OpacityProperty, 0.25) } };
            var stackPanel = new StackPanel();
            
            stackPanel.Children.Add(new Image {Source = ImageSource, Style = new Style { Triggers = { dataTrigger } }, Height = 25, Margin = new Thickness(0)});
            var label = new Label
                              {
                                  HorizontalAlignment = HorizontalAlignment.Center,
                                  HorizontalContentAlignment = HorizontalAlignment.Left,
                                  Margin = new Thickness(0),
                                  Padding = new Thickness(0),
                              };
            if (!string.IsNullOrEmpty(Label))
                label.Content = new AccessText{Text = Label, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0)};

            stackPanel.Children.Add(label);
            Content = stackPanel;
        }

        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);
            Configure();
            SetValue(ToolTipService.ShowOnDisabledProperty, true);
        }
    }
}