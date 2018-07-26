
using System;

public interface IPluginContentView : IControlledView {
    void Setup(bool animationEnabled);
    event Action OnHideStarted ;
    event Action OnHideFinished;
    event Action OnShown ;
}
