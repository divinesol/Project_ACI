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

    private Food temp;

    // Use this for initialization
    void Start()
    {
        truckManager = GameObject.FindGameObjectWithTag("TruckManager").GetComponent<TruckManager>();
        orderListManager = GameObject.FindGameObjectWithTag("OrderListManager").GetComponent<OrderListManager>();
        //toReject = GameObject.FindGameObjectWithTag("Reject").GetComponent<EditPathScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //if theres something in the truck
        if (TruckManager.foodList[index] != null && TruckManager.foodList[index].foodName != null)
        {
            food = TruckManager.foodList[index];
            foodObject.SetActive(true);
            //foodObject.GetComponent<MeshFilter>().sharedMesh = truckManager.foodList[index].foodPrefab.GetComponent<MeshFilter>().sharedMesh;
            //foodObject.GetComponent<MeshRenderer>().sharedMaterial = truckManager.foodList[index].foodPrefab.GetComponent<MeshRenderer>().sharedMaterial;

            //if(foodObject.gameObject.GetComponent<MoveOnPath>().CurrentWayPointID >= 2)
            if (foodObject.gameObject.GetComponent<MoveOnPath>().waypointDone)
            {
                if (foodObject.gameObject.GetComponent<MoveOnPath>().PathToFollow == GameObject.FindGameObjectWithTag("Reject").GetComponent<EditPathScript>())
                    readyToRespawn = true;

                TruckManager.foodList[index] = new Food();
                foodObject.gameObject.GetComponentInParent<Truck>().food = new Food();
                foodObject.gameObject.GetComponent<MoveOnPath>().PathToFollow = null;
                foodObject.gameObject.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
                foodObject.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                foodObject.gameObject.GetComponent<MoveOnPath>().waypointDone = false;
                foodObject.SetActive(false);

                if (readyToRespawn)
                {
                    //truckManager.AddFoodToTruck(orderListManager.transform.GetChild(index).GetComponent<Order>().food);
                    for(int i = 0; i < orderListManager.transform.childCount; i++)
                    {
                        for(int j = 0; j < truckManager.transform.childCount; j++)
                        {
                            if(orderListManager.transform.GetChild(i).GetComponentInChildren<Order>().food != truckManager.transform.GetChild(j).GetComponent<Truck>().food)
                            {
                                temp = orderListManager.transform.GetChild(i).GetComponentInChildren<Order>().food;
                            }
                        }
                    }
                    truckManager.AddFoodToTruck(temp);
                    readyToRespawn = false;
                }
            }
        }
        //if theres nothing in the truck
        else
        {
            foodObject.SetActive(false);
        }
    }
}