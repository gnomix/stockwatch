using System;
using gorilla.utility;
using solidware.financials.infrastructure.eventing;

namespace solidware.financials.messages
{
    public class AddedNewFamilyMember : Event
    {
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

        public override string ToString()
        {
            return "Welcome to the family {0} {1}".format(first_name, last_name);
        }
    }
}