using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {
    public interface IInviteFriendsPhoneService : IService {
        void Execute(List<string> phones);
    }
}
