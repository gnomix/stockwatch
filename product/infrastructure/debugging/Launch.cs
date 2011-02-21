using System.Diagnostics;

namespace infrastructure.debugging
{
    static public class Launch
    {
        static public void the_debugger()
        {
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
            else Debugger.Launch();
#endif
        }
    }
}