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
    private Vector2 m_ballLaunchDirection;
    private Vector2 spawnPos;

    [Header("Heading hint")]
    public HeadingHint HeadingArrow;

    [HideInInspector]
    public GameObject initialBall;

    public bool canLaunch;
    public bool canDropLine;
    private bool lineDropped;

    private bool firstBallSet;

    private bool canWePlay = true;


    // for dragging
    private float force;
    private bool mousePressed;
    private Vector3 mouseStartPosition;
    private Vector3 mouseEndPosition;
    private Vector3 heading;
    public float distance;


    void OnSecondMenuConditionChanged(bool condition) {
        Debug.Log("canWePlay " + canWePlay);
        canWePlay = condition;
    }

    private void Subscribe() {
        GameManager.OnSecondMenuConditionChanged += OnSecondMenuConditionChanged;
    }

    private void UnSubscribe() {
        GameManager.OnSecondMenuConditionChanged -= OnSecondMenuConditionChanged;
    }

    // Use this for initialization
    void Start() {
        Subscribe();
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
        AlignHint();
        HeadingArrow.transform.position = initialBall.transform.position;
        HeadingArrow.gameObject.SetActive(false);
        numberOfBallsText.transform.position = new Vector2(spawnPos.x, spawnPos.y + 0.3f);
        numberOfBallsText.text = "x " + numberOfBalls;
    }

    private void AlignHint() {
        var position = initialBall.transform.position;
        position.z = -1;
        HeadingArrow.transform.position = position;

    }

    // Update is called once per frame
    void Update() {
        if (!canWePlay)
            return;

        if (canLaunch) {

            if (Input.GetMouseButtonDown(0)) {

                mousePressed = true;
                AlignHint();
                HeadingArrow.gameObject.SetActive(true);
                Ray vRayStart = Camera.main.ScreenPointToRay(Input.mousePosition);
                mouseStartPosition = vRayStart.origin;
            }

            if (mousePressed) {
                Ray vRayEnd = Camera.main.ScreenPointToRay(Input.mousePosition);
                mouseEndPosition = vRayEnd.origin;
                heading = mouseEndPosition - initialBall.transform.position;
                distance = heading.magnitude;
                m_ballLaunchDirection = heading;
                m_ballLaunchDirection.Normalize();
                if (Input.GetMouseButtonUp(0))
                {
                    HeadingArrow.gameObject.SetActive(false);
                    mousePressed = false;
                    if (mouseStartPosition == mouseEndPosition)
                        return;
                   /* if (mouseEndPosition.y > mouseStartPosition.y)
                    {
                        return;
                    }*/
                    if (mouseEndPosition.y < initialBall.transform.position.y) { 

                    return;
                }
                    FFButton.SetActive(true);
                    initialBall.SetActive(false);
                    launchBalls = true;
                    canLaunch = false;
                }
                else {
                    HeadingArrow.SetHeading(Mathf.Atan(-m_ballLaunchDirection.x / m_ballLaunchDirection.y) * Mathf.Rad2Deg);
                }
            }

            if (launchBalls) {
                StartCoroutine("LaunchBallsCoroutine");
                launchBalls = false;
            }


        }
        else if (!canLaunch) {
            //If we have only 1 ball left AKA the first ball, activate the launch
            if (transform.childCount == 1) {
                canLaunch = true;

                //Set the position and the value of the numberOfBallsText
                numberOfBallsText.transform.position = new Vector2(initialBall.transform.position.x, initialBall.transform.position.y + 0.3f);
                numberOfBallsText.text = "x " + numberOfBalls;

                FFButton.SetActive(false);
                Time.timeScale = 1;

                if (!lineDropped) {
                    canDropLine = true;
                    lineDropped = true;

                }
            }
        }
    }

    IEnumerator LaunchBallsCoroutine() {
        int ballsLaunched = numberOfBalls;

        while (ballsLaunched > 0) {
            if (ballsLaunched - 1 > 0)
                numberOfBallsText.text = "x " + (ballsLaunched - 1);
            else if (ballsLaunched - 1 == 0)
                numberOfBallsText.text = "";

            GameObject ball = Instantiate(BallPrefab, spawnPos, Quaternion.identity);

            //Change the color of the ball
            ball.GetComponent<SpriteRenderer>().color = ballColor;

            ball.transform.parent = this.gameObject.transform;
            ball.GetComponent<Rigidbody2D>().AddForce(m_ballLaunchDirection.normalized * launchForce);

            DisableCollisionsBetweenBalls();

            lineDropped = false;

            ballsLaunched--;

            yield return new WaitForSeconds(0.1f);
        }

        firstBallSet = false;
        yield return null;
    }

    void DisableCollisionsBetweenBalls() {
        for (int i = 0; i < transform.childCount; i++) {
            for (int j = 0; j < transform.childCount; j++) {
                Physics2D.IgnoreCollision(transform.GetChild(i).GetComponent<Collider2D>(), transform.GetChild(j).GetComponent<Collider2D>());
            }
        }
    }

    public void SetFirstBall(Vector3 FirstBallPosition) {
        if (!firstBallSet) {
            initialBall.transform.position = new Vector2(FirstBallPosition.x,
                GameObject.FindGameObjectWithTag("Wall Bottom").transform.position.y + 0.65f);
            spawnPos = initialBall.transform.position;
            AlignHint();

            //Enable the initialBall
            initialBall.SetActive(true);

            firstBallSet = true;

        }
    }

    public void PlayBallSound() {
        BallSound.Play();
    }

    public void PlayEarnCoinSound() {
        EarnAndCoinSound.Play();
    }

    public void ResetSettings() {
        for (int i = 0; i < transform.childCount; i++) {
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
