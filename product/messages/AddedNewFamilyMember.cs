using System;
using desktop.ui.eventing;

namespace desktop.ui.presenters
{
    public class AddedNewFamilyMember : Event
    {
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}