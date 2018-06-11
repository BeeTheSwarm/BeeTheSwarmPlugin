using System;
using UnityEngine;
using System.Collections;

public interface IService: IInjectTarget {
    event Action<BTS_Error> OnErrorReceived;
    event Action OnSuccessFinish;
}
