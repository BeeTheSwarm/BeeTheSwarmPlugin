using UnityEngine;

public class BallHitBottom : MonoBehaviour {

    private BallControl BC;

    private float refVelocity;
    public float smoothTime;

    private bool canDestroy;
    private bool collided;

	// Use this for initialization
	void Start () {
        BC = transform.GetComponentInParent<BallControl>();

        collided = false;
        canDestroy = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (collided)
        {
            Vector2 pos = transform.position;
            pos.x = Mathf.SmoothDamp(pos.x, BC.initialBall.transform.position.x, ref refVelocity, smoothTime);
            pos.y = BC.initialBall.transform.position.y;
            transform.position = pos;

            if(Mathf.Abs(transform.position.x - BC.initialBall.transform.position.x) < 0.05f)
            {
                collided = false;
                Destroy(this.gameObject);
            }
        }

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.transform.tag == "Wall Bottom")
        {
            //Set a value in the parent BallControl Script
            transform.parent.GetComponent<BallControl>().SetFirstBall(this.transform.position);

            //Set the speed to null
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            collided = true;
        }
        else if (collision.transform.tag == "Box")
        {
            BC.PlayBallSound();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     if(collision.transform.tag == "EarnBall")
        {
            BC.PlayEarnCoinSound();
        } else if (collision.transform.tag == "Coin")
        {
            BC.PlayEarnCoinSound();
        }
    }
}
