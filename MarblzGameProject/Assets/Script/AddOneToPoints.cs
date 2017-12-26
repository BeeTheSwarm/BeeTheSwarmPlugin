using UnityEngine;

public class AddOneToPoints : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int s = GameObject.Find("GameManager").GetComponent<GameManager>().coins;
        s++;
        GameObject.Find("GameManager").GetComponent<PlayerPrefsManager>().SaveCoins(s);

        GameObject.Find("GameManager").GetComponent<GameManager>().UpdateScoreText();
        Destroy(this.gameObject);
    }
}
