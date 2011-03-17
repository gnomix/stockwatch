using solidware.financials.infrastructure.eventing;

namespace solidware.financials.windows.ui.events
{
    public class UpdateOnLongRunningProcess : Event
    {
        public string message { get; set; }
        public int percent_complete { get; set; }
    }
}