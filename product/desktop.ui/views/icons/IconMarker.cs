using System.IO;
using System.Reflection;

namespace solidware.financials.windows.ui.views.icons
{
    public class IconMarker
    {
        private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        public static Stream GetImage(string path)
        {
            return _assembly.GetManifestResourceStream(typeof (IconMarker), path);
        }
    }
}