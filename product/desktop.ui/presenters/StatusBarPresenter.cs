using System.Threading;
using solidware.financials.infrastructure.eventing;
using solidware.financials.windows.ui.events;

namespace solidware.financials.windows.ui.presenters
{
    public class StatusBarPresenter : Observable<StatusBarPresenter>, Presenter, EventSubscriber<UpdateOnLongRunningProcess>
    {
        public string progress_message { get; set; }
        public bool is_progress_bar_on { get; set; }
        public string username { get; set; }
        public string title { get; set; }

        public void present()
        {
            username = Thread.CurrentPrincipal.Identity.Name;
            title = "N/A";
        }

        public void notify(UpdateOnLongRunningProcess message)
        {
            progress_message = message.message;
            is_progress_bar_on = message.percent_complete < 100;
            update(x => x.progress_message, x => x.is_progress_bar_on);
        }
    }
}