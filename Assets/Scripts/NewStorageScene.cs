using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewStorageScene : MonoBehaviour {

    [Header("Camera")]
    public Camera mainCam;


    [Header("Order Details")]
    public GameObject blackBackground;
    public GameObject deliveryDetails;
    public Text foodName;
    public Text foodStorageType;
    private GameObject selectedBox;

    [Header("Storing Related")]
    public GameObject selectStorage;
    public Text acceptedWrongOrderFeedback;
    public Button placementTop;
    public Button placementBottom;

    [SerializeField]
    private GameObject orderList;
    [SerializeField]
    private GameObject truckManager;


    // Use this for initialization
    void Start() {
        orderList.GetComponent<OrderListManager>().GetOrderList();
    }

    // Update is called once per frame
    void Update() {

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


            if ((Physics.Raycast(mousePosN, mousePosF - mousePosN, out hit)))
            {
                if (!selectStorage.activeSelf && selectedBox == null && !deliveryDetails.activeSelf)
                {
                    switch (hit.collider.gameObject.tag)
                    {
                        //case ("freezeDoor"):
                        //    {
                        //        mainCam.transform.position = new Vector3(-15.2f, 2, -4.2f);
                        //        mainCam.transform.rotation = Quaternion.Euler(15, 0, 0);
                        //        inStorageRoom = true;
                        //    }
                        //    break;
                    
                        case ("Truck"):
                            {
                                if (hit.collider.gameObject.GetComponent<Truck>().food.foodPrefab != null &&
                                   hit.collider.gameObject.GetComponent<Truck>() != null)
                                {
                                    selectedBox = hit.collider.gameObject;
                                    blackBackground.SetActive(true);
                                    deliveryDetails.SetActive(true);
                                    foodName.text = selectedBox.gameObject.GetComponent<Truck>().food.foodName.ToString();
                                    switch (selectedBox.gameObject.GetComponent<Truck>().food.foodType)
                                    {
                                        // Store in Dry
                                        case ((Food.FoodType)0):
                                            foodStorageType.text = "Dry";
                                            break;
                                        // Store in Cold
                                        case ((Food.FoodType)1):
                                            foodStorageType.text = "Cold";
                                            break;
                                        // Store in Freeze
                                        case ((Food.FoodType)2):
                                            foodStorageType.text = "Freeze";
                                            break;
                                    }

                                }
                            }
                            break;
                    }
                }
                else if (selectStorage.activeSelf)
                {
                    if (hit.collider.gameObject.name == ("dryDoor"))
                    {
                        checkForCorrectDoor(0); // Dry food         
                    }
                    else if (hit.collider.gameObject.name == ("wetDoor"))
                    {
                        checkForCorrectDoor(1);// Cold food
                    }
                    else if (hit.collider.gameObject.name == ("freezeDoor"))
                    {
                        checkForCorrectDoor(2); // Frozen food
                    }
                }
            }
        }
    }

    void showOrderDetails()
    {
        blackBackground.SetActive(true);
        deliveryDetails.SetActive(true);
    }

    public void acceptOrder()
    {
        OrderListManager.orderLimit--;
        //destroy the food from the orderlist
        removeFromOrderList();

        //if correct items
        if (checkForCorrectItems())
        {
            //TouchManager.gameObject.GetComponent<JHTouchManager>().pointsGiveToStockBar = 0.2f;
            selectStorage.SetActive(true);
        }
        else
        {
            // TouchManager.gameObject.GetComponent<JHTouchManager>().pointsGiveToStockBar = 0.1f;

            //enable the wrong food pop up
            // wrongFoodPopup.SetActive(true);
            //play that animation
            // TouchManager.gameObject.GetComponent<JHTouchManager>().wrongFoodAnim.SetTrigger("Show");
            acceptedWrongOrderFeedback.gameObject.SetActive(true);
        }
        blackBackground.SetActive(false);
        deliveryDetails.SetActive(false);
    }

    public void rejectOrder()
    {

    }

    public void closeOrder()
    {
        // closes the delivery details UI, allowing you to select another box to view its contents
        blackBackground.SetActive(false);
        deliveryDetails.SetActive(false);
        selectedBox = null;
    }

    public void removeFromOrderList()
    {
        int num = 0;

        for (int i = 0; i < orderList.transform.childCount; i++)
        {
            //if correct food
            if (orderList.transform.GetChild(i).GetComponentInChildren<Order>().food == selectedBox.GetComponent<Truck>().food)
            {
                num = i;
                break;
            }
            //if wrong food
            else
            {
                //run a check between the orderlist and truck manager to find the odd one out(the wrong food)
                for (int j = 0; j < orderList.transform.childCount; j++)
                    for (int k = 0; k < truckManager.transform.childCount; k++)
                        if (orderList.transform.GetChild(j).GetComponentInChildren<Order>().food != truckManager.transform.GetChild(k).GetComponent<Truck>().food)
                            num = i;
            }
        }

        //delete them from orderlist
        Destroy(orderList.transform.GetChild(num).gameObject);
    }

    public bool checkForCorrectItems()
    {
        bool status = false;
        //check the food in the truck is what the orderlist wants
        for (int i = 0; i < orderList.transform.childCount; i++)
        {
            //if correct food
            if (orderList.transform.GetChild(i).GetComponentInChildren<Order>().food == selectedBox.GetComponent<Truck>().food)
            {
                status = true;
                break;
            }
            else
                status = false;
        }

        //return if its the correct food
        return status;
    }

    void checkForCorrectDoor(int selectedFoodType)
    {
        if (selectedBox.gameObject.GetComponent<Truck>().food.foodType == (Food.FoodType)selectedFoodType)
        {
            selectStorage.SetActive(false);
            if (acceptedWrongOrderFeedback.gameObject.activeSelf)
                acceptedWrongOrderFeedback.gameObject.SetActive(false);
            mainCam.transform.position = new Vector3(-15.2f, 1.85f, -3.65f);
            mainCam.transform.rotation = Quaternion.Euler(15, 0, 0);
            placementTop.gameObject.SetActive(true);
            placementBottom.gameObject.SetActive(true);
        }
        else
        {
            selectStorage.GetComponent<Text>().text = "Not there. Try Again";
        }
    }

    public void PlacementTop()
    {
        if (selectedBox.GetComponent<Truck>().food.foodPlacement.ToString().Equals("TOP"))
        {
            Debug.Log("TOP OK");
            //Debug.Log("GOOD!!");
            //TouchManager.gameObject.GetComponent<JHTouchManager>().reducePoints = false;
            //TouchManager.gameObject.GetComponent<JHTouchManager>().OpenUserFeedback();
            //TouchManager.gameObject.GetComponent<JHTouchManager>().UserFeedback.GetComponentInChildren<Text>().text = "Correct!";
            //TouchManager.gameObject.GetComponent<JHTouchManager>().placementDone = true;

            ////add to stock points
            //StocknPopularityManager.stockValue += TouchManager.gameObject.GetComponent<JHTouchManager>().pointsGiveToStockBar;
        }
        else
        {
            Debug.Log("TOP BAD");
            //Debug.Log("BAD!!");
            //TouchManager.gameObject.GetComponent<JHTouchManager>().reducePoints = true;
            //TouchManager.gameObject.GetComponent<JHTouchManager>().OpenUserFeedback();
            //TouchManager.gameObject.GetComponent<JHTouchManager>().UserFeedback.GetComponentInChildren<Text>().text = "Wrong placement!";
        }
    }
    public void PlacementBot()
    {
        if (selectedBox.GetComponent<Truck>().food.foodPlacement.ToString().Equals("BOTTOM"))
        {
            Debug.Log("BOTTOM OK");
            //Debug.Log("GOOD!!");
            //TouchManager.gameObject.GetComponent<JHTouchManager>().reducePoints = false;
            //TouchManager.gameObject.GetComponent<JHTouchManager>().OpenUserFeedback();
            //TouchManager.gameObject.GetComponent<JHTouchManager>().UserFeedback.GetComponentInChildren<Text>().text = "Correct!";
            //TouchManager.gameObject.GetComponent<JHTouchManager>().placementDone = true;

            ////add to stock points
            //StocknPopularityManager.stockValue += TouchManager.gameObject.GetComponent<JHTouchManager>().pointsGiveToStockBar;
        }
        else
        {
            Debug.Log("BOTTOM BAD");
            //Debug.Log("BAD!!");
            //TouchManager.gameObject.GetComponent<JHTouchManager>().reducePoints = true;
            //TouchManager.gameObject.GetComponent<JHTouchManager>().OpenUserFeedback();
            //TouchManager.gameObject.GetComponent<JHTouchManager>().UserFeedback.GetComponentInChildren<Text>().text = "Wrong placement!";
        }
    }
}
