using System;
using System.Collections.Generic;

namespace BTS  {
    public interface IOpenChestService: IService {
        void Execute(Action<List<ChestReward>, int> callback);
    }
}