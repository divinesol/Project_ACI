using UnityEngine;
using System.Collections;

public class MeatFabManager : MonoBehaviour {

    public static MeatFabManager Instance;

    MEAT_CUT_TYPE meatCutTypes;

    Vector3 startCutPoint, endCutPoint;

    public GameObject sliceableObject;

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
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
 
        //if(hit.collider != null)
        {
            Debug.Log ("Target Position: " + hit.point);
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
        //To reset collider whenever sprite is changed
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
