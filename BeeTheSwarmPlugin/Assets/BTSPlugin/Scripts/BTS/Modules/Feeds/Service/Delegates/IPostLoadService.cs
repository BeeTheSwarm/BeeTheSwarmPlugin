using System;
using System.Collections.Generic;

namespace BTS {
    public interface IPostLoadService: IService {
        void Execute(int offset, int limit, Action<List<PostModel>> callback);
    }
}