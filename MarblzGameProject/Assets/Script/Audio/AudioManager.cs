using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SA_Singleton<AudioManager> {

	[SerializeField] AudioSource audioSource;
	[SerializeField] float FadeInOutSpeed;
	[SerializeField] AudioClip Menu;

	bool autoMode = true;
	bool isFadeIn;
	bool isFadeOut;
	AudioClip destClip;

	float minValue;
	float maxValue;

	[SerializeField] UnityEngine.UI.Slider soundSlider;
	[SerializeField] UnityEngine.UI.Slider musicSlider;
	bool isSubscriber;

	[SerializeField] float volume;

	///---------------
	////Get/Set
	/// --------------

	private float LastPlayedSound {

		get { 
			return PlayerPrefs.GetFloat ("volume sound");
		}
		set { 
			PlayerPrefs.SetFloat ("volume sound", value);	
		}
	} 

	private float LastPlayedMusic{

		get{ 
			return PlayerPrefs.GetFloat ("volume music");
		}
		set { 
			PlayerPrefs.SetFloat ("volume music", value);
		}
	}

	/////////
	/// Builds-in UNITY functions
	/// 

	void Awake(){


		//PlayerPrefs.DeleteAll ();
		SetTheMusicVolumeSlider (musicSlider);
		SetTheVolumeSlider (soundSlider);
		DontDestroyOnLoad (this.gameObject);
		minValue = 0.01f;

		if (audioSource) {
		
			maxValue = audioSource.volume;
			audioSource.volume = 0f;
			audioSource.clip = Menu;
			audioSource.Play ();
		}

		//isFadeIn = true;

		if (!PlayerPrefs.HasKey ("volume sound")) {
			soundSlider.value = 1f;
			PlayerPrefs.SetFloat ("volume sound", soundSlider.value);
		} 
		else {
			soundSlider.value = LastPlayedSound;
			AudioListener.volume = soundSlider.value;
		}	

		if (!PlayerPrefs.HasKey ("volume music")) {
			musicSlider.value = 1f;
			PlayerPrefs.SetFloat ("volume music", musicSlider.value);
		}
		else {
			musicSlider.value = LastPlayedMusic;
			audioSource.volume = musicSlider.value;
		}
	
	}

	void Update(){

		if (audioSource) {
		
			if (isFadeOut) {
			
			FadeOut ();

				if (audioSource.volume <= minValue) {
					audioSource.clip = destClip;
					audioSource.Play ();
					isFadeOut = false;
					isFadeIn = true;
				}
			}
			if (isFadeIn) {
			FadeIn ();
				if (audioSource.volume >= maxValue) {
			
					isFadeIn = false;
				}
			}
		}
	}


	///////////
	/// Public functions
	/// 

	public void SetApplicationVolume(float volume) {

		AudioListener.volume = volume;
		LastPlayedSound = AudioListener.volume;
	}

	public void SetTheVolumeSlider (UnityEngine.UI.Slider slider){

		if (slider != null) {
			slider.value = volume;
			soundSlider = slider;
			soundSlider.onValueChanged.AddListener (UpdateApplicationVolume);
		}	
	}

	public void SetTheMusicVolumeSlider(UnityEngine.UI.Slider mslider){

		if (mslider != null) {
			mslider.value = volume;
			musicSlider = mslider;
			musicSlider.onValueChanged.AddListener (UpdateMusicVolumeSlider);

		}
	}

	public void SetFadeInOutSpeed(float time){
	
		FadeInOutSpeed = time;
	}

	//////////
	/// private functions
	/// 

	private void PlayClipWithTransition(AudioClip clip){
	
		if (audioSource) {
			destClip = clip;
			isFadeOut = true;
			isFadeIn = false;
		}
	}

	void UpdateApplicationVolume(float volume){
		if (soundSlider && soundSlider.gameObject.activeSelf) {
			SetApplicationVolume (soundSlider.value);
			volume = soundSlider.value;
		}
	}

	void UpdateMusicVolumeSlider(float volume) {

		if (musicSlider && musicSlider.gameObject.activeSelf) {
			audioSource.volume = musicSlider.value;
			LastPlayedMusic = audioSource.volume;
		}
	}

	void FadeIn(){
		if (audioSource)
			audioSource.volume = Mathf.Lerp (audioSource.volume, maxValue + 0.1f * maxValue, Time.unscaledDeltaTime * FadeInOutSpeed);
	}

	void FadeOut(){
		if (audioSource)
			audioSource.volume = Mathf.Lerp (audioSource.volume, 0, Time.unscaledDeltaTime * FadeInOutSpeed);
	}

}
