using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
	public interface ITutorialView : IControlledView {
		void SetViewModel(TutorialViewModel m_viewModel);
		IPostlistContainer GetPostsContainer();
		void ResetToStartPage();
		void HideGame();
	}
}
