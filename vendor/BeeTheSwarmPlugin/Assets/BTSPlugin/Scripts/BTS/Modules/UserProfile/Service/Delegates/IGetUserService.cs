using System;

namespace BTS {
    public interface IGetUserService: IService {
        void Execute(Action<UserModel> callback);
    }
}