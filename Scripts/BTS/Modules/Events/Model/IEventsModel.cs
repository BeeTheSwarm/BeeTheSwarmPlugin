using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS
{
    public interface IEventsModel : IModel
    {
        List<EventModel> Events {get; }
        void SetEvents(List<EventModel> events);
    }
}