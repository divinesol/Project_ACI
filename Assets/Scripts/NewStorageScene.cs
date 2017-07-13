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
                switch (hit.collider.gameObject.name)
                {
                    case ("freezeDoor"):
                        mainCam.transform.position = new Vector3(-15.2f, 2, -4.2f);
                        mainCam.transform.rotation = Quaternion.Euler(15, 0, 0);
                        inStorageRoom = true;
                        break;
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
