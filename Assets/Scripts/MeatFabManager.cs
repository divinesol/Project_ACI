using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class MeatFabManager : MonoBehaviour {

    public static MeatFabManager Instance;

    MEAT_CUT_TYPE meatCutTypes;
    public GameObject sliceableObject;
    public GameObject popup;
    public bool startSuccess, endSuccess;
    public float startBaseValue_X, startBaseValue_Y, endBaseValue_X, endBaseValue_Y, range;
    public int numOfCuts;
    public TMP_Dropdown MeatSelectionUI;

    public enum MEAT_CUT_TYPE
    {
        CHICKEN_MAIN,
        CHICKEN_THIGH,
        BEEF_TEST

    };

    public FabricationDatabase database;
    public int selection = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start ()
    {
        MeatSelectionUI.onValueChanged.AddListener(delegate
        {
            ValueChange(MeatSelectionUI);
        });

        numOfCuts = 1;

        startBaseValue_X = -3.2f;
        startBaseValue_Y = 4.2f;
        endBaseValue_X = 2.3f;
        endBaseValue_Y = 0.2f;
        range = 0.5f;

        startSuccess = false;
        endSuccess = false;

        meatCutTypes = MEAT_CUT_TYPE.BEEF_TEST;
    }
	
	// Update is called once per frame
	void Update () {

        UpdateDropdown();

        Debug.Log(database.FabList[selection].FabName);

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Debug.Log("Target Position: " + hit.point);
        if (Input.GetMouseButtonDown(0))
        {
            //if(numOfCuts > 0)
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

    private void ValueChange(TMP_Dropdown g_dropdown)
    {
    }

    public void UpdateDropdown()
    {
        switch(MeatSelectionUI.value)
        {
            case 1:
                selection = 0;
                break;
            case 2:
                selection = 1;
                break;
        }
        CheckIfCutIsCorrect();
    }

    public void CheckIfCutIsCorrect()
    {
        switch (database.FabList[selection].FabName)
        {
            case "Beef":
                sliceableObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/MEAT");
                break;

            case "Chicken":
                sliceableObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/CHICKEN");
                break;
        }
        //To reset collider whenever sprite is changed
        Destroy(sliceableObject.GetComponent<PolygonCollider2D>());
        sliceableObject.AddComponent<PolygonCollider2D>();
    }

    public void test()
    {
        switch(database.FabList[selection].FabName)
        {
            case "Beef":
                selection = 1;
                break;
            case "Chicken":
                selection = 0;
                break;
        }
        CheckIfCutIsCorrect();
    }
}
