using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Truck : MonoBehaviour
{

    public Food food;
    public int index;
    TruckManager truckManager;
    OrderListManager orderListManager;
    public GameObject foodObject;
    //EditPathScript toReject;
    private bool readyToRespawn;
    // Use this for initialization
    void Start ()
    {
        truckManager = GameObject.FindGameObjectWithTag("TruckManager").GetComponent<TruckManager>();
        orderListManager = GameObject.FindGameObjectWithTag("OrderListManager").GetComponent<OrderListManager>();
        //toReject = GameObject.FindGameObjectWithTag("Reject").GetComponent<EditPathScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if theres something in the truck
        if (TruckManager.foodList[index] != null && TruckManager.foodList[index].foodName != null)
        {
            food = TruckManager.foodList[index];
            foodObject.SetActive(true);

            if (foodObject.gameObject.GetComponent<MoveOnPath>().waypointDone)
            {
                if (foodObject.gameObject.GetComponent<MoveOnPath>().PathToFollow == GameObject.FindGameObjectWithTag("Reject").GetComponent<EditPathScript>())
                    readyToRespawn = true;

                if(readyToRespawn)
                {
                    RespawnTruckV2();
                }

                TruckManager.foodList[index] = new Food();
                foodObject.gameObject.GetComponentInParent<Truck>().food = new Food();
                foodObject.gameObject.GetComponent<MoveOnPath>().PathToFollow = null;
                foodObject.gameObject.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
                foodObject.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                foodObject.gameObject.GetComponent<MoveOnPath>().waypointDone = false;
                foodObject.SetActive(false);
                
            }
        }
        //if theres nothing in the truck
        else
        {
            foodObject.SetActive(false);
        }
    }
    
    void RespawnTruckV2()
    {
        truckManager.ChangeTruckFood(orderListManager.transform.GetChild(0).GetComponent<Order>().food, index);
        //string ChangeTo = orderListManager.transform.GetChild(orderListManager.transform.childCount - index - 1).GetComponent<Order>().food.foodName;
        //string Original = truckManager.transform.GetChild(index).GetComponent<Truck>().food.foodName;


        //Debug.Log("From " + Original + " to " + ChangeTo + " (Order Reference is " + ChangeTo + ")");   

        //if (ChangeTo != truckManager.transform.GetChild(index).GetComponent<Truck>().food.foodName)
        //    Debug.Log("DID NOT CHANGE TO GOAL YOU FK! THE TRUCK IS " + truckManager.transform.GetChild(index).GetComponent<Truck>().food.foodName);
        readyToRespawn = false;
    }
}
