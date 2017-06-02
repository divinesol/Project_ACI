using UnityEngine;
using System.Collections;

public class SupplierInfo : MonoBehaviour
{


    [SerializeField]
    public int minRating;
    [SerializeField]
    public int maxRating;
    //public Tutorial tutorial;

    // Use this for initialization
    void Start()
    {
        //if (tutorial.tutDone)
        {
            int tempmin;
            minRating = tempmin = Random.Range(1, 3);
            maxRating = Random.Range(tempmin + 1, 6);
            //Debug.Log("temp:" + tempmin);
        }
        /*else
        {
            minRating = 3;
            maxRating = 5;
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
