using UnityEngine;
using UnityEngine.UI;

public class BlocksManager : MonoBehaviour {

    [Header("Player Prefs Manager")]
    public PlayerPrefsManager PPM;

    [Header("Prefabs")]
    public GameObject BlockPrefab;
    public GameObject EarnBallPrefab;
    public GameObject PointBallPrefab;

    [Header("Ball Control Script")]
    public BallControl BC;

    [Header("Text Display")]
    public Text levelText;
    public Text bestScoreText;


    public float blockSpace = 0.75f;
    public float blockSpaceY = 0.75f;
    private float numberOfBlocks = 7;
    private Vector2 startPos;

    public int linesAmount;
    public int bestLinesAmount;

	// Use this for initialization
	void Start () {

        

        startPos.x = ((numberOfBlocks - 1) / 2) * (-blockSpace);
        startPos.y = 3.75f - blockSpaceY;

        linesAmount = 1;
        bestLinesAmount = PPM.LoadBestScore();

        levelText.text = "" + linesAmount;
        bestScoreText.text = "" + bestLinesAmount;

}

// Update is called once per frame
void Update () {
		
        //Check if all the balls are back, if so, dropBlockLine then SpawnBlockLine
        if(BC.canDropLine )
        {
            SpawnBlockLine();
            BC.canDropLine = false;
        }

    }

    void DropBlockLine()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.childCount > 0)
                if(transform.GetChild(i).gameObject != null)
                transform.GetChild(i).transform.position = new Vector2(transform.GetChild(i).transform.position.x,
                    transform.GetChild(i).transform.position.y - blockSpaceY);

            if(transform.GetChild(i).GetComponent<BoxCollidedScript>() != null)
                transform.GetChild(i).GetComponent<BoxCollidedScript>().CheckifTouchingBottom();
        }
    }

    public void SpawnBlockLine()
    {

        if (linesAmount > bestLinesAmount)
            bestLinesAmount = linesAmount;

        //Save the best score
        PPM.SaveBestScore(bestLinesAmount);

        bestScoreText.text = "" + bestLinesAmount;
        levelText.text = "" + linesAmount;

        DropBlockLine();

        for (int i = 0; i < numberOfBlocks; i++)
        {
            int randomSpawn = Random.Range(0, 9);

            //Spawn a block
            if (randomSpawn == 1 || randomSpawn == 2 || randomSpawn == 3)
            {

                GameObject newBlock = Instantiate(BlockPrefab,
                    startPos + new Vector2(i * blockSpace, 0),
                    Quaternion.identity, this.transform);


                int randomAmount = Random.Range(1, 5);

                if (randomAmount == 1 || randomAmount == 3 || randomAmount == 4)
                    newBlock.GetComponent<BoxCollidedScript>().raqem = linesAmount;
                else if (randomAmount == 2)
                    newBlock.GetComponent<BoxCollidedScript>().raqem = linesAmount * 2;

            } else if( randomSpawn == 4 || randomSpawn == 5)
            {
                GameObject newBlock = Instantiate(EarnBallPrefab,
                    startPos + new Vector2(i * blockSpace, 0),
                    Quaternion.identity, this.transform);

                
            } else if (randomSpawn == 6)
            {
                GameObject newBlock = Instantiate(PointBallPrefab,
                    startPos + new Vector2(i * blockSpace, 0),
                    Quaternion.identity, this.transform);
            }
        }

        linesAmount++;
        

    }
}
