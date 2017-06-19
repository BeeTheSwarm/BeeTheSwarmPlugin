using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    [Header("Scripts")]
    public BallControl BC;
    public PlayerPrefsManager PPM;
    public GameManager GM;

    [Header("Shop Button Prefab")]
    public GameObject ShopButtonPrefab;

    public GameObject[] BallzShop;
    private Button[] LockedButtons;

    private Color32[] BallzColors;

    private static int colorsAmount;
    private static int ballPrice = 200;

	// Use this for initialization
	void Start () {

        colorsAmount = BallzShop.Length;

        //Change the number of colors here
        //BallzShop = new GameObject[colorsAmount];
        LockedButtons = new Button[colorsAmount];

        //Setup the colors of the balls
        BallzColors = new Color32[colorsAmount];
        SetupColors();

        //Scale the Viewport Content so that we can scroll correctly and see all the buttons
        RectTransform r = this.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(r.rect.x, (BallzShop.Length * 80) + 120);

        //Populate the shop
        for (int i = 0; i < BallzShop.Length; i++)
        {/*
            //Instantiate a new button
            GameObject go = Instantiate(ShopButtonPrefab, this.transform);
            go.transform.name = "" + i;

            //Populate the BallzShop array
            BallzShop[i] = go;
            */

            BallzShop[i].transform.name = "" + i;


            //Set the color of the ball
            BallzShop[i].transform.GetChild(0).GetComponent<Image>().color = BallzColors[i];
        }


        Invoke("PopulateLockedButtons", 0.05f);
        Invoke("AddListeners", 0.06f);
        Invoke("LoadPrefValues", 0.07f);
    }

    void LoadPrefValues()
    {
        for (int i = 0; i <  BallzShop.Length; i++)
        {
            //If the ball has already been purchased, delete the locked icon
            if(PPM.LoadBallPurchased(i) == 1)
            {
                Destroy(BallzShop[i].transform.GetChild(1).gameObject);
            }
        }
    }

    void PopulateLockedButtons()
    {
        for (int i = 0; i < BallzShop.Length; i++)
        {
            LockedButtons[i] = BallzShop[i].transform.GetChild(1).GetComponent<Button>();
           
        }
    }

    void AddListeners()
    {
        int k = 0;
        foreach (GameObject b in BallzShop)
        {

            int _k = k++;
            b.GetComponent<Button>().onClick.AddListener(delegate { SetBallColor(_k); });
        }

        int j = 0;
        foreach (Button c in LockedButtons)
        {

            int _j = j++;
            c.onClick.AddListener(delegate { TryPurchaseColor(_j); });
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetBallColor(int buttonId)
    {
        BC.ballColor = BallzShop[buttonId].transform.GetChild(0).GetComponent<Image>().color;
    }

    void TryPurchaseColor(int buttonId)
    {
        if(PPM.LoadCoins() >= ballPrice)
        {
            //Destroy the locked icon
            Destroy(this.transform.GetChild(buttonId).GetChild(1).gameObject);

            //Set the value in the Player Preferences
            PPM.SaveBallPurchased(buttonId);

            //Diminish the coins amount
            PPM.SaveCoins(PPM.LoadCoins() - ballPrice);
            GM.UpdateScoreText();
        }

    }

    void SetupColors()
    {
        //Color 1
        BallzColors[0] = new Color32(255, 174, 175, 255);

        //Color 2
        BallzColors[1] = new Color32(255, 112, 85, 255);

        //Color 3
        BallzColors[2] = new Color32(140, 219, 216, 255);

        //Color 4
        BallzColors[3] = new Color32(255, 128, 44, 255);

        //Color 5
        BallzColors[4] = new Color32(217, 47, 103, 255);

        //Color 6
        BallzColors[5] = new Color32(252, 225, 0, 255);

        //Color 7
        BallzColors[6] = new Color32(255, 4, 44, 255);

        //Color 8
        BallzColors[7] = new Color32(24, 255, 3, 255);

        //Color 9
        BallzColors[8] = new Color32(3, 78, 255, 255);

        //Color 10
        BallzColors[9] = new Color32(255, 107, 235, 255);
    }
}
