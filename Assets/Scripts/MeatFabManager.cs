using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class MeatFabManager : MonoBehaviour {

    public static MeatFabManager Instance;

    TYPE_OF_MEAT meatType;

    public GameObject popup;
    public bool startSuccess, endSuccess, cutFail;
    public float startBaseValue_X, startBaseValue_Y, endBaseValue_X, endBaseValue_Y, range;

    public TMP_Dropdown MeatSelectionUI;
    public Transform ParentOfSlicedObjects;

    public GameObject SlicePrefab;

    public GameObject correctResultTab, wrongResultTab;
    public TextMeshProUGUI correctResultText, wrongResultText;
 

    public enum TYPE_OF_MEAT
    {
        DEFAULT,
        CHICKEN,
        FISH,
        CRAB
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

        startBaseValue_X = 0;
        endBaseValue_X = 0;
        startBaseValue_Y = 0;
        endBaseValue_Y = 0;

        startSuccess = false;
        endSuccess = false;
        cutFail = false;

        meatType = TYPE_OF_MEAT.DEFAULT;

        //ChickenParts.Add(new Vector2(0,0));
        
    }

    // Update is called once per frame
    void Update () {

        //slicedObject = ParentOfSlicedObjects.GetChild(0).gameObject;

        //UpdateSelection();
        if (selection != MeatSelectionUI.value)
        {
            selection = MeatSelectionUI.value;
            CheckIfCutIsCorrect();
        }   

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //Debug.Log("Target Position: " + hit.point);
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                Debug.Log("Target Position: " + hit.point);
                if (hit.point.x < startBaseValue_X + range && hit.point.x > startBaseValue_X - range)
                {
                    if (hit.point.y < startBaseValue_Y + range && hit.point.y > startBaseValue_Y - range)
                    {
                        Debug.Log("startX: "+hit.point.x+"endX: "+hit.point.y);
                        Debug.Log("Start Success!");
                        startSuccess = true;
                    }
                    else
                    {
                        startSuccess = false;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (hit.collider != null)
            {
                if (hit.point.x < endBaseValue_X + range && hit.point.x > endBaseValue_X - range)
                {
                    Debug.Log("Target Position: " + hit.point);
                    if (hit.point.y < endBaseValue_Y + range && hit.point.y > endBaseValue_Y - range)
                    {
                        Debug.Log("startY: " + hit.point.x + "endY: " + hit.point.y);
                        Debug.Log("End Success!");
                        endSuccess = true;

                    }
                    else
                    {
                        endSuccess = false;
                        cutFail = true;
                    }
                }
            }
        }

        if(startSuccess && endSuccess)
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

    public void SetTypeOfMeat(string type)
    {
        MeatSelectionUI.options.Clear();
        switch (type)
        {
            case "chicken":
                meatType = TYPE_OF_MEAT.CHICKEN;
                ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/1_Full Chicken");

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Head"));

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Both Foot"));

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Left Back"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Right Back"));

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Left Breast"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Right Breast"));

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Thigh"));
                break;
            case "fish":
                meatType = TYPE_OF_MEAT.FISH;
                ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Fish/1_Full Fish");

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Fish Left Side"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Fish Left Side"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Debone Fish"));
                break;
            case "crab":
                meatType = TYPE_OF_MEAT.CRAB;
                ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Crab/1_Full Crab");

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Kill Crab"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Remove shell"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Cut Crab into 2"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Cut Left Pincer"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Cut Right Pincer"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Remove Gills"));
                break;

        }
        MeatSelectionUI.value = 0;
        MeatSelectionUI.RefreshShownValue();
        CheckIfCutIsCorrect();
    }

    public void CheckIfCutIsCorrect()
    {
        if (meatType == TYPE_OF_MEAT.CHICKEN)
        {
            switch (selection)
            {
                //head
                case 0:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/1_Full Chicken");
                    break;
                //both foot
                case 1:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/2_Headless Chicken");
                    break;
                //left back
                case 2:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/3_Legless Chicken");
                    break;
                //right back
                case 3:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/4_Back Cut 1 Chicken");
                    break;
                //left breast
                case 4:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/6_Chicken Chest");
                    break;
                //right breast
                case 5:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/7_Chicken Chest Cut 1");
                    break;
                case 6:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/9_Half Chicken");
                    break;
            }

            //slicedObject.AddComponent<PolygonCollider2D>();

            startBaseValue_X = database.ChickenParts[selection].startCutPointX;
            endBaseValue_X = database.ChickenParts[selection].endCutPointX;
            startBaseValue_Y = database.ChickenParts[selection].startCutPointY;
            endBaseValue_Y = database.ChickenParts[selection].endCutPointY;
            
            //Debug.Log("startX:"+database.ChickenParts[selection].startCutPointX+ "endX:" +database.ChickenParts[selection].endCutPointX+ "startY:" + database.ChickenParts[selection].startCutPointY + "endY:" + database.ChickenParts[selection].endCutPointY);
        }
        //switch (database.FabList[selection].FabName)
        //{
        //    //beef
        //    case "Beef":
        //        ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/MEAT");
        //        break;
        //    //chicken
        //    case "Chicken":
        //        ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/1_Full Chicken Edited");
        //        break;
        //    //fish
        //    case "Fish":
        //        ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Fish/1_Full Fish");
        //        break;
        //    //crab
        //    case "Crab":
        //        ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Crab/1_Full Crab");
        //        break;
        //}

        //startBaseValue_X = database.FabList[selection].startCutPointX;
        //startBaseValue_Y = database.FabList[selection].startCutPointY;
        //endBaseValue_X = database.FabList[selection].endCutPointX;
        //endBaseValue_Y = database.FabList[selection].endCutPointY;
        //numOfCuts = database.FabList[selection].FabNumOfCuts;

        //reset collider on sprite change
        //if (!slicedObject.GetComponent<PolygonCollider2D>())
        //{
        //    Debug.Log("DELETED POLYGON");
        //    //Destroy(slicedObject.GetComponent<PolygonCollider2D>());
        //    //slicedObject.GetComponent<PolygonCollider2D>() = new PolygonCollider2D();
        //    //slicedObject.AddComponent<PolygonCollider2D>();
        //    slicedObject.AddComponent<PolygonCollider2D>();
        //}

       
    }

    public void ResetSliceableObjects()
    {
        Debug.Log("RESET");
        //foreach(Transform child in ParentOfSlicedObjects)
        //{
        //    Debug.Log("AIUSGFBIYSAFGIUAFGI");
        //    Destroy(child.gameObject);
        //}

        for(int i = ParentOfSlicedObjects.childCount; i > 0; i--)
        {
            Debug.Log("AIUSGFBIYSAFGIUAFGI");
            Destroy(ParentOfSlicedObjects.GetChild(i-1).gameObject);
        }

        GameObject go = Instantiate(SlicePrefab);
        go.transform.SetParent(ParentOfSlicedObjects);
        
    }

    public void ProceedToNextStep()
    { 
        ResetSliceableObjects();
        switch(meatType)
        {
            case TYPE_OF_MEAT.CHICKEN:
                //Debug.Log("first selection:" + selection);
                if (selection < database.ChickenParts.Count)
                {
                    selection++;
                    MeatSelectionUI.value = selection;
                    //Debug.Log("final selection:" +selection);
                }
                break;
            case TYPE_OF_MEAT.FISH:

                break;
        }
        //if(!slicedObject.GetComponent<PolygonCollider2D>())
            //slicedObject.AddComponent<PolygonCollider2D>();

        Debug.Log("Before check cut");
        CheckIfCutIsCorrect();
    }
  
}
