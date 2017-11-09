using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarnBeesInterstitialController : MonoBehaviour {

	private static EarnBeesInterstitialController _instance;

	public enum EarnBeesInterstitialShowType{

		Table,
		DailyMaximum,
		BeesEarned
	}

	[SerializeField] GameObject _earnBeesTable;
	[SerializeField] GameObject _beesEarnedGo;
	[SerializeField] GameObject _dailyBeesMaximumGo;
	[SerializeField] Text _beesEarnedText;

	[SerializeField] List<Image> _beesOnTable;
	[SerializeField] Animator _animator;

	bool _isShowing = false;

	private int _beesEarnedCount = 0;

	List<KeyValuePair<EarnBeesInterstitialShowType, int>> _queue = new List<KeyValuePair<EarnBeesInterstitialShowType, int>> ();

	bool _usePause = false;

	public static EarnBeesInterstitialController Instance{

		get{ 
			return _instance;
		}
	}

	void Awake (){
		DontDestroyOnLoad (this.gameObject);
		_instance = this;
	}

	public void Show (EarnBeesInterstitialShowType showType, int bees = 0){
		if (_isShowing == true)
			return;

		_isShowing = true;

		_animator.SetTrigger ("FadeIn");

		switch (showType) {

		case EarnBeesInterstitialShowType.BeesEarned:
			_earnBeesTable.SetActive (true);
			_dailyBeesMaximumGo.SetActive (false);
			_beesEarnedGo.SetActive (true);

			_beesEarnedText.text = bees.ToString ();

			break;


		case EarnBeesInterstitialShowType.DailyMaximum:
			_earnBeesTable.SetActive (true);
			_dailyBeesMaximumGo.SetActive (true);
			_beesEarnedGo.SetActive (false);

			break;


		case EarnBeesInterstitialShowType.Table:

			_earnBeesTable.SetActive (true);
			_dailyBeesMaximumGo.SetActive (false);
			_beesEarnedGo.SetActive (false);

			break;
		}

	}


	public void FadeOut(){
	
		if (_isShowing == false)
			return;

		_isShowing = false;
		_animator.SetTrigger ("FadeOut");


	}

	public void OnTableExitButtonClick(){
		FadeOut ();
	}

	public void OnBeesEarnedExitButtonClick(){

		_beesEarnedGo.SetActive (false);
	}

	public void OnDailyBeesMaximumExitButtonClick(){
	
		_dailyBeesMaximumGo.SetActive (false);
	}

	public void ResetBees(){

		_beesEarnedCount = 0;
		StartCoroutine (UpdateBeesCoroutine ());
	}

	public void UpdateBees(){
	
		//here will be code 
		StartCoroutine(UpdateBeesCoroutine());
	}



	IEnumerator UpdateBeesCoroutine(){
		for (int b = 0; b < _beesOnTable.Count; b++) {
			if (b < _beesEarnedCount) {
				_beesOnTable [b].color = Color.yellow;
				continue;
			}
			_beesOnTable [b].color = Color.white;
		}
		yield return new WaitForEndOfFrame();
	}
}
