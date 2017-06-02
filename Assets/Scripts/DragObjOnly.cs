using UnityEngine;
using System.Collections;

public class DragObjOnly : MonoBehaviour {


    //distance away from camera
    private float distance = 5f;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
    }

    void OnMouseDown()
    {

    }

    void OnMouseDrag()
    {
      
            //change mouseScreen position to object position on z-axis
            Vector3 onMousePosition = new Vector3(Input.mousePosition.x,  Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(onMousePosition);

            transform.position = objPosition;
        
    }
}
