using System.Drawing;
using System.IO;
using System.Windows.Media;

namespace solidware.financials.windows.ui.views.icons
{
    public class UIIcon
    {
        static public readonly UIIcon Null = new NullUIIcon();
        static public readonly UIIcon Category = new UIIcon("category.png");
        static public readonly UIIcon Comment = new UIIcon("comment.png");
        static public readonly UIIcon Delete = new UIIcon("delete.png");
        static public readonly UIIcon Edit = new UIIcon("edit.png");
        static public readonly UIIcon Help = new UIIcon("help.ico");
        static public readonly UIIcon Plus = new UIIcon("plus.png");
        static public readonly UIIcon Refresh = new UIIcon("refresh.png");
        static public readonly UIIcon Running = new UIIcon("running.gif");
        static public readonly UIIcon Success = new UIIcon("success.png");
        static public readonly UIIcon Application = new UIIcon("mokhan.ico");
        static public readonly UIIcon Close = new UIIcon("close.png");
        static public readonly UIIcon Info = new UIIcon("info.png");

        UIIcon(string path)
        {
            this.path = path;
        }

        public virtual Stream ImageStream()
        {
            return IconMarker.GetImage(path);
        }
        
        public virtual Icon AsIcon()
        {
            return new Icon(ImageStream());
        }

        public virtual ImageSource BitmapFrame()
        {
            return System.Windows.Media.Imaging.BitmapFrame.Create(ImageStream());
        }

        public override string ToString()
        {
            return string.Format("Path: {0}", path);
        }

        string path;

        class NullUIIcon : UIIcon
        {
            public NullUIIcon() : base(string.Empty) {}

            public override Stream ImageStream()
            {
                return null;
            }

            public override ImageSource BitmapFrame()
            {
                return null;
            }
        }
    }
}