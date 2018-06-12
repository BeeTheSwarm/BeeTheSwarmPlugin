using System;

namespace BTS {
    public interface IGetChestService: IService {
        void Execute(Action<int> callback);
    }
}