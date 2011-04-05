using gorilla.utility;

namespace solidware.financials.messages
{
    public class StartWatchingSymbol : ValueType<StartWatchingSymbol>, Announcement
    {
        public string Symbol { get; set; }

        public override string ToString()
        {
            return "I will start watching {0}".format(Symbol);
        }

        public void AnnounceUsing(Announcer announcer)
        {
            announcer.Say(ToString());
        }
    }
}