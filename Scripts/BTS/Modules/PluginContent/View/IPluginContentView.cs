
using System;

public interface IPluginContentView : IControlledView {
    void Setup(bool animationEnabled, bool dragEnabled);
    event Action OnHideStarted ;
    event Action OnHideFinished;
    event Action OnShown ;
}
