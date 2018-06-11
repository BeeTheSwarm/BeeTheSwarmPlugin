using UnityEngine;
using System.Collections;

namespace BTS {

    internal interface ISendPushIdService: IService {
        void Execute(string userId);
    } 
}
