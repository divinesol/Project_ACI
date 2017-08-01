using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class MeatFabManager : MonoBehaviour {

    public static MeatFabManager Instance;

    public TYPE_OF_MEAT meatType;

    public bool startSuccess, endSuccess;
    private bool cutFail;
    public bool startFail, endFail;
    public float startBaseValue_X, startBaseValue_Y, endBaseValue_X, endBaseValue_Y, range;

    public TMP_Dropdown MeatSelectionUI;
    public Transform ParentOfSlicedObjects;

    public GameObject SlicePrefab;

    public GameObject correctResultTab, wrongResultTab;
    public TextMeshProUGUI correctResultText, wrongResultText, wrongResultHint;
    public Image correctResultImage;

    public enum TYPE_OF_MEAT
    {
        DEFAULT,
        CHICKEN,
        FISH,
        CRAB
    };

    public FabricationDatabase database;
    public int selection = 0;

    public bool UIActive, touchDown;

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

        range = 0.9f;
        selection = 0;
        UIActive = false;
        touchDown = false;
        startFail = false;
        endFail = false;
        
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

        endFail = false;

        //UpdateSelection();
        if (selection != MeatSelectionUI.value)
        {
            selection = MeatSelectionUI.value;
            CheckIfCutIsCorrect();
        }   

        if(correctResultTab.activeSelf || wrongResultTab.activeSelf)
        {
            UIActive = true;
        }
        else
        {
            UIActive = false;
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //Debug.Log("Target Position: " + hit.point);
        //mouse down

        if(meatType != TYPE_OF_MEAT.DEFAULT)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchDown = true;
                //hits collider + not in UI
                if (hit.collider != null && !UIActive)
                {
                    //Debug.Log("Target Position: " + hit.point);
                    //if hit start x range
                    if (hit.point.x < startBaseValue_X + range && hit.point.x > startBaseValue_X - range)
                    {
                        //if hit start y range
                        if (hit.point.y < startBaseValue_Y + range && hit.point.y > startBaseValue_Y - range)
                        {
                            //Debug.Log("startX: "+hit.point.x+"endX: "+hit.point.y);
                            startSuccess = true;
                            startFail = false;
                        }
                        else
                        {
                            startFail = true;
                            startSuccess = false;
                        }
                    }
                    //if doesnt hit
                    else
                    {
                        startFail = true;
                        startSuccess = false;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0) && touchDown)
            {
                if (hit.collider != null && !UIActive)
                {
                    if (hit.point.x < endBaseValue_X + range && hit.point.x > endBaseValue_X - range)
                    {
                        //Debug.Log("Target Position: " + hit.point);
                        if (hit.point.y < endBaseValue_Y + range && hit.point.y > endBaseValue_Y - range)
                        {
                            //Debug.Log("startY: " + hit.point.x + "endY: " + hit.point.y);
                            endSuccess = true;
                            endFail = false;
                        }
                        else
                        {
                            endFail = true;
                            endSuccess = false;
                        }
                    }
                    else
                    {
                        endFail = true;
                        endSuccess = false;
                    }
                }
                touchDown = false;
            }
        }
        
        //if cut success
        if(startSuccess && endSuccess)
        {
            ShowCorrectResults();
        }
        //if cut fail
        if((startFail && endFail) || (startSuccess && endFail) || (startFail && endSuccess)) 
        {
            ShowWrongResults();
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
            startBaseValue_X = database.ChickenParts[selection].startCutPointX;
            endBaseValue_X = database.ChickenParts[selection].endCutPointX;
            startBaseValue_Y = database.ChickenParts[selection].startCutPointY;
            endBaseValue_Y = database.ChickenParts[selection].endCutPointY;           
        }
    }

    public void ResetSliceableObjects()
    {
        Debug.Log("RESET");

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

        Debug.Log("Before check cut");
        CheckIfCutIsCorrect();
    }
  
    public void ShowCorrectResults()
    {
        correctResultTab.SetActive(true);
        if(meatType == TYPE_OF_MEAT.CHICKEN)
        {
            switch (selection)
            {
                //finish cutting head
                case 0:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/2_Headless Chicken");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by removing the head";
                    break;
                //finish cutting both foot
                case 1:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/3_Legless Chicken");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by removing both of the feet";
                    break;
                //finish cutting left back
                case 2:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/4_Back Cut 1 Chicken");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by cutting the left side of the back";
                    break;
                //finish cutting right back
                case 3:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/5_Back Cut 2 Chicken");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by cutting the right side of the back";
                    break;
                //finish cutting left breast
                case 4:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/7_Chicken Chest Cut 1");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by cutting the left side of the breast";
                    break;
                //finish cutting right breast
                case 5:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/8_Chicken Chest Cut 2");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by cutting the right side of the breast";
                    break;
                case 6:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/9_Half Chicken");
                    break;
            }
        }
        
    }

    public void ShowWrongResults()
    {
        wrongResultTab.SetActive(true);
        if (meatType == TYPE_OF_MEAT.CHICKEN)
        {
            wrongResultText.text = "You have failed to fabricate the chicken";
            switch (selection)
            {
                //fail cutting head
                case 0:
                    wrongResultHint.text = "Hint : Start from the top of the chicken";
                    break;
                //fail cutting both foot
                case 1:
                    wrongResultHint.text = "Hint : Start from the bottom of the chicken";
                    break;
                //fail cutting left back
                case 2:
                    wrongResultHint.text = "Hint : IDK";
                    break;
                //fail cutting right back
                case 3:
                    wrongResultHint.text = "Hint : IDK";
                    break;
                //fail cutting left breast
                case 4:
                    wrongResultHint.text = "Hint : IDK";
                    break;
                //fail cutting right breast
                case 5:
                    wrongResultHint.text = "Hint : IDK";
                    break;
                case 6:
                    break;
            }
        }
    }

    public void ResetCutFail()
    {
        wrongResultTab.SetActive(false);
        startFail = false;
        endFail = false;
        startSuccess = false;
        endSuccess = false;
    }

}
