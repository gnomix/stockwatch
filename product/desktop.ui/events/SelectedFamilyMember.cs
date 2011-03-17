using System;
using solidware.financials.infrastructure.eventing;

namespace solidware.financials.windows.ui.events
{
    public class SelectedFamilyMember : Event
    {
        public Guid id { get; set; }
    }
}