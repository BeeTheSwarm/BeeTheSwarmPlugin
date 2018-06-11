namespace BTS {
    public interface IConfirmResetPassword: IService {
        void Execute(string login, int code, string password, string confirmPassword);
    }
}