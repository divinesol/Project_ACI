using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeatFabManager : MonoBehaviour {

    public static MeatFabManager Instance;

    MEAT_CUT_TYPE meatCutTypes;
    public GameObject sliceableObject;
    public GameObject popup;
    public bool startSuccess, endSuccess;
    public float startBaseValue_X, startBaseValue_Y, endBaseValue_X, endBaseValue_Y, range;
    public int numOfCuts;

    public enum MEAT_CUT_TYPE
    {
        CHICKEN_MAIN,
        CHICKEN_THIGH,
        BEEF_TEST

    };

    public List<MeatFabricationData> FabList = new List<MeatFabricationData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {

        startBaseValue_X = -3.2f;
        startBaseValue_Y = 4.2f;
        endBaseValue_X = 2.3f;
        endBaseValue_Y = 0.2f;
        range = 0.5f;

        startSuccess = false;
        endSuccess = false;

        FabList.Add(new MeatFabricationData("Beef", new Vector2(0, 0), new Vector2(0, 0), 1));
        FabList.Add(new MeatFabricationData("Chicken", new Vector2(0, 0), new Vector2(0, 0), 2));

        meatCutTypes = MEAT_CUT_TYPE.BEEF_TEST;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Debug.Log("Target Position: " + hit.point);
        if (Input.GetMouseButtonDown(0))
        {
            if(numOfCuts > 0)
            {
                if (hit.collider != null)
                {
                    //x = -3.2 base
                    if (hit.point.x < startBaseValue_X + range && hit.point.x > startBaseValue_X - range)
                    {
                        //y = 4.2 base
                        if (hit.point.y < startBaseValue_Y + range && hit.point.y > startBaseValue_Y - range)
                        {
                            Debug.Log("Start Success!");
                            startSuccess = true;
                            //popup.SetActive(true);
                        }
                    }
                    else
                    {
                        //popup.SetActive(false);
                    }
                }
            }
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(numOfCuts > 0)
            {
                if (hit.collider != null)
                {
                    Debug.Log("Target Position: " + hit.point);
                    //x = -3.2 base
                    if (hit.point.x < endBaseValue_X + range && hit.point.x > endBaseValue_X - range)
                    {
                        //y = 4.2 base
                        if (hit.point.y < endBaseValue_Y + range && hit.point.y > endBaseValue_Y - range)
                        {
                            Debug.Log("End Success!");
                            endSuccess = true;
                            popup.SetActive(true);
                            numOfCuts--;
                        }
                    }
                    else
                    {
                        popup.SetActive(false);
                    }
                }
            }
            
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
                //case "Beef":
                //    MeatFabricationData._Instance.ChangeSprite(MeatFabricationData._Instance.currentFab.fabName);
                //    break;

                //case "Chicken":
                //    MeatFabricationData._Instance.ChangeSprite(MeatFabricationData._Instance.currentFab.fabName);
                //    break;
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
                //case "Beef":
                //    MeatFabricationData._Instance.currentFab = MeatFabricationData._Instance.fabList[1];
                //    break;
                //case "Chicken":
                //    MeatFabricationData._Instance.currentFab = MeatFabricationData._Instance.fabList[0];
                //    break;
        }
        CheckIfCutIsCorrect();
    }
}
