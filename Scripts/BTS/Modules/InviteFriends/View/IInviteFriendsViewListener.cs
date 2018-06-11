using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInviteFriendsViewListener : IViewEventListener
{
    void InviteFriendsByMail();
    void CopyCode();
    void InviteFriendsByPhone();
}
