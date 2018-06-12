using System;

namespace BTS  {
    public interface IAddChestService: IService {
        void Execute(Action<int> callback);
    }
}