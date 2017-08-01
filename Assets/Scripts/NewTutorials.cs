using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewTutorials : MonoBehaviour {

    [Header("UI Buttons")]
    public GameObject TUT_StorageButton;
    public GameObject TUT_ShopsButton;
    public GameObject TUT_DinerButton;
    public GameObject TUT_OrderListButton;
    public GameObject TUT_OptionsButton;
    public GameObject TUT_MeatFabButton;


    [Header("Others")]
    public GameObject TUT_OptionsMenu;
    public GameObject TUT_OrderList;
    public GameObject TUT_HUD;
    public GameObject TUT_blackBackground;
    public GameObject Arrow;
    public GameObject TUT_Instructions;
    public GameObject inShopInstructions;
    public GameObject TapAnywhereToCont;

    [Header("\"Holders\"")]
    public GameObject mainGame;
    public GameObject Tutorial;
    public GameObject TutorialArrows;
    public GameObject inSupplierShopTutorial;

    public static bool tutDone;
    bool boughtFood;
    bool storedFood;
    public static int currentStep = 3; // which part of the tutorial

    /*
     * Tutorial Steps/Screens active
     * Step 1: Welcome
     * Step 2: No Food
     * Step 3: Click on any shops to go in (supplier scene)
     * Step 4: Yellow arrows pointing at each of the shops
     * Step 5: Choose the food that looks the freshest
     * Step 6: Accept/Reject the food
     * Step 7: Confirm to buy food
     */
	// Use this for initialization
	void Start () {
       
	}

    // Update is called once per frame
    void Update()
    {
        if (tutDone)
            return;

        if (SceneManager.GetActiveScene().name == "Virt_Restuarant")
        {
            switch (currentStep)
            {
                // Step 1: Tap anywhere to continue, text changes to "need to buy food"
                case 1:
                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        TUT_HUD.SetActive(true);
                        TUT_Instructions.GetComponentInChildren<Text>().text = "First, to operate a restaurant, \nyou need food. You don't have\n food, let's buy some.";
                        currentStep += 1;
                    }
                    break;
                // Step 2: Tap anywhere to continue, text changes to "Click on Shops to head over to the suppliers."
                case 2:
                    if (Input.GetMouseButtonDown(0))
                    {
                        TapAnywhereToCont.SetActive(false);
                        TUT_HUD.SetActive(false);
                        TUT_ShopsButton.SetActive(true);
                        Arrow.SetActive(true);
                        TUT_Instructions.GetComponentInChildren<Text>().text = "Click on Shops to\n head over to the suppliers.";
                        currentStep += 1;
                        // Goes to supplier scene here
                    }
                    break;

            }
        }
        else if (SceneManager.GetActiveScene().name == "Virt_Suppliers")
        {
            Debug.Log(currentStep);
            switch (currentStep)
            {
                //Step 3: Tap anywhere to continue, after this the tutorial window closes, allowing players to choose which shop they want to enter
                case 3:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Tutorial.SetActive(false);
                        TutorialArrows.SetActive(true);
                        currentStep += 1;
                    }
                    break;
                // Step 4: Entered Shop
                case 4:
                    if (TouchManager.inShop)
                    {
                        TutorialArrows.SetActive(false);
                        currentStep += 1;                   
                    }
                    break;
                // Step 5: Choose which food they want
                    
                case 5:
                    if (Input.GetMouseButtonDown(0))
                    {
                        inShopInstructions.transform.localPosition = new Vector3(357, 321, 0);
                        inShopInstructions.GetComponentInChildren<Text>().text = "Tap Accept to confirm the\n order, and tap Reject to\n choose something else.";
                        currentStep += 1;
                    }
                    break;
                // Step 6: Chosen food window is open, waiting on user to choose accept or reject
                    // If user clicks reject (to choose another food, currentStep will -1, done on click of reject button 
                    // If they click on the back to overview button, currentStep will -2, done on click of back to overview button
                case 6:
                    break;
                case 7:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Tutorial.SetActive(true);
                        mainGame.SetActive(true);
                        TUT_blackBackground.SetActive(true);
                        TUT_Instructions.GetComponentInChildren<Text>().text = "Now you have to store your\n food. Click Storage.";
                        TUT_StorageButton.SetActive(true);
                        TapAnywhereToCont.SetActive(false);
                    }
                    break;
                case 8:
                    break;
            }
        }
    }

    public void addCurrentStep(int amt)
    {
        if (!tutDone)
            currentStep += amt;
    }

    public void backToOverViewReduceStep()
    {
        if (!tutDone)
            currentStep = 4;
        TouchManager.inShop = false;
    }
}
