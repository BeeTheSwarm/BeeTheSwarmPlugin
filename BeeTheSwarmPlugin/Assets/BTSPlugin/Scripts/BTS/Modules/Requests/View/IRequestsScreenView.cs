using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {
    public interface IRequestsScreenView : IControlledView {
        void SetViewModel(RequestsScreenViewModel m_viewModel);
    }
}