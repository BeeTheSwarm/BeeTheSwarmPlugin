using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour {

    [Header("Sounds")]
    public AudioSource BallSound;
    public AudioSource EarnAndCoinSound;

    [Header("Ball")]
    public GameObject BallPrefab;
    public float launchForce;
    [HideInInspector]
    public Color ballColor;

    [Header("Caracs")]
    public int numberOfBalls;
    private bool launchBalls;

    [Header("Fast Forward Button")]
    public GameObject FFButton;

    [Space(10)]

    public Text numberOfBallsText;

    private Rigidbody2D ballRigidbody;
    private Vector2 initialPos;
    private Vector2 touchPos;
    private Vector2 dir;
    private Vector2 spawnPos;

    [HideInInspector]
    public GameObject initialBall;

    public bool canLaunch;
    public bool canDropLine;
    private bool lineDropped;

    private bool firstBallSet;

	private bool canWePlay = true;

	void OnSecondMenuConditionChanged(bool condition) {
		Debug.Log ("canWePlay " + canWePlay);
		canWePlay = condition;
	}

	private void Subscribe() {
		GameManager.OnSecondMenuConditionChanged += OnSecondMenuConditionChanged;
	}

	private void UnSubscribe() {
		GameManager.OnSecondMenuConditionChanged -= OnSecondMenuConditionChanged;
	}

    // Use this for initialization
    void Start () {
		Subscribe ();
        //ballRigidbody = this.GetComponent<Rigidbody2D>();

        ballColor = Color.white;

        //First Ball set initially false
        firstBallSet = false;
        canLaunch = true;

        launchBalls = false;
        canDropLine = true;
        lineDropped = false;

        //Hide the Fast Forward Button
        FFButton.SetActive(false);

        //Instantiate the initial Ball
        spawnPos = new Vector2(0, GameObject.FindGameObjectWithTag("Wall Bottom").transform.position.y + 0.65f);

        initialBall = Instantiate(BallPrefab, spawnPos, Quaternion.identity);
        initialBall.GetComponent<SpriteRenderer>().color = ballColor;
        initialBall.transform.parent = this.transform;

        numberOfBallsText.transform.position = new Vector2(spawnPos.x, spawnPos.y + 0.3f);
        numberOfBallsText.text = "x " + numberOfBalls;
    }
	
	// Update is called once per frame
	void Update () {
		if (!canWePlay)
			return;
		
        if (canLaunch)
        {
            if (Input.GetMouseButtonDown(0) && 
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y > initialBall.transform.position.y &&
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y < Camera.main.orthographicSize - 0.65f)
            {
				//Show the FF Button
                FFButton.SetActive(true);
				Debug.Log ("clicks");
                //Get the direction for launching
                touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dir = touchPos - (Vector2)initialBall.transform.position;

                //Disable the initialBall
                initialBall.SetActive(false);

                //Launch the balls
                launchBalls = true;

                //Disable the launch until all the balls have disappeared
                canLaunch = false;
            }

            if (launchBalls)
            {
                StartCoroutine("LaunchBallsCoroutine");
                launchBalls = false;
            }

           
        } else if(!canLaunch)
        {
			//If we have only 1 ball left AKA the first ball, activate the launch
            if (transform.childCount == 1)
            {
                canLaunch = true;

                //Set the position and the value of the numberOfBallsText
                numberOfBallsText.transform.position = new Vector2(initialBall.transform.position.x, initialBall.transform.position.y + 0.3f);
                numberOfBallsText.text = "x " + numberOfBalls;

                FFButton.SetActive(false);
                Time.timeScale = 1;

                if (!lineDropped)
                {
                    canDropLine = true;
                    lineDropped = true;

                }
            }
        }
	}

    IEnumerator LaunchBallsCoroutine()
    {
        int ballsLaunched = numberOfBalls;

        while(ballsLaunched > 0)
        {
            if (ballsLaunched - 1 > 0)
                numberOfBallsText.text = "x " + (ballsLaunched - 1);
            else if (ballsLaunched - 1 == 0)
                numberOfBallsText.text = "";

            GameObject ball = Instantiate(BallPrefab, spawnPos, Quaternion.identity);

            //Change the color of the ball
            ball.GetComponent<SpriteRenderer>().color = ballColor;

            ball.transform.parent = this.gameObject.transform;
            ball.GetComponent<Rigidbody2D>().AddForce(dir.normalized * launchForce);

            DisableCollisionsBetweenBalls();

            lineDropped = false;

            ballsLaunched--;

            yield return new WaitForSeconds(0.1f);
        }

        firstBallSet = false;
        yield return null;
    }

    void DisableCollisionsBetweenBalls()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.childCount; j++)
            {
                Physics2D.IgnoreCollision(transform.GetChild(i).GetComponent<Collider2D>(), transform.GetChild(j).GetComponent<Collider2D>());
            }
        }
    }

    public void SetFirstBall(Vector3 FirstBallPosition)
    {
        if (!firstBallSet)
        {
            initialBall.transform.position = new Vector2(FirstBallPosition.x, 
                GameObject.FindGameObjectWithTag("Wall Bottom").transform.position.y + 0.65f);
            spawnPos = initialBall.transform.position;


            //Enable the initialBall
            initialBall.SetActive(true);

            firstBallSet = true;

        }
    }

    public void PlayBallSound()
    {
        BallSound.Play();
    }

    public void PlayEarnCoinSound()
    {
        EarnAndCoinSound.Play();
    }

    public void ResetSettings()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        //First Ball set initially false
        firstBallSet = false;
        canLaunch = true;

        launchBalls = false;
        canDropLine = true;
        lineDropped = false;

        //Instantiate the initial Ball
        spawnPos = new Vector2(0, GameObject.FindGameObjectWithTag("Wall Bottom").transform.position.y + 0.65f);
        initialBall = Instantiate(BallPrefab, spawnPos, Quaternion.identity);
        initialBall.GetComponent<SpriteRenderer>().color = ballColor;

        initialBall.transform.parent = this.transform;

        numberOfBalls = 1;

        numberOfBallsText.transform.position = new Vector2(spawnPos.x, spawnPos.y + 0.3f);
        numberOfBallsText.text = "x " + numberOfBalls;
    }
}
