using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
	public class TutorialController : BaseScreenController<ITutorialView>, ITutorialViewListener, ITutorialController {

		[Inject] private ITutorialModel m_tutorialModel;
		[Inject] private IPostListControllerDelegate m_postsControllerDelegate;
		[Inject] private IGetPostsService m_postLoaderService;
		[Inject] private IInviteFriendsController m_inviteFriendsController;
		[Inject] private IOurGamesController m_ourGamesController;
		[Inject] private IGetTutorialRewardService m_getTutorialRewardService;
		[Inject] private IGetTutorialStateService m_getTutorialStateService;

		private TutorialViewModel m_viewModel = new TutorialViewModel();
		public override void PostInject() {
			base.PostInject();
			m_postsControllerDelegate.SetView(m_view.GetPostsContainer());
			m_postsControllerDelegate.SetMaxItems(1);
			m_postsControllerDelegate.PostsClickable = false;
			m_postsControllerDelegate.SetItemsSource(m_postLoaderService.Execute);
		}

		protected override void PostSetView() {
			base.PostSetView();
			m_view.SetViewModel(m_viewModel);
		}

		public void OnShowCampaign() {
			m_postsControllerDelegate.Update();
		}

		public void OnStartGame() {
			
		}

		public override void Show() {
			base.Show();
			m_getTutorialStateService.OnSuccessFinish += OnTutorialStateReceived;
			m_getTutorialStateService.Execute();
			m_view.ResetToStartPage();
		}

		private void OnTutorialStateReceived() {
			m_viewModel.TutorialAvailable = m_tutorialModel.IsTutorialAvailable;
			m_getTutorialStateService.OnSuccessFinish -= OnTutorialStateReceived;
		}

		public void OnPassCampaignTutorial() {
			
		}

		public void OnInviteClick() {
			m_inviteFriendsController.Show();
		}

		public void OnPlayGamesClick() {
			m_ourGamesController.Show();
		}

		public void GetReward(int gameScore) {
			m_getTutorialRewardService.OnSuccessFinish += GetRewardHandler;
			m_getTutorialRewardService.Execute(gameScore);
		}

		private void GetRewardHandler() {
			m_viewModel.TutorialAvailable = false;
			m_viewModel.NeedToDonate = true;
			m_view.HideGame();
		}
	}
}
