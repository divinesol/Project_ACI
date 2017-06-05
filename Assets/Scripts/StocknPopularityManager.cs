using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class StocknPopularityManager : MonoBehaviour {

    public static float stockValue;
    public static float popValue;
    public static float mainRatingValue;

    public TextMeshProUGUI stockText;

    public Image popularityBar;
    public Image mainRatingBar;

    // Use this for initialization
    void Start () {

        stockText.text = (stockValue * 100).ToString() + "%";
        popularityBar.fillAmount = 0.0f;
        mainRatingBar.fillAmount = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(mainRatingValue);

        if (stockValue >= 1) stockValue = 1;
        if (popValue >= 1) popValue = 1;
        if (mainRatingValue >= 1) mainRatingValue = 1;

        if (stockValue <= 0) stockValue = 0;
        if (popValue <= 0) popValue = 0;
        if (mainRatingValue <= 0) mainRatingValue = 0;

        popularityBar.fillAmount = popValue;
        mainRatingBar.fillAmount = mainRatingValue;

        stockText.text = (stockValue * 100).ToString() + "%";

        //if(stockValue >= 0 && stockValue <= 0.2)
        //    stockBar.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/stock_0");
        //if (stockValue > 0.2 && stockValue <= 0.4)                                   
        //    stockBar.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/stock_1");
        //if (stockValue > 0.4 && stockValue <= 0.6)                               
        //    stockBar.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/stock_2");
        //if (stockValue > 0.6 && stockValue <= 0.8)                                 
        //    stockBar.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/stock_3");
        //if (stockValue > 0.8 && stockValue <= 1)                               
        //    stockBar.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/stock_4");


        //if (popValue >= 0 && popValue <= 0.2)
        //    popularityBar.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/Popularity_0");
        //if (popValue > 0.2 && popValue <= 0.4)                                                 
        //    popularityBar.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/Popularity_1");
        //if (popValue > 0.4 && popValue <= 0.6)                                                
        //    popularityBar.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/Popularity_2");
        //if (popValue > 0.6 && popValue <= 0.8)                                                
        //    popularityBar.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/Popularity_3");
        //if (popValue > 0.8 && popValue <= 1)                                                  
        //    popularityBar.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/Popularity_4");

        //if (mainRatingValue >= 0 && mainRatingValue <= 0.2)
        //    popIcon.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/face_1");
        //if (mainRatingValue > 0.2 && mainRatingValue <= 0.4)                      
        //    popIcon.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/face_2");
        //if (mainRatingValue > 0.4 && mainRatingValue <= 0.6)                     
        //    popIcon.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/face_3");
        //if (mainRatingValue > 0.6 && mainRatingValue <= 0.8)                     
        //    popIcon.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/face_4");
        //if (mainRatingValue > 0.8 && mainRatingValue <= 1)                       
        //    popIcon.sprite = Resources.Load<Sprite>("HUD_Popularity and stock/face_5");


        #region DebugInputs

        if (Input.GetKeyUp(KeyCode.O))
        {
            ReduceStockPoints(0.1f);
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            AddStockPoints(0.1f);
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            ReducePopularityPoints(0.1f);
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            AddPopularityPoints(0.1f);
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            ReduceRatingsPoints(0.1f);
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            AddRatingsPoints(0.1f);
        }

        #endregion

    }


    public void AddStockPoints(float amt)
    {
        stockValue += amt;
    }

    public void ReduceStockPoints(float amt)
    {
        stockValue -= amt;
    }

    public void AddPopularityPoints(float amt)
    {
        popValue += amt;
    }

    public void ReducePopularityPoints(float amt)
    {
        popValue -= amt;
    }
    public void AddRatingsPoints(float amt)
    {
        mainRatingValue += amt;
    }

    public void ReduceRatingsPoints(float amt)
    {
        mainRatingValue -= amt;
    }
}
