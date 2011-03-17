using System;
using solidware.financials.infrastructure.eventing;

namespace solidware.financials.messages
{
    public class AddedNewFamilyMember : Event
    {
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}