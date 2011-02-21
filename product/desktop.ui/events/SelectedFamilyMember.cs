using System;
using desktop.ui.eventing;

namespace desktop.ui.events
{
    public class SelectedFamilyMember : Event
    {
        public Guid id { get; set; }
    }
}