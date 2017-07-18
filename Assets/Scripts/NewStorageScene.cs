using UnityEngine;
using System.Collections;

public class NewStorageScene : MonoBehaviour {

    public Camera mainCam;

    bool inStorageRoom;
    // Use this for initialization
    void Start() {
        inStorageRoom = false;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButtonUp(0) && !inStorageRoom)
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
                switch (hit.collider.gameObject.tag)
                {
                    case ("freezeDoor"):
                        mainCam.transform.position = new Vector3(-15.2f, 2, -4.2f);
                        mainCam.transform.rotation = Quaternion.Euler(15, 0, 0);
                        inStorageRoom = true;
                        break;
                    //case ("Truck"):
                    //    {
                    //        //only works if the truck contain food and the storage icon is disable
                    //        if (hit.collider.gameObject.GetComponent<Truck>().food.foodPrefab != null &&
                    //        hit.collider.gameObject.GetComponent<Truck>() != null &&
                    //        hit.collider.gameObject.GetComponentInChildren<MoveOnPath>().PathToFollow == null &&
                    //        !selectionUI.activeSelf)
                    //        {
                    //            //hide UI
                    //            mainUI.SetActive(false);

                    //            //which truck is being selected
                    //            selectedTruck = hit.collider.gameObject;

                    //            //pop up the choice for player
                    //            //selectedTruckPopup.SetActive(true);
                    //            //selectedTruckPopup.transform.position = selectedTruck.transform.position;

                    //            //enable the truck info UI and get the truck data
                    //            truckInfo.SetActive(true);

                    //            //enable animation
                    //            truckInfoAnim.SetTrigger("Show");

                    //            //display the screenModel to the truck data
                    //            screenModelInfo.SetActive(true);
                    //            screenModelInfo.transform.GetChild(0).GetComponent<MeshFilter>().mesh = selectedTruck.gameObject.GetComponent<Truck>().food.foodPrefab.GetComponent<MeshFilter>().sharedMesh;
                    //            screenModelInfo.transform.GetChild(0).GetComponent<MeshRenderer>().material = selectedTruck.gameObject.GetComponent<Truck>().food.foodPrefab.GetComponent<MeshRenderer>().sharedMaterial;

                    //            //change the truck info text to the truck data
                    //            TruckInfoName.text = selectedTruck.gameObject.GetComponent<Truck>().food.foodName.ToString();
                    //            TruckInfoType.sprite = selectedTruck.gameObject.GetComponent<Truck>().food.foodIconType;

                    //            //change the OnChoosingPlacement 3D UI to the truck data
                    //            OnChoosingPlacement.transform.GetChild(0).GetComponent<MeshFilter>().mesh = selectedTruck.gameObject.GetComponent<Truck>().food.foodPrefab.GetComponent<MeshFilter>().sharedMesh;
                    //            OnChoosingPlacement.transform.GetChild(0).GetComponent<MeshRenderer>().material = selectedTruck.gameObject.GetComponent<Truck>().food.foodPrefab.GetComponent<MeshRenderer>().sharedMaterial;

                    //            //change the acceptedTruckInfo 3D UI to the truck data
                    //            acceptedTruckInfo.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<AcceptedFoodInfo>().food = selectedTruck.gameObject.GetComponent<Truck>().food;
                    //            acceptedTruckInfo.transform.GetChild(1).GetComponentInChildren<Text>().text = selectedTruck.gameObject.GetComponent<Truck>().food.foodName.ToString();
                    //        }
                    //    }
                    //    break;
                }
            }

        }
        else if (inStorageRoom)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                mainCam.transform.position = new Vector3(-0.5f, 3.05f, -6.22f);
                mainCam.transform.rotation = Quaternion.Euler(10, 0, 0);
                inStorageRoom = false;
            }
        }

    }
}
