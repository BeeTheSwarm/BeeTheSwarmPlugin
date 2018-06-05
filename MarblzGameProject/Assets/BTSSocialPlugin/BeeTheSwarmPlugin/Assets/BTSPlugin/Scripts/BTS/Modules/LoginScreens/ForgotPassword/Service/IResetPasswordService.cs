using System;

namespace BTS {
    public interface IResetPasswordService: IService {
        void Execute(string login, Action<bool> callback);
    }
}