using System.Collections.Generic;

namespace BTS
{
    public interface IInviteFriendsEmailService: IService
    {
        void Execute(List<string> mails);
    }
}