using System;
using gorilla.utility;
using solidware.financials.infrastructure.eventing;

namespace solidware.financials.messages
{
    public class AddIncomeCommandMessage : ValueType<AddIncomeCommandMessage>, Event
    {
        public Guid PersonId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class IncomeMessage : ValueType<IncomeMessage>, Announcement
    {
        public Guid PersonId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return "You got paid {0:C}!".format(Amount);
        }

        public void AnnounceUsing(Announcer announcer)
        {
        }
    }
}