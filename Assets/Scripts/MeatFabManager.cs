using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class MeatFabManager : MonoBehaviour {

    public static MeatFabManager Instance;

    public FabricationDatabase database;

    public TYPE_OF_MEAT meatType;
    public TYPE_OF_FABRICATION fabricationType;

    public TextMeshProUGUI fabTypeText;

    //Dropdown Menu for selecting steps
    public TMP_Dropdown MeatSelectionDropdownUI;

    //Parent of the sliceableObject - Used for resetting the sliceable object
    public Transform ParentOfSlicedObjects;

    public GameObject SlicePrefab;

    //Result tab (Correct + Incorrect) | Result text (Correct + Incorrect) + Hint for Incorrect | Correct Result Image
    public GameObject correctResultTab, wrongResultTab;
    public TextMeshProUGUI correctResultText, wrongResultText, wrongResultHint;
    public Image correctResultImage;

    //After fully fabricating a meat
    public GameObject finishedFabricationTab;
    public TextMeshProUGUI finishedFabricationText;

    //Booleans to check for Start, End Success and Start, End Failures
    public bool startSuccess, endSuccess;
    public bool startFail, endFail;

    //Start Position, End Position and Range of Position
    public float startBaseValue_X, startBaseValue_Y, endBaseValue_X, endBaseValue_Y, range;

    public enum TYPE_OF_MEAT
    {
        DEFAULT,
        CHICKEN,
        FISH,
        SHELLFISH
    };

    public enum TYPE_OF_FABRICATION
    {
        DEFAULT,
        CUT_MODE,
        REMOVE_MODE,
        BREAK_MODE
    };

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
        MeatSelectionDropdownUI.onValueChanged.AddListener(delegate
        {
            ValueChange(MeatSelectionDropdownUI);
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

        fabricationType = TYPE_OF_FABRICATION.DEFAULT;
        meatType = TYPE_OF_MEAT.DEFAULT;
    }

    // Update is called once per frame
    void Update ()
    {
        //Constantly resets bool unless cut fails
        endFail = false;

        //UpdateSelection();
        if (selection != MeatSelectionDropdownUI.value)
        {
            selection = MeatSelectionDropdownUI.value;
            UpdateSliceableBeforeCut();
        }   

        //Check if UI is active - if UIActive, raycast for meat fabrication would be off
        if(correctResultTab.activeSelf || wrongResultTab.activeSelf || MeatSelectionDropdownUI.transform.childCount > 3)
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
                    Debug.Log("Target Position: " + hit.point);

                    //fabricationType == REMOVE / CUT
                    if (fabricationType != TYPE_OF_FABRICATION.BREAK_MODE)
                    {
                        //if hit start x range
                        if (hit.point.x < startBaseValue_X + range && hit.point.x > startBaseValue_X - range)
                        {
                            //if hit start y range
                            if (hit.point.y < startBaseValue_Y + range && hit.point.y > startBaseValue_Y - range)
                            {
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
                    //fabricationType == BREAK
                    else if (fabricationType == TYPE_OF_FABRICATION.BREAK_MODE)
                    {
                        //if hit start x range
                        if (hit.point.x < startBaseValue_X + range && hit.point.x > startBaseValue_X - range)
                        {
                            //if hit start y range
                            if (hit.point.y < startBaseValue_Y + range && hit.point.y > startBaseValue_Y - range)
                            {
                                startSuccess = true;
                                endSuccess = true;
                                startFail = false;
                                endFail = false;
                            }
                            else
                            {
                                startFail = true;
                                endFail = true;
                                startSuccess = false;
                                endSuccess = false;
                            }
                        }
                        //if doesnt hit
                        else
                        {
                            startFail = true;
                            endFail = true;
                            startSuccess = false;
                            endSuccess = false;
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0) && touchDown)
            {
                if (hit.collider != null && !UIActive)
                {
                    if (fabricationType != TYPE_OF_FABRICATION.BREAK_MODE)
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
        MeatSelectionDropdownUI.options.Clear();
        switch (type)
        {
            case "chicken":
                meatType = TYPE_OF_MEAT.CHICKEN;
                ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/1_Full Chicken");

                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Chicken Head"));
                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Chicken Both Foot"));
                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Chicken Left Back"));
                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Chicken Right Back"));
                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Chicken Left Breast"));
                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Chicken Right Breast"));
                break;
            case "fish":
                meatType = TYPE_OF_MEAT.FISH;
                ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Fish/1_Full Fish");

                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Fish Left Side"));
                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Debone Fish"));
                break;
            case "shellfish":
                meatType = TYPE_OF_MEAT.SHELLFISH;
                ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Crab/1_Full Crab");

                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Kill Crab"));
                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Remove shell"));
                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Cut Crab into 2"));
                MeatSelectionDropdownUI.options.Add(new TMP_Dropdown.OptionData("Cut Left Pincer"));
                break;

        }
        MeatSelectionDropdownUI.value = 0;
        MeatSelectionDropdownUI.RefreshShownValue();
        UpdateSliceableBeforeCut();
    }

    //Check and update sprites accordingly before cut
    public void UpdateSliceableBeforeCut()
    {
        Debug.Log("UpdateSliceableBeforeCut");
        if (meatType == TYPE_OF_MEAT.CHICKEN)
        {
            switch (selection)
            {
                //head
                case 0:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/1_Full Chicken");
                    fabricationType = TYPE_OF_FABRICATION.CUT_MODE;
                    break;
                //both foot
                case 1:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/2_Headless Chicken");
                    fabricationType = TYPE_OF_FABRICATION.CUT_MODE;
                    break;
                //left back
                case 2:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/3_Legless Chicken");
                    fabricationType = TYPE_OF_FABRICATION.CUT_MODE;
                    break;
                //right back
                case 3:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/4_Back Cut 1 Chicken");
                    fabricationType = TYPE_OF_FABRICATION.CUT_MODE;
                    break;
                //left breast
                case 4:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/6_Chicken Chest");
                    fabricationType = TYPE_OF_FABRICATION.CUT_MODE;
                    break;
                //right breast
                case 5:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/7_Chicken Chest Cut 1");
                    fabricationType = TYPE_OF_FABRICATION.CUT_MODE;
                    break;
            }

            if(selection < database.ChickenParts.Count)
            {
                startBaseValue_X = database.ChickenParts[selection].startCutPointX;
                endBaseValue_X = database.ChickenParts[selection].endCutPointX;
                startBaseValue_Y = database.ChickenParts[selection].startCutPointY;
                endBaseValue_Y = database.ChickenParts[selection].endCutPointY;
            }
            
        }
        else if (meatType == TYPE_OF_MEAT.SHELLFISH)
        {
            switch (selection)
            {
                //kill crab
                case 0:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Shellfish/1_Full Crab");
                    fabricationType = TYPE_OF_FABRICATION.CUT_MODE;
                    break;
                //remove shell
                case 1:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Shellfish/2.1_Full Crab Top");
                    fabricationType = TYPE_OF_FABRICATION.CUT_MODE;
                    break;
                //chop body in half
                case 2:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Shellfish/2.1_Full Crab Top");
                    fabricationType = TYPE_OF_FABRICATION.CUT_MODE;
                    break;
                //break claw
                case 3:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Shellfish/11_Crab Claw");
                    fabricationType = TYPE_OF_FABRICATION.BREAK_MODE;
                    break;
            }

            if(selection < database.ShellfishParts.Count)
            {
                startBaseValue_X = database.ShellfishParts[selection].startCutPointX;
                endBaseValue_X = database.ShellfishParts[selection].endCutPointX;
                startBaseValue_Y = database.ShellfishParts[selection].startCutPointY;
                endBaseValue_Y = database.ShellfishParts[selection].endCutPointY;
            }

        }
        else if (meatType == TYPE_OF_MEAT.FISH)
        {
            switch (selection)
            {
                //Stomach
                case 0:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Fish/1_Full Fish");
                    fabricationType = TYPE_OF_FABRICATION.CUT_MODE;
                    break;
                //Remove bone
                case 1:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Fish/2_Side Cutted Fish");
                    fabricationType = TYPE_OF_FABRICATION.REMOVE_MODE;
                    break;
            }

            if(selection < database.FishParts.Count)
            {
                startBaseValue_X = database.FishParts[selection].startCutPointX;
                endBaseValue_X = database.FishParts[selection].endCutPointX;
                startBaseValue_Y = database.FishParts[selection].startCutPointY;
                endBaseValue_Y = database.FishParts[selection].endCutPointY;
            }
            
        }

        UpdateFabricationTypeText();
    }

    public void ResetSliceableObjects()
    {
        Debug.Log("ResetSliceableObjects");

        for(int i = ParentOfSlicedObjects.childCount; i > 0; i--)
        {
            Destroy(ParentOfSlicedObjects.GetChild(i-1).gameObject);
        }

        GameObject go = Instantiate(SlicePrefab);
        go.transform.SetParent(ParentOfSlicedObjects);
    }

    public void ProceedToNextStep()
    {
        Debug.Log("ProceedToNextStep");
        ResetSliceableObjects();
        switch(meatType)
        {
            case TYPE_OF_MEAT.CHICKEN:
                //If there is still steps - proceed
                if (selection < database.ChickenParts.Count - 1)
                {
                    selection++;
                    MeatSelectionDropdownUI.value = selection;
                }
                //If there isn't any steps - proceed to congratulatory popup + reset gameobjects
                else
                {
                    finishedFabricationTab.SetActive(true);
                    finishedFabricationText.text = "Congratulations! You have successfully fabricated a chicken";

                    meatType = TYPE_OF_MEAT.DEFAULT;
                    fabricationType = TYPE_OF_FABRICATION.DEFAULT;
                    selection = 0;
                    MeatSelectionDropdownUI.ClearOptions();
                    MeatSelectionDropdownUI.RefreshShownValue();
                }
                break;
            case TYPE_OF_MEAT.SHELLFISH:
                //If there is still steps - proceed
                if (selection < database.ShellfishParts.Count - 1)
                {
                    selection++;
                    MeatSelectionDropdownUI.value = selection;
                }
                //If there isn't any steps - proceed to congratulatory popup + reset gameobjects
                else
                {
                    finishedFabricationTab.SetActive(true);
                    finishedFabricationText.text = "Congratulations! You have successfully fabricated a crab";

                    meatType = TYPE_OF_MEAT.DEFAULT;
                    fabricationType = TYPE_OF_FABRICATION.DEFAULT;
                    selection = 0;
                    MeatSelectionDropdownUI.ClearOptions();
                    MeatSelectionDropdownUI.RefreshShownValue();
                }
                break;
            case TYPE_OF_MEAT.FISH:
                //If there is still steps - proceed
                if (selection < database.FishParts.Count - 1)
                {
                    selection++;
                    MeatSelectionDropdownUI.value = selection;
                }
                //If there isn't any steps - proceed to congratulatory popup + reset gameobjects
                else
                {
                    finishedFabricationTab.SetActive(true);
                    finishedFabricationText.text = "Congratulations! You have successfully fabricated a fish";

                    meatType = TYPE_OF_MEAT.DEFAULT;
                    fabricationType = TYPE_OF_FABRICATION.DEFAULT;
                    selection = 0;
                    MeatSelectionDropdownUI.ClearOptions();
                    MeatSelectionDropdownUI.RefreshShownValue();
                }
                break;
        }
        
        UpdateSliceableBeforeCut();
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
        else if(meatType == TYPE_OF_MEAT.SHELLFISH)
        {
            switch (selection)
            {
                //finish killing crab
                case 0:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Shellfish/2_Dead Crab");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by removing the head";
                    break;
                //finish removing crab shell
                case 1:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Shellfish/3_Clawless Crab");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by removing both of the feet";
                    break;
                //finish cutting in half
                case 2:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Shellfish/4_Crab Legs and Claw");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by cutting the left side of the back";
                    break;
                //finish break claw
                case 3:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Shellfish/12_Crab Claw Cracked");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by cutting the right side of the back";
                    break;
                
            }
        }
        else if (meatType == TYPE_OF_MEAT.SHELLFISH)
        {
            switch (selection)
            {
                //finish stomach cut
                case 0:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Fish/2_Side Cutted Fish");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by removing the head";
                    break;
                //finish debone 
                case 1:
                    correctResultImage.sprite = Resources.Load<Sprite>("MeatFabrication/Fish/3_Debone Fish");
                    correctResultImage.SetNativeSize();
                    correctResultText.text = "You have successfully fabricated the chicken by removing both of the feet";
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
        else if (meatType == TYPE_OF_MEAT.SHELLFISH)
        {
            wrongResultText.text = "You have failed to fabricate the chicken";
            switch (selection)
            {
                //fail killing crab
                case 0:
                    wrongResultHint.text = "Hint : Start from the top of the chicken";
                    break;
                //fail removing crab shell
                case 1:
                    wrongResultHint.text = "Hint : Start from the bottom of the chicken";
                    break;
                //fail cutting in half
                case 2:
                    wrongResultHint.text = "Hint : IDK";
                    break;
                //fail breaking claw
                case 3:
                    wrongResultHint.text = "Hint : IDK";
                    break;
                
            }
        }
        else if (meatType == TYPE_OF_MEAT.FISH)
        {
            wrongResultText.text = "You have failed to fabricate the chicken";
            switch (selection)
            {
                //fail stomach cut
                case 0:
                    wrongResultHint.text = "Hint : Start from the top of the chicken";
                    break;
                //fail debone
                case 1:
                    wrongResultHint.text = "Hint : Start from the bottom of the chicken";
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

    public void UpdateFabricationTypeText()
    {
        Debug.Log("UpdateFabricationTypeText");
        switch(fabricationType)
        {
            case TYPE_OF_FABRICATION.DEFAULT:
                fabTypeText.text = " ";
                break;
            case TYPE_OF_FABRICATION.CUT_MODE:
                fabTypeText.text = "Cutting Mode";
                break;
            case TYPE_OF_FABRICATION.REMOVE_MODE:
                fabTypeText.text = "Removing Mode";
                break;
            case TYPE_OF_FABRICATION.BREAK_MODE:
                fabTypeText.text = "Breaking Mode";
                break;
        }
    }

}
