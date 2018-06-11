using UnityEngine;
using System.Collections;

public class InviteFriendViewModel
{
    public readonly Observable<string> ReferalCode = new Observable<string>();
    public readonly Observable<int> RewardForInvite = new Observable<int>();
}
