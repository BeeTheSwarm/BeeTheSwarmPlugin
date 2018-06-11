using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {
    internal interface IAddPostView : IControlledView {
        void SetViewModel(IAddPostViewModel m_viewModel);
        void ShowError(string v);
        void Clear();
    }
}