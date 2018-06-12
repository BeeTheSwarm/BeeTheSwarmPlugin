using System;

namespace BTS {
    public interface IStartupService: IService {
        void Execute(Action callback);
    }
}