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
    public bool startSuccess, endSuccess, cutFail;
    public float startBaseValue_X, startBaseValue_Y, endBaseValue_X, endBaseValue_Y, range;
    public int numOfCuts;
    public TMP_Dropdown MeatSelectionUI;
    public Transform ParentOfSlicedObjects;
    public GameObject SlicePrefab;

    public GameObject correctResultTab, wrongResultTab;
    public TextMeshProUGUI correctResultText, wrongResultText;

    public List<Vector2> ChickenParts;

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

        range = 0.5f;
        selection = 0;

        startSuccess = false;
        endSuccess = false;
        cutFail = false;

        meatCutTypes = MEAT_CUT_TYPE.BEEF_TEST;

        ChickenParts.Add(new Vector2(0,0));
    }
	
	// Update is called once per frame
	void Update () {

        //UpdateSelection();

        Debug.Log(database.FabList[selection].FabNumOfCuts);

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //Debug.Log("Target Position: " + hit.point);
        if (Input.GetMouseButtonDown(0))
        {
            if(numOfCuts > 0)
            {
                if (hit.collider != null)
                {
                    if(database.FabList[selection].FabName == "Chicken")
                    {

                    }

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
                        else
                        {
                            startSuccess = false;
                        }
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
                    //Debug.Log("Target Position: " + hit.point);
                    //x = -3.2 base
                    if (hit.point.x < endBaseValue_X + range && hit.point.x > endBaseValue_X - range)
                    {
                        //y = 4.2 base
                        if (hit.point.y < endBaseValue_Y + range && hit.point.y > endBaseValue_Y - range)
                        {
                            Debug.Log("End Success!");
                            endSuccess = true;
                            //popup.SetActive(true);
                            numOfCuts--;
                        }
                        else
                        {
                            endSuccess = false;
                            cutFail = true;
                        }
                    }
                }
            }
            
        }

        if(numOfCuts == 0 && startSuccess && endSuccess)
        {
            correctResultTab.SetActive(true);

        }

        else if(cutFail)
        {
            Debug.Log("CUT FAIL");
            wrongResultTab.SetActive(true);
            cutFail = false;
        }

    }

    private void ValueChange(TMP_Dropdown g_dropdown)
    {
    }

    public void UpdateSelection()
    {
        switch(MeatSelectionUI.value)
        {
            case 1:
                selection = 0;
                break;
            case 2:
                selection = 1;
                break;
            case 3:
                selection = 2;
                break;
            case 4:
                selection = 3;
                break;
        }
        CheckIfCutIsCorrect();
    }

    public void CheckIfCutIsCorrect()
    {
        switch (database.FabList[selection].FabName)
        {
            //beef
            case "Beef":
                ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/MEAT");
                break;
            //chicken
            case "Chicken":
                ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/1_Full Chicken Edited");
                break;
            //fish
            case "Fish":
                ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Fish/1_Full Fish");
                break;
            //crab
            case "Crab":
                ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Crab/1_Full Crab");
                break;
        }

        //startBaseValue_X = database.FabList[selection].startCutPointX;
        //startBaseValue_Y = database.FabList[selection].startCutPointY;
        //endBaseValue_X = database.FabList[selection].endCutPointX;
        //endBaseValue_Y = database.FabList[selection].endCutPointY;
        //numOfCuts = database.FabList[selection].FabNumOfCuts;

        //To reset collider whenever sprite is changed
        Destroy(ParentOfSlicedObjects.GetComponentInChildren<PolygonCollider2D>());
        //sliceableObject.AddComponent<PolygonCollider2D>();
        ParentOfSlicedObjects.GetChild(0).gameObject.AddComponent<PolygonCollider2D>();
    }

    public void ResetSliceableObjects()
    {
        foreach(Transform child in ParentOfSlicedObjects)
        {
            Destroy(child.gameObject);
        }

        GameObject go = Instantiate(SlicePrefab);
        go.transform.SetParent(ParentOfSlicedObjects);

        MeatSelectionUI.value = 0;
        ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = null;
    }

  
}
