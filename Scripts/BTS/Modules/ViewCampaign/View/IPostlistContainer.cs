using UnityEngine;
using System.Collections;
namespace BTS {
    public interface IPostlistContainer:IView {
        void SetViewModel(ObservableList<PostViewModel> viewModel);
    }
}
