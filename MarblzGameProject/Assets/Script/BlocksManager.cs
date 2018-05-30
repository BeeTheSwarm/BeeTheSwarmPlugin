using UnityEngine;
using UnityEngine.UI;

public class BlocksManager : MonoBehaviour {

    [Header("Player Prefs Manager")]
    public PlayerPrefsManager PPM;

    [Header("Prefabs")]
    public GameObject BlockPrefab;
    public GameObject EarnBallPrefab;
    public GameObject PointBallPrefab;
    public GameObject ChestBlockPrefab;

    [Header("Ball Control Script")]
    public BallControl BC;

    [Header("Text Display")]
    public Text levelText;
    public Text bestScoreText;
    public Text m_countDropChance;


    public float blockSpace = 0.75f;
    public float blockSpaceY = 0.75f;
    private float numberOfBlocks = 7;
    private Vector2 startPos;

    public int linesAmount;
    public int bestLinesAmount;

    private float m_increaseChanceRate = 0.1f;
    private float m_startChanceRateChest = 1.9f;
    public float m_nextChanceRateChest;
    private float m_maxChanceRateChest = 50f;
	// Use this for initialization
	void Start () {

        

        startPos.x = ((numberOfBlocks - 1) / 2) * (-blockSpace);
        startPos.y = 3.75f - blockSpaceY;

        linesAmount = 1;
        bestLinesAmount = PPM.LoadBestScore();

        levelText.text = "" + linesAmount;
        bestScoreText.text = "" + bestLinesAmount;
        m_countDropChance.text = "" + m_startChanceRateChest;
        m_nextChanceRateChest = m_startChanceRateChest;

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

            if (transform.GetChild(i).GetComponent<ChestCollided>() != null)
                transform.GetChild(i).GetComponent<ChestCollided>().CheckTouchChestBottom();
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


        IncreaseChanceRateChest();
        m_countDropChance.text = "" + m_nextChanceRateChest;
        Debug.Log("NEW CHANCE RATE:" + m_nextChanceRateChest);
        DropBlockLine();

        

        for (int i = 0; i < numberOfBlocks; i++)
        {
            int randomSpawn = Random.Range(0, 9);

            if (linesAmount % 20 == 0 && i==0)
            {
                Debug.Log("100 CHANCE DROP");
                GameObject newBlock = Instantiate(ChestBlockPrefab,
                            startPos + new Vector2(0 * blockSpace, 0),
                            Quaternion.identity, this.transform);
                newBlock.GetComponent<ChestCollided>().raqem = linesAmount * 4;
            }

            
            //Spawn a block
            else if (randomSpawn == 1 || randomSpawn == 2 || randomSpawn == 3 || randomSpawn == 4)
            {
                                               
                GameObject newBlock = Instantiate(BlockPrefab,
                    startPos + new Vector2(i * blockSpace, 0),
                    Quaternion.identity, this.transform);

                int randomAmount = Random.Range(1, 5);

                if (randomAmount == 1 || randomAmount == 3 || randomAmount == 4)
                    newBlock.GetComponent<BoxCollidedScript>().raqem = linesAmount;
                else if (randomAmount == 2)
                    newBlock.GetComponent<BoxCollidedScript>().raqem = linesAmount * 2;
                
            }
            else if (randomSpawn == 5)
            {
                GameObject newBlock = Instantiate(EarnBallPrefab,
                    startPos + new Vector2(i * blockSpace, 0),
                    Quaternion.identity, this.transform);


            }
            else if (randomSpawn == 6)
            {
                GameObject newBlock = Instantiate(PointBallPrefab,
                    startPos + new Vector2(i * blockSpace, 0),
                    Quaternion.identity, this.transform);
            }
            else if (randomSpawn == 7)
            {

                int spawnPercentage = Random.Range(1,101);
                Debug.Log("spawn percentage:" + spawnPercentage);
                if (spawnPercentage < m_nextChanceRateChest)
                {
                    Debug.Log("ITS HAPPENS.NEW CHEST SPAWNS!!!!");
                    GameObject newBlock = Instantiate(ChestBlockPrefab,
                        startPos + new Vector2(i * blockSpace, 0),
                        Quaternion.identity, this.transform);
                   newBlock.GetComponent<ChestCollided>().raqem = linesAmount * 4;
                  
                }
               
            }
        }

        linesAmount++;
        

    }

    private void IncreaseChanceRateChest()
    {
        if (linesAmount < 100)
        {
            m_nextChanceRateChest = (float)System.Math.Round(m_nextChanceRateChest + m_increaseChanceRate,2);
        }
        else
        {
            m_nextChanceRateChest = (float)System.Math.Round(m_nextChanceRateChest + (m_increaseChanceRate / 2),2);
        }

        if (m_nextChanceRateChest >= m_maxChanceRateChest)
        {
            m_nextChanceRateChest = m_maxChanceRateChest;
            Debug.Log("MAX CHANCE RATE");
        }
    }
}
