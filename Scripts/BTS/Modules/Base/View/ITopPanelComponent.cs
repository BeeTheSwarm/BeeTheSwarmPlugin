using UnityEngine;
using System.Collections;

public interface ITopPanelComponent : IView {
    void SetViewModel(TopPanelViewModel viewModel);
}
