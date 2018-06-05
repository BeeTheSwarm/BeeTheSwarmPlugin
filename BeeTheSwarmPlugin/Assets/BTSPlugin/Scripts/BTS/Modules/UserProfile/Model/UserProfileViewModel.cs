using UnityEngine;
using System.Collections;

namespace BTS
{
    public class UserProfileViewModel
    {
        public readonly Observable<int> Rank = new Observable<int>();
        public readonly Observable<float> Impact = new Observable<float>();
        public Observable<Sprite> Avatar = new Observable<Sprite>();
        public Observable<int> NotificationsCount = new Observable<int>();
        public Observable<int> RequestsCount = new Observable<int>();
    }
}
