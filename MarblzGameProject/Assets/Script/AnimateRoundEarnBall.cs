
using UnityEngine;

public class AnimateRoundEarnBall : MonoBehaviour {

    private bool isBig;
    public float step;

	// Use this for initialization
	void Start () {
        isBig = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!isBig)
        {
            if (transform.localScale.x < 1.5f)
            {
                transform.localScale = new Vector2(transform.localScale.x + step, transform.localScale.y + step);// + step, transform.localScale.z);

                if (transform.localScale.x >= 1.5f)
                    isBig = true;
            }
        } else if(isBig)
        {
            if (transform.localScale.x > 1.2f)
            {
                transform.localScale = new Vector2(transform.localScale.x - step, transform.localScale.y - step);// - step, transform.localScale.z);

                if (transform.localScale.x <= 1.2f)
                    isBig = false;
            }
        }

	}
}
