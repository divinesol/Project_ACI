using UnityEngine;
using System.Collections;

public class MeatFabManager : MonoBehaviour {

    public static MeatFabManager Instance;

    MEAT_CUT_TYPE meatCutTypes;

    Vector3 startCutPoint, endCutPoint;

    public GameObject sliceableObject;

    /*Collider sizes
            x  | y  
               |
    Beef = 4.5,| 3
               |
    */
    public enum MEAT_CUT_TYPE
    {
        CHICKEN_MAIN,
        CHICKEN_THIGH,
        BEEF_TEST

    };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        meatCutTypes = MEAT_CUT_TYPE.BEEF_TEST;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {

        }
        
    }

    public void CheckIfCutIsCorrect()
    {
        switch (meatCutTypes)
        {
            case MEAT_CUT_TYPE.BEEF_TEST:
                
                sliceableObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/MEAT");
                break;

            case MEAT_CUT_TYPE.CHICKEN_MAIN:
              
                sliceableObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/CHICKEN");
                break;

            case MEAT_CUT_TYPE.CHICKEN_THIGH:
                break;
        }

        Destroy(sliceableObject.GetComponent<PolygonCollider2D>());
        sliceableObject.AddComponent<PolygonCollider2D>();
    }

    public void test()
    {

        switch(meatCutTypes)
        {
            case MEAT_CUT_TYPE.BEEF_TEST:
                meatCutTypes = MEAT_CUT_TYPE.CHICKEN_MAIN;
                break;
            case MEAT_CUT_TYPE.CHICKEN_MAIN:
                meatCutTypes = MEAT_CUT_TYPE.BEEF_TEST;
                break;
        }

        

        CheckIfCutIsCorrect();
    }
}
