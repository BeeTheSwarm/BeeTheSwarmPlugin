using System;
using System.Collections.Generic;

namespace BTS {
    public interface IGetEventsService:IService {
        void Execute(Action<List<EventModel>> callback);
    }
}