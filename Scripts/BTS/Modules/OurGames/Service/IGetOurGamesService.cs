using System;
using System.Collections.Generic;

namespace BTS {
    public interface IGetOurGamesService: IService {
        void Execute(Action<List<GameModel>> callback);
    }
}