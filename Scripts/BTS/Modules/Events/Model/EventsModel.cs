using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS
{
    public class EventsModel : IEventsModel
    {
        private List<EventModel> m_events;

        public List<EventModel> Events {
            get {
                return m_events;
            }
        }
        
        public EventsModel()
        {
        }
        
        public void SetEvents(List<EventModel> events) {
            m_events = events;
        }
    }
}