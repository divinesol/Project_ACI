using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class StockManager : MonoBehaviour
{
    public static StockManager StockInstance;


    public StockInfo CurrentStock = null;
    public int tempSizeCheck;
    public int tempFoodRand;
    public FoodDatabase database;
    public float[] StarRatingList = new float[4];

   

    [SerializeField]public Image Ratings;

    [SerializeField]public GameObject SelectionModel_A;
    [SerializeField]public GameObject SelectionModel_B;
    [SerializeField]public GameObject SelectionModel_C;
    [SerializeField]public GameObject SelectionModel_D;

    [SerializeField]
    public GameObject MeatSelectionModel_A;
    [SerializeField]
    public GameObject MeatSelectionModel_B;
    [SerializeField]
    public GameObject MeatSelectionModel_C;
    [SerializeField]
    public GameObject MeatSelectionModel_D;

    public GameObject particlegood;
    public GameObject particlebad;

    public Text FoodTitle;
    public GameObject SelectionModel;


    void Awake()
    {
        if (StockInstance == null)
        {
            StockInstance = this;
        }
    }

    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < 4; i++)
        {
            //Generate Stock/Food data for all Models During Selection Scene
            SelectionModel.transform.GetChild(i).GetComponent<StockInfo>().index = i;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReturnCurrentModelSize()
    {

        if (CurrentStock != null)
        {
            CurrentStock.SetActiveSelected(false);
        }
    }

    //Set Food Quality
    public void SetStockRatings()
    {
        //Food Quality is determined by min and max quality of Selected Supplier
        //float minR = SupplierSceneManager.SupplierInstance.CurrentSupplier.minRating;
        //float maxR = SupplierSceneManager.SupplierInstance.CurrentSupplier.maxRating;

        //for (int i = 0; i < StarRatingList.Length; ++i)
        //{
        //    float temp = Mathf.Round(Random.Range(minR, maxR));
        //    StarRatingList[i] = temp;
        //}
    }

    public void SetRatingFill()
    {
        /* 1 = 100% of Image
         * 0.2 = 1/5 of Image
         * 1/5 image = 1 star
         */
        Ratings.fillAmount = (float)CurrentStock.transform.GetComponent<StockInfo>().food.foodRarity * 0.2f;
    }

    public void RandomizeFoodType(int choosenfoodorder)
    {
        //Randomize through FoodDatabase
        tempFoodRand = Random.Range(0, database.food.Count);
        int newRangeCheck;

        //The "tempFoodRand/5" is because all food within Database is in multiples of 5.
        //Thus, it is easy to check based on a single number divisible by 5.
        newRangeCheck = tempFoodRand / 5;

        //choosenfoodorder : See TouchManager.cs Find: decidefoodnumber
        switch (choosenfoodorder)
        {
            case 0:
                {
                    //Keep randomizing until reach Veg Food
                    while (newRangeCheck != 0 && newRangeCheck != 3)
                    {
                        tempFoodRand = Random.Range(0, database.food.Count);
                        newRangeCheck = tempFoodRand / 5;
                    }
                }
                break;
            case 1:
                {
                    //Fixed Canned Food
                    tempFoodRand = 5;
                    newRangeCheck = choosenfoodorder;
                }
                break;
            case 2:
                {
                    //Keep randomizing until reached Meat type Food
                    while (newRangeCheck != 2 && newRangeCheck != 4)
                    {
                        tempFoodRand = Random.Range(0, database.food.Count);
                        newRangeCheck = tempFoodRand / 5;
                    }

                }
                break;
            case 3:
                {
                    //Fixed Dairy Food (Cheese)
                    tempFoodRand = 25;
                    newRangeCheck = tempFoodRand / 5;
                }
                break;
            default:
                {//Inital Gameplay (UNUSED)
                    //Change TouchManager's "decidefoodnumber" = 99 to access Random
                    Debug.Log("Entered Random");
                    newRangeCheck = tempFoodRand / 5;
                }
                break;
        }


        //Give the food model food data
        for (int i = 0; i < 5; i++)
        {
            do
            {
                switch (newRangeCheck)
                {
                    case 0: //Tomato
                        {
                            //for each child "i" randomize food quality within the food type range in the database
                            SelectionModel.transform.GetChild(i).GetComponent<StockInfo>().food 
                                = database.food[Random.Range(0, 5)];

                            //Displays food type during selection scene. Using the food name, reduce the length by 2 to remove the "grade" (eg. A B C)
                            FoodTitle.GetComponent<Text>().text 
                                = database.food[tempFoodRand].foodName.Remove(database.food[tempFoodRand].foodName.Length - 2);
                        }
                        break;
                    case 1: //Canned Food
                        {
                            SelectionModel.transform.GetChild(i).GetComponent<StockInfo>().food 
                                = database.food[Random.Range(5, 10)];

                            FoodTitle.GetComponent<Text>().text 
                                = database.food[tempFoodRand].foodName.Remove(database.food[tempFoodRand].foodName.Length - 2);
                        }
                        break;
                    case 2: //Steak
                        {
                            if (SceneManager.GetActiveScene().name != "AR_Main")
                            {
                                SelectionModel.transform.GetChild(i).GetComponent<StockInfo>().food
                                = database.food[Random.Range(10, 15)];

                                FoodTitle.GetComponent<Text>().text
                                    = database.food[tempFoodRand].foodName.Remove(database.food[tempFoodRand].foodName.Length - 2);
                            }
                            else
                            {
                                //If AR mode, change to Placeholder Image. 
                                //(Meat no steak img -> changed to default meat : Chicken)

                                //TODO : Change to ACI Provided Image!
                                newRangeCheck = 4;
                            }
                        }
                        break;
                    case 3: //Mushroom
               
                        {
                            if (SceneManager.GetActiveScene().name != "AR_Main")
                            {
                                SelectionModel.transform.GetChild(i).GetComponent<StockInfo>().food
                                = database.food[Random.Range(15, 20)];

                                FoodTitle.GetComponent<Text>().text
                                    = database.food[tempFoodRand].foodName.Remove(database.food[tempFoodRand].foodName.Length - 2);
                            }
                            else
                            {
                                //If AR mode, change to Placeholder Image.
                                //(Veg no mushroom img -> changed to default veg : Tomato)
                                 
                                //TODO : Change to ACI Provided Image!
                                newRangeCheck = 0;
                            }
                        }
                        break;
                    case 4: //Chicken

                        {

                            //if (SceneManager.GetActiveScene().name != "AR_Main")
                            //{
                            //if (!SelectionModel.activeSelf)
                            //{
                                SelectionModel.transform.GetChild(i).GetComponent<StockInfo>().food
                                    = database.food[Random.Range(20, 25)];
                            //}
                            //}

                            FoodTitle.GetComponent<Text>().text 
                                = database.food[tempFoodRand].foodName.Remove(database.food[tempFoodRand].foodName.Length - 2);

                        }
                        break;
                    case 5: //Cheese

                        {
                            //if (SceneManager.GetActiveScene().name != "AR_Main")
                            //{
                            //if (!SelectionModel.activeSelf)
                            //{
                            //if(SelectionModel.transform.GetChild(i).GetComponent<StockInfo>().food != null)
                            //{ 
                                SelectionModel.transform.GetChild(i).GetComponent<StockInfo>().food
                                = database.food[Random.Range(25, 30)];
                            //}
                            //}
                            //}

                            FoodTitle.GetComponent<Text>().text = database.food[tempFoodRand].foodName.Remove(database.food[tempFoodRand].foodName.Length - 2);

                        }
                        break;
                }

            }
            //reset till the rarity is between min - max
            while (SelectionModel.transform.GetChild(i).GetComponent<StockInfo>().food.foodRarity < SupplierSceneManager.SupplierInstance.CurrentSupplier.minRating ||
                    SelectionModel.transform.GetChild(i).GetComponent<StockInfo>().food.foodRarity > SupplierSceneManager.SupplierInstance.CurrentSupplier.maxRating);

        }

    }

}
