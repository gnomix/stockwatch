using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using desktop.ui.eventing;
using desktop.ui.events;
using gorilla.utility;

namespace desktop.ui.presenters
{
    public class SelectedFamilyMemberPresenter : Observable<SelectedFamilyMemberPresenter>, Presenter, EventSubscriber<AddedNewFamilyMember>
    {
        PersonDetails selected_member;
        EventAggregator event_aggregator;
        Mapper mapper;
        ServiceBus bus;

        public SelectedFamilyMemberPresenter(EventAggregator event_aggregator, Mapper mapper, ServiceBus bus)
        {
            this.bus = bus;
            this.mapper = mapper;
            this.event_aggregator = event_aggregator;
            family_members = new ObservableCollection<PersonDetails>();
        }

        public ICollection<PersonDetails> family_members { get; set; }

        public PersonDetails SelectedMember
        {
            get { return selected_member; }
            set
            {
                selected_member = value;
                update(x => x.SelectedMember);
                event_aggregator.publish(new SelectedFamilyMember {id = value.id});
            }
        }

        public void present()
        {
            bus.publish<FindAllFamily>();
        }

        public void notify(AddedNewFamilyMember message)
        {
            family_members.Add(mapper.map_from<AddedNewFamilyMember, PersonDetails>(message));
            update(x => x.family_members);
        }
    }

    public class FindAllFamily
    {
    }

    public class AddedNewFamilyMember : Event
    {
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}