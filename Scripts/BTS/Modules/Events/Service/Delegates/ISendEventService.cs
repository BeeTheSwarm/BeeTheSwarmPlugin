using System;

namespace BTS {
    public interface ISendEventService: IService {
        void Execute(string levelId, int score, Action<int> callback);
    }
}