using UnityEngine;
using UnityEditor;
using System;

public abstract class BaseService : IService
{
    
    public virtual void PostInject() { 
    }

    public event Action<BTS_Error> OnErrorReceived = delegate {  };
    public event Action OnSuccessFinish = delegate {  };
    protected void FireSuccessFinishEvent() {
        OnSuccessFinish.Invoke();
    }
    
    protected void FireErrorEvent(BTS_Error error) {
        OnErrorReceived.Invoke(error);
    }

}