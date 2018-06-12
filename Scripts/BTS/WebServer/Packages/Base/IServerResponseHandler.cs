using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IServerResponseHandler<T>
{
    void OnTimeout();
    void OnSuccess(T data);
    void OnError(BTS_Error error);
}
