using UnityEngine;

public class MoveTitle : MonoBehaviour {

    private bool goDown;
    public int distance;
    public float step;

    private Vector2 initialPos;

	// Use this for initialization
	void Start () {
        goDown = false;

        initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(goDown)
        {
            //Then the title must go up

            if (transform.position.y < initialPos.y + distance)
            {
                Vector2 pos = transform.position;
                pos.y += step;
                transform.position = pos;
            }
            else
                goDown = false;

        } else
        {
            //Then the title must go down

            if (transform.position.y > initialPos.y - distance)
            {
                Vector2 pos = transform.position;
                pos.y -= step;
                transform.position = pos;
            }
            else
                goDown = true;
        }

	}
}
