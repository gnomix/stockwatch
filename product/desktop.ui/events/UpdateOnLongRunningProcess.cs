
using desktop.ui.eventing;

namespace desktop.ui.events
{
    public class UpdateOnLongRunningProcess : Event
    {
        public string message { get; set; }
        public int percent_complete { get; set; }
    }
}