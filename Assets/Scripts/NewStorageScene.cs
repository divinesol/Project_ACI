using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewStorageScene : MonoBehaviour {

    float pointsToBeAddedToStock;

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
    public Text StoragePlacementFeedback;
    public Button ExitStorage;

    [Header("Managers")]
    [SerializeField]
    private GameObject orderList;
    [SerializeField]
    private GameObject truckManager;
    [SerializeField]
    private GameObject stockAndPopularityManager;

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
                    if (hit.collider.gameObject.tag == "Truck")
                    {
                        if (hit.collider.gameObject.GetComponent<Truck>().food.foodPrefab != null &&
                                   hit.collider.gameObject.GetComponent<Truck>() != null)
                        {
                            selectedBox = hit.collider.gameObject;
                            foodName.text = selectedBox.gameObject.GetComponent<Truck>().food.foodName.ToString();
                            switch (selectedBox.gameObject.GetComponent<Truck>().food.foodType)
                            {
                                // Store in Dry
                                case (0):
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
                            blackBackground.SetActive(true);
                            deliveryDetails.SetActive(true);

                        }
                    }
                }
                else if (selectStorage.activeSelf || acceptedWrongOrderFeedback.gameObject.activeSelf)
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
            pointsToBeAddedToStock = 0.2f;
            selectStorage.SetActive(true);
        }
        else
        {
            pointsToBeAddedToStock = 0.1f;
            acceptedWrongOrderFeedback.gameObject.SetActive(true);
        }
        blackBackground.SetActive(false);
        deliveryDetails.SetActive(false);
    }

    public void rejectOrder()
    {
        selectedBox.GetComponent<Truck>().readyToRespawn = true;
        selectedBox = null;
        blackBackground.SetActive(false);
        deliveryDetails.SetActive(false);
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
            if (selectStorage.activeSelf)
                selectStorage.GetComponent<Text>().text = "Not there. Try Again";
            else if (acceptedWrongOrderFeedback.IsActive())
                acceptedWrongOrderFeedback.GetComponent<Text>().text = "Not there. Try Again";
        }
    }

    public void PlacementTop()
    {
        if (selectedBox.GetComponent<Truck>().food.foodPlacement.ToString().Equals("TOP"))
        {
            placementTop.gameObject.SetActive(false);
            placementBottom.gameObject.SetActive(false);
            selectStorage.SetActive(false);
            selectStorage.GetComponent<Text>().text = "Where should it be stored?";
            acceptedWrongOrderFeedback.gameObject.SetActive(false);
            TruckManager.foodList[selectedBox.GetComponent<Truck>().index] = new Food();
            selectedBox.GetComponent<Truck>().foodObject.gameObject.GetComponentInParent<Truck>().food = new Food();
            selectedBox.GetComponent<Truck>().foodObject.SetActive(false);
            selectedBox = null;

            ExitStorage.gameObject.SetActive(true);
            StoragePlacementFeedback.gameObject.SetActive(false);

            //add to stock points
            StocknPopularityManager.stockValue += pointsToBeAddedToStock;
        }
        else
        {
            StoragePlacementFeedback.gameObject.SetActive(true);
        }
    }
    public void PlacementBot()
    {
        if (selectedBox.GetComponent<Truck>().food.foodPlacement.ToString().Equals("BOTTOM"))
        {
            placementTop.gameObject.SetActive(false);
            placementBottom.gameObject.SetActive(false);
            selectStorage.SetActive(false);
            selectStorage.GetComponent<Text>().text = "Where should it be stored?";
            acceptedWrongOrderFeedback.gameObject.SetActive(false);
            TruckManager.foodList[selectedBox.GetComponent<Truck>().index] = new Food();
            selectedBox.GetComponent<Truck>().foodObject.gameObject.GetComponentInParent<Truck>().food = new Food();
            selectedBox.GetComponent<Truck>().foodObject.SetActive(false);
            selectedBox = null;
            ExitStorage.gameObject.SetActive(true);
            StoragePlacementFeedback.gameObject.SetActive(false);

            //add to stock points
            StocknPopularityManager.stockValue += pointsToBeAddedToStock;
        }
        else
        { 
            StoragePlacementFeedback.gameObject.SetActive(true);
        }
    }

    public void ExitStorageFunc()
    {
        mainCam.transform.position = new Vector3(-0.5f, 3.05f, -6.22f);
        mainCam.transform.rotation = Quaternion.Euler(10, 0, 0);
        ExitStorage.gameObject.SetActive(false);
    }
}
