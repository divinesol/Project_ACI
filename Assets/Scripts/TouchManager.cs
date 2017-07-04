using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchManager : MonoBehaviour
{

    /*TouchManager/GameManager for Supplier Scene
     
         Scene is divided into 3 parts : Main Scene, Supplier Info, Selection Scene
         - Main Scene : Initial Scene
         - Supplier Info : Displays supplier info
         - Selection Scene : The gameplay that players chooses their food
         
         This is the main script that controls SUPPLIER SCENE*/


    [Header("Animation")]
    public Animator SupplierAnim;          //Animation for Supplier Info popup
    public Animator SelectionTopAnim;      //During Selection, Top bar Drop animation
    public Animator SelectionBtmAnim;      //During Selection, Btm bar rise animation


    [Header("Canvas Objects")]
    //Canvas as GameObjects to enable easy active off/on in codes
    public GameObject UI_MainCanvas;
    public GameObject UI_SupplierCanvas;          //supplier ui
    public GameObject UI_SelectionCanvas;
    public GameObject UI_SettingsCanvas;
    //public GameObject UI_NotificationBar;       //Panel for OrderList
    public GameObject UI_FullStockNotif;
    public GameObject ObjectListParent;         //Parent to Create instance of each "order" under as child
    public GameObject ObjectListBackPanel;
    public GameObject UI_SelectConfirmation;    //Seperated Confirmation UI with Purchase UI so Borders of 
    public GameObject UI_PurchaseUI;                //Selection Scene not needed to remove
    //public GameObject tutorialObject;           //Tutorial Panel stuffs
    //Popup when food bought in Main scene
    public GameObject popupnotifObject;


    //[Header("Image Buttons")]
    ////MenuBar Navigation
    //public GameObject btnGO_Restaurant;
    //public GameObject btnGO_Warehouse;
    //public GameObject btnGO_Supplier;
    //public GameObject btnGO_AR;
    //public GameObject returnbtnForSupplier;


    [Header("Canvas 3D Objects")]
    //3D on UI is done by putting 3D model then a plane UI as Camera Child
    public GameObject UI_SupplierModel;
    public GameObject UI_SelectionModels;
    public GameObject UI_SelectionPanel;
    public GameObject AllSuppliers_Parent;
    public GameObject AR_StarQuality_Parent;



    [Header("InGame Objects")]
    public GameObject SelectionSceneForAR;      //AR Images and UI on AR Scene
    public GameObject AllEnvironmentForAR;      //All environment for UI
    public GameObject SelectionCircleForAR;     
    public GameObject ARConfirmation;           
    public GameObject ARPurchased;              
    //Storage Indicator when Food stock exist
    //public GameObject CircleStorage;

    public GameObject Grime;

    //public GameObject[] notificationList;

    [Header("Spawn Locations")]
    //Spawn location for trucks for different Suppliers
    public GameObject Spawn_A;  
    public GameObject Spawn_B;
    public GameObject Spawn_C;
    public GameObject Spawn_D;

    [Header("DataTemps")]
    public GameObject selectedFood;        //Data holder for last food selected
    public Text FoodOftheDay;               //Text to show general food type during selection
    public GameObject FinalPurchased_ReturnPosition;    //Coded Animation For Food selection in Virtual Scene
    public GameObject FinalPurchased_Target;            


    [Header("Miscellaneous")]
    public GameObject cameraCheck;
    public int CheckWhichSupplierToSendTruck;
    bool mainCanvas_open;
    bool SelConfm_open;
    private bool isDragging;    //Check for Drag so suppliers or other elements cant be selected while drag

    //bool for selection model animation
    bool moveforward;
    bool moveback;

    public StocknPopularityManager stocknPopInstance;   //Bar UI Instance
    public Tutorial tutorial;                   //Tutorial Instance
    //number that decides food type to supplier type
    private int decidefoodnumber;


    // Use this for initialization
    void Start()
    {
        CheckWhichSupplierToSendTruck = 0;
        mainCanvas_open = true;

        //Loads the OrderList from GameCache In Editor
        ObjectListParent.GetComponent<OrderListManager>().GetOrderList();

        /*99 = any food ok (Random)
            0 = Vegetables Only (Tomato, Mushroom)
            1 = Canned/Packed Products (Canned Food)
            2 = Meat Products (Steak, Chicken)
            3 = Dairy (Cheese)*/
        decidefoodnumber = 99;
        
    }

    // Update is called once per frame
    void Update()
    {


        //Storage Indicator 
        //Check if Stock pending Storage Exist. (AR has no Storage button)
        //if (SceneManager.GetActiveScene().name != "AR_Main")
        //{
        //    if (OrderListManager.orderInstance.transform.childCount >= 1)
        //    {
        //        CircleStorage.SetActive(true);
        //    }
        //    else
        //        CircleStorage.SetActive(false);
        //}


        //Check on touch
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosFar = new Vector3(Input.mousePosition.x,
                                               Input.mousePosition.y,
                                               Camera.main.farClipPlane);
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x,
                                               Input.mousePosition.y,
                                               Camera.main.nearClipPlane);
            Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
            Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);

            Debug.DrawRay(mousePosN, mousePosF - mousePosN, Color.green);


            //Create Raycast
            RaycastHit hit;


            if ((Physics.Raycast(mousePosN, mousePosF - mousePosN, out hit)) 
                //&& !UI_SupplierCanvas.activeSelf
                && !UI_SettingsCanvas.activeSelf
                && !cameraCheck.GetComponent<DragCamera>().IsDragging
                && !UI_FullStockNotif.activeSelf
                && !ObjectListBackPanel.activeSelf
                /*&& tutorialObject.activeSelf == false*/)
            {
                //Check Raycast hit with Gameobject's tag
                switch (hit.collider.gameObject.tag)
                {

                    #region RaycastHitTags

                    case ("example"):
                        {
                            Destroy(hit.transform.gameObject);
                        }
                        break;

                    case ("Supplier"):
                        {

                            //To OPEN supplier screen
                            //Check if SelectionModels is off AND if Tutorial is done
                            if (!UI_SelectionModels.activeSelf)
                            {
                                CloseMainUI();

                                //CurrentSupplier is a temp to hold "last clicked" Supplier's data
                                SupplierSceneManager.SupplierInstance.CurrentSupplier = hit.collider.GetComponent<SupplierInfo>();
                                SupplierSceneManager.SupplierInstance.ChangeUI();

                                //After Checking tag, Check gameobject name to check for supplier type to generate the models
                                //if (hit.collider.gameObject.name == "Factory 1") //Dairy/Cheese food supplier
                                //{
                                //    CheckWhichSupplierToSendTruck = 4;
                                //    decidefoodnumber = 3;
                                //}
                                //if (hit.collider.gameObject.name == "Canned_Food_Factory")
                                //{
                                //    CheckWhichSupplierToSendTruck = 3;
                                //    decidefoodnumber = 1;
                                //}
                                //if (hit.collider.gameObject.name == "Vegetable_Factory")
                                //{
                                //    CheckWhichSupplierToSendTruck = 2;
                                //    decidefoodnumber = 0;
                                //}
                                //if (hit.collider.gameObject.name == "Meat_Factory")
                                //{
                                //    CheckWhichSupplierToSendTruck = 1;
                                //    decidefoodnumber = 2;
                                //}
                                if (hit.collider.gameObject.name == "Factory 1") //Dairy/Cheese food supplier
                                {
                                    CheckWhichSupplierToSendTruck = 4;
                                    decidefoodnumber = 3;
                                }
                                if (hit.collider.gameObject.name == "Canned_Food_Factory")
                                {
                                    CheckWhichSupplierToSendTruck = 3;
                                    decidefoodnumber = 1;
                                }
                                if (hit.collider.gameObject.name == "VeggieShop")
                                {
                                    CheckWhichSupplierToSendTruck = 2;
                                    decidefoodnumber = 0;
                                }
                                if (hit.collider.gameObject.name == "Meat_Factory")
                                {
                                    CheckWhichSupplierToSendTruck = 1;
                                    decidefoodnumber = 2;
                                }
                                //Setting temp model to model of clicked supplier
                                UI_SupplierModel.transform.GetChild(0).gameObject.GetComponent<MeshFilter>().mesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;
                                UI_SupplierModel.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = hit.collider.gameObject.GetComponent<MeshRenderer>().material;

                                OpenSupplierUI();
                            }
                        }
                        break;


                    //During Selection Scene (Virt), FoodSelection tag per Food item during selection
                    case ("FoodSelection"):
                        {
                            //On selected raycast hit food

                            //Check if first Confirmation(yes/no) is active
                            if (UI_SelectConfirmation.activeSelf == false)  
                            {
                                //check if already purchased screen is active
                                if (UI_PurchaseUI.activeSelf == false) 
                                {
                                    //set current selected stock to the current hit by raycast. StockInfo hold the data . current is a data holder
                                    StockManager.StockInstance.CurrentStock = hit.collider.GetComponent<StockInfo>();

                                    //selectedfood is same as current. holds data of current food but used in this script (TouchManager)
                                    selectedFood = hit.collider.gameObject; 

                                    //Enable/show and move animated model
                                    MoveSelectedFoodAnimation();
                                }
                            }

                        }
                        break;

                    //During Selection Scene(AR), ImageSelectionAR tag per Food item during selection
                    case ("ImageSelectionAR"):
                        {
                            SelectionCircleForAR.transform.position = hit.collider.gameObject.transform.position;   
                            SelectionCircleForAR.SetActive(true);
                            ARConfirmation.SetActive(true);
                            selectedFood = hit.collider.gameObject;

                        }
                        break;
                    case ("ARWrongBtn"):
                        {
                            SelectionCircleForAR.SetActive(false);
                            ARConfirmation.SetActive(false);
                        }
                        break;
                    case ("ARRightBtn"):
                        {
                            OrderListManager.orderInstance.ShowOrder(selectedFood.GetComponent<StockInfo>().food.foodName);
                            ARConfirmation.SetActive(false); 
                            SupplierSceneManager.SupplierInstance.RandomSupplierRating();
                            OpenPurchaseUI();
                            //off selection header;
                        }
                        break;

                    //Enable Purchased Image
                    case ("ARPurchased"):
                        {
                            ClosePurchaseAR();
                            
                            BuyStock();
                        }
                        break;

                    case ("Grime"):
                        {
                            hit.collider.gameObject.SetActive(false);
                        }

                        break;

                        #endregion

                }
            }
        }


        #region Animation for model display when selected

        //SelectFoodAnim
        RunForwardSelectionAnimation();

        RunReturnSelectionAnimation();

        #endregion


    }


    #region OpenCloseUIStuff

    public void OpenMainUI()
    {
        UI_MainCanvas.SetActive(true);

    }
    public void CloseMainUI()
    {
        UI_MainCanvas.SetActive(false);
    }
    public void OpenSupplierUI()
    {
        UI_SupplierCanvas.SetActive(true);
        UI_SupplierModel.SetActive(true);   //3D supplier on UI Screen
        SupplierAnim.SetTrigger("Open");
    }
    public void CloseSupplierUI()
    {
        SupplierAnim.SetTrigger("Close");
        //Delay so GameObject UI does not close instantly (Close after Animation is done)
        StartCoroutine(CloseSupplierAnimProcess());
    }

    //Animation to close supplier
    public IEnumerator CloseSupplierAnimProcess()
    {
        yield return new WaitForSeconds(SupplierAnim.GetCurrentAnimatorStateInfo(0).length);
        
        //Set UI active to hide after Animation is done
        UI_SupplierCanvas.SetActive(false);
        UI_SupplierModel.SetActive(false);
    }

    public void OpenSelectionUI()
    {
        UI_SelectionCanvas.SetActive(true);
        UI_SelectionModels.SetActive(true);     //4 Food Models that players will see to choose quality from
    }
    public void CloseSelectionUI()
    {
        UI_SelectionCanvas.SetActive(false);
        UI_SelectionModels.SetActive(false);
    }

    //Close ONLY "yes/no" buttons DURING SELECTION
    public void CloseSelectionConfirmation()
    {
        UI_SelectConfirmation.SetActive(false);
    }

    //"You have Bought xxx!" UI.
    public void OpenPurchaseUI()
    {
        if (SceneManager.GetActiveScene().name != "AR_Main")
        {
            if (!tutorial.tutDone)
                tutorial.NextBtn();

            UI_PurchaseUI.SetActive(true);
            
            //Ratings on Purchase Scene to show quality of food bought
            StockManager.StockInstance.SetRatingFill();
        }
        else
        {
            //markaa
            //TODO : Add Quality of food during AR Mode
            /* Current Quality of food in virtual mode is done with "Image Fill Amount".
                In 3D space of AR mode, Image has no "Fill Amount".*/


            int tempQuality = (int)(selectedFood.GetComponent<StockInfo>().food.foodRarity);


            for (int i = 0; i < tempQuality; i++)
            {
                AR_StarQuality_Parent.transform.GetChild(i+1).gameObject.SetActive(true);
            }

            ARPurchased.SetActive(true);
        }
    }

    public void ClosePurchase()
    {
        if (!tutorial.tutDone)
            tutorial.NextBtn();

        UI_PurchaseUI.SetActive(false);
    }

    public void ClosePurchaseAR()
    {
        ARPurchased.SetActive(false);
        AllEnvironmentForAR.SetActive(true);
        SelectionSceneForAR.SetActive(false);
        SelectionCircleForAR.SetActive(false);
        UI_MainCanvas.SetActive(true);

        for (int i = 1; i < AR_StarQuality_Parent.transform.childCount; i++)
        {
            AR_StarQuality_Parent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    #endregion


    //Check if Purchase amount has reached orderlist Limit
    public void BasicCheckBeforeBuying()
    {
        if (!tutorial.tutDone)
            tutorial.NextBtn();

        //Max 5 limit of Purchases
        if (ObjectListParent.transform.childCount >= 5)
        {
            //if reached max, Show MaxLimit Notification
            LimitedPurchase();
        }
        else
        {
            SuccessfulPurchase();
        }
    }

    public void SuccessfulPurchase()
    {
        //Open Selection Scene
        CloseSupplierUI();
        if (SceneManager.GetActiveScene().name != "AR_Main")
        {
            OpenSelectionUI();
            UI_SelectionPanel.SetActive(true);
        }
        else
        {
            SelectionSceneForAR.SetActive(true);
            AllEnvironmentForAR.SetActive(false);
        }

        //Set Quality of foods during Selection Scene (4 Choices)
        StockManager.StockInstance.SetStockRatings();
        //Generate Type of Food to be Chosen. ("decidefoodnumber" is fixed based on factory/Supplier type during raycast collision)
        StockManager.StockInstance.RandomizeFoodType(decidefoodnumber);
    }
    public void LimitedPurchase()
    {
        CloseSupplierUI();
        UI_FullStockNotif.SetActive(true);
    }

    public void ReturnWithoutBuying()
    {
        CheckWhichSupplierToSendTruck = 0;
    }

    public void BuyStock()
    {
        //Instantiate Truck based on player's choice of food supplier
        switch (CheckWhichSupplierToSendTruck)
        {
            case 1:
                {//Meat Factory
                    Spawn_A.gameObject.GetComponent<SpawnScript>().waitSpawner();
                    CheckWhichSupplierToSendTruck = 0;
                }
                break;
            case 2:
                {//Vegetable Factory
                    Spawn_B.gameObject.GetComponent<SpawnScript>().waitSpawner();
                    CheckWhichSupplierToSendTruck = 0;
                }
                break;
            case 3:
                {//Canned Food Factory
                    Spawn_C.gameObject.GetComponent<SpawnScript>().waitSpawner();
                    CheckWhichSupplierToSendTruck = 0;
                }
                break;
            case 4:
                {//Dairy Factory Spawn Point
                    Spawn_D.gameObject.GetComponent<SpawnScript>().waitSpawner();
                    CheckWhichSupplierToSendTruck = 0;
                }
                break;
            default:    //On default, Do nothing
                return;
        }

        //Set Feedback to tell player to go Storage Scene
        popupnotifObject.SetActive(true);
        PopUpBarNotifAnim.ispopup = true;

        //Update Overall Quality of food. (Player interface at top left)
        UpdateNewStockRatings();

    }


    //Update Player's Overall Food Quality
    /*This element of the game determines how much popularity points 
        is given by the customers in the later <Restaurant> Stage.
        
         It is determined by the average of "Player's Current Quality" 
          and the quality of food that the player last bought.
         */
    public void UpdateNewStockRatings()
    {
        float currTemp = StocknPopularityManager.mainRatingValue * 5;
        float currFoodRarity = (float)selectedFood.GetComponent<StockInfo>().food.foodRarity;
        float final = (currTemp + currFoodRarity) * 0.5f;

        //Round off the quality fill at "half" the star.
        if(final % 1 > 0.5)
            StocknPopularityManager.mainRatingValue = ((int)final + 1) * 0.2f;
        else if(final % 1 < 0.5)
            StocknPopularityManager.mainRatingValue = (int)final * 0.2f;
        else
            StocknPopularityManager.mainRatingValue = final * 0.2f;

    }

    //After Purchase close
    public void PurchaseReturnMarco(bool finalCheck)
    {
        CloseSelectionConfirmation();

        CloseSelectionUI();

        OpenMainUI();

        if (finalCheck)
        {
            //Reset all Supplier's Food Quality
            SupplierSceneManager.SupplierInstance.RandomSupplierRating();

            ClosePurchase();

            //Reset Selection Models positions
            StockManager.StockInstance.ReturnCurrentModelSize();

            StockManager.StockInstance.SetStockRatings();

            BuyStock();

        }
        ReturnSelectedFoodAnimation();
    }


    //Animation to control food movement during selection
    #region AnimationDuringSelection

    /* Coded Animation of 3D Selection models movement to Center of Screen.
     
         Actual order of execution is numbered by "#".
         To see where the code is running, Find: SelectFoodAnim */

    //#2
    public void RunForwardSelectionAnimation()
    {
        //On Update, if animated model is moveforward is true
        if (moveforward)
        {
            //Check distance btwn animated model and display target
            float dist;
            dist = Vector3.Distance(FinalPurchased_Target.transform.position, selectedFood.transform.position);

            //move animated model to target
            if (dist > 0.1)
                selectedFood.transform.position += (FinalPurchased_Target.transform.position - selectedFood.transform.position).normalized * 2 * Time.deltaTime;
            else
            {
                //when reached target, set position and stop bool to stop movement
                selectedFood.transform.position = FinalPurchased_Target.transform.position;
                moveforward = false;
                UI_SelectConfirmation.SetActive(true);
            }
        }
    }

    //#4
    public void RunReturnSelectionAnimation()
    {
        if (selectedFood != null)
        {
            if (moveback)
            {
                float dist;
                dist = Vector3.Distance(FinalPurchased_ReturnPosition.transform.position, selectedFood.transform.position);

                if (dist > 0.1)
                    selectedFood.transform.position += (FinalPurchased_ReturnPosition.transform.position - selectedFood.transform.position).normalized * 2 * Time.deltaTime;
                else
                {
                    selectedFood.transform.position = FinalPurchased_ReturnPosition.transform.position;
                    moveback = false;
                    
                    //Set all models to show
                    for (int i = 0; i < 4; i++)
                    {
                        UI_SelectionModels.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = true;
                        UI_SelectionModels.transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = true;
                    }
                }
            }
        }
    }

    //#1
    public void MoveSelectedFoodAnimation()
    {
        //Setting the animated model position to selected food position
        FinalPurchased_ReturnPosition.transform.position = selectedFood.transform.position;

        //Disable not selected models
        for (int i = 0; i < 4; i++)
        {
            if(UI_SelectionModels.transform.GetChild(i).name != selectedFood.name)
            {
                UI_SelectionModels.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = false;
                UI_SelectionModels.transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                UI_SelectionModels.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = true;
                UI_SelectionModels.transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        moveforward = true;
    }

    //#3
    public void ReturnSelectedFoodAnimation()
    {
        moveback = true;
    }

    #endregion


}
