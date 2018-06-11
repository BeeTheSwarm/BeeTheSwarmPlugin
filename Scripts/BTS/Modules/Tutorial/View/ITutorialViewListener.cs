using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITutorialViewListener : IViewEventListener {
	
	void OnBackPressed();
	void OnShowCampaign();
	void OnStartGame();
	void OnPassCampaignTutorial();
	void OnInviteClick();
	void OnPlayGamesClick();
	void GetReward(int gameScore);
}
