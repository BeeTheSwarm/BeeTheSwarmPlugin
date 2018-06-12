using System;

namespace BTS {
    public interface ILoadInitDataService: IService {
        bool Loaded { get; }
        event Action OnLoad;
        void Execute(Action callback);
    }
}