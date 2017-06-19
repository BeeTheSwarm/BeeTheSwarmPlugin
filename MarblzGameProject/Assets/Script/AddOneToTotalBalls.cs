using UnityEngine;

public class AddOneToTotalBalls : MonoBehaviour {

    private bool rigidBodyAdded;

	// Use this for initialization
	void Start () {
        rigidBodyAdded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(rigidBodyAdded)
        {
            if (transform.position.y < -Camera.main.orthographicSize)
                Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("BallSpawner").GetComponent<BallControl>().numberOfBalls++;
        //Destroy(this.gameObject);

        if (!rigidBodyAdded)
        {
            this.GetComponent<Collider2D>().enabled = false;

            this.gameObject.AddComponent<Rigidbody2D>();
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);

            Destroy(transform.GetChild(0).gameObject);

            rigidBodyAdded = true;
        }
    }
}
