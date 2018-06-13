using System.Runtime.InteropServices;
using BTS;
using UnityEngine;
using UnityEngine.UI;

public class ClientGameMockup : MonoBehaviour {
	[SerializeField] private Text m_statusText;
	[SerializeField] private Button m_initButton;
	[SerializeField] private Button m_showButton;
	[SerializeField] private Button m_addBees;
	[SerializeField] private Button m_addChest;
	[SerializeField] private Button m_getChest;
	
	
	void Start () {
        m_statusText.text = "Plugin is not inited";
		m_initButton.onClick.AddListener(InitClickHandler);
		m_showButton.onClick.AddListener(ShowClickHandler);
		m_addBees.onClick.AddListener(AddBeesClickHandler);
		m_addChest.onClick.AddListener(AddChestClickHandler);
		m_getChest.onClick.AddListener(GetChestClickHandler);
	}

	private void GetChestClickHandler() {
		Debug.LogError("GetChest is not implemented");
	}

	private void AddChestClickHandler() {
		Debug.LogError("AddChest is not implemented");
	}

	private void AddBeesClickHandler() {
		BTSPlugin.AddBees(10);
	}

	private void ShowClickHandler() {
		BTSPlugin.Show();
	}

	private void InitClickHandler() {
		BTSPlugin.Init("2082fb9894daf21853c3616d9c4b8f78", () => {
			m_statusText.text = "Plugin inited";
		}, true);
	}
}
