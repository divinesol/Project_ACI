using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StocknPopularityManager : MonoBehaviour {

    public static float stockValue;
    public static float popValue;
    public static float mainRatingValue;

    //once happiness bar is maxed, add 1 star, then reset happiness bar
    public static float starRating;
    public Image stockBar;
    public Image popularityBar;
    public Image mainRatingBar;

    // Use this for initialization
    void Start () {

<<<<<<< HEAD
        //starRating = 0.0f;
        stockBar.fillAmount = 0.0f;
=======
        starRating = 0.0f;
        //stockBar.fillAmount = 0.0f;
>>>>>>> 003831efca26ac6a8d15538c941192a5dea5da66
        popularityBar.fillAmount = 0.0f;
        mainRatingBar.fillAmount = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

        if (stockValue >= 1) stockValue = 1;
        if (popValue >= 1)
        {
            starRating += 0.2f;
            popValue = 0;
        }
        if (starRating >= 1) starRating = 1;

        if (stockValue <= 0) stockValue = 0;
        if (popValue <= 0) popValue = 0;
        if (starRating <= 0) starRating = 0;


        popularityBar.fillAmount = popValue;
        mainRatingBar.fillAmount = starRating;
        //stockBar.fillAmount = stockValue;

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
            ReduceRatingsPoints(0.2f);
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            AddRatingsPoints(0.2f);
            Debug.Log(starRating);
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
        starRating += amt;
    }

    public void ReduceRatingsPoints(float amt)
    {
        starRating -= amt;
    }
}
