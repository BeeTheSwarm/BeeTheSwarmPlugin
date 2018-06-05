using BTS;
using UnityEngine;

public class ClientGameMockup : MonoBehaviour {
    [SerializeField]
    private GameObject m_splashScreen;
    private bool m_inited;
    void Start () {
        BTSPlugin.Init("2082fb9894daf21853c3616d9c4b8f78", () => {
            m_inited = true;
            m_splashScreen.SetActive(false);
            BTSPlugin.Show();
        });
	}
	    
}
