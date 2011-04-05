using solidware.financials.infrastructure.eventing;

namespace solidware.financials.messages
{
    public interface Announcement : Event
    {
        void AnnounceUsing(Announcer announcer);
    }

    public interface Announcer
    {
        void Say(string message);
    }
}