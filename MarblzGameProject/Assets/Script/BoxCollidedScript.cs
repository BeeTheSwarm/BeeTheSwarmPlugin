using UnityEngine;
using UnityEngine.UI;

public class BoxCollidedScript : MonoBehaviour {

    //private Text thisText;
    public float raqem;

    Color boxColor;
    SpriteRenderer thisSpriteRenderer;
	[SerializeField] Text text;
	[SerializeField] ParticleSystem ps;
	[SerializeField] SpriteRenderer srTemplate;

    private bool canDestroy;

	// Use this for initialization
	void Start () {

        canDestroy = false;

        //thisText = transform.GetChild(0).GetComponent<Text>();
 

		text.text = "" + raqem;

        //Initialize the sprite Renderer
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();

        SetBoxColor();

    }
	
	// Update is called once per frame
	void Update () {
		
        if(canDestroy)
        {
            //if (transform.GetChild(1).GetComponent<ParticleSystem>().isStopped)
			if (ps.isStopped)
			{
                Destroy(this.gameObject);
            }
        }

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        raqem = raqem - 1;
		text.text = "" + raqem;

        SetBoxColor();

        if (raqem == 0)
        {
            //Hide the text
			text.enabled = false;

			thisSpriteRenderer.enabled = false;
			srTemplate.enabled = false;
//            transform.GetChild(1).GetComponent<ParticleSystem>().Play();
			ps.Play();

            //Disable the box collider
            this.GetComponent<Collider2D>().enabled = false;

            canDestroy = true;
        }
            //Destroy(this.gameObject);

               
    }

    void SetBoxColor()
    {
        //Green when raqem = 1, Red when > to 10, orange in between
        if (raqem >= 10)
            thisSpriteRenderer.color = new Color(91/255f, 151/255f, 210/255f);

        else if(raqem >= 5 && raqem < 10)
        {
            thisSpriteRenderer.color = new Color(1, 1 - (raqem/2-1)/8, 0);
        }
        else if (raqem < 5)
        {
            thisSpriteRenderer.color = new Color( (raqem*2 - 1) / 8, 1, 0);
        }
    }

    public void CheckifTouchingBottom()
    {
        if(transform.position.y < -3.45f)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().SetGameOver();
            Destroy(this.gameObject);

        }
    }
}
