using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class MeatFabManager : MonoBehaviour {

    public static MeatFabManager Instance;

    TYPE_OF_MEAT meatType;
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

        startSuccess = false;
        endSuccess = false;
        cutFail = false;

        meatType = TYPE_OF_MEAT.DEFAULT;

        //ChickenParts.Add(new Vector2(0,0));
    }

    

    // Update is called once per frame
    void Update () {

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

    public void SetTypeOfMeat(string type)
    {
        MeatSelectionUI.options.Clear();
        MeatSelectionUI.value = 0;

        switch (type)
        {
            case "chicken":
                meatType = TYPE_OF_MEAT.CHICKEN;
                ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/1_Full Chicken");

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Head"));

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Left Foot"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Right Foot"));

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Left Back"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Right Back"));

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Left Breast"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Right Breast"));

                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Left Thigh"));
                MeatSelectionUI.options.Add(new TMP_Dropdown.OptionData("Chicken Right Thigh"));
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

       // CheckIfCutIsCorrect();
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
                //left foot
                case 1:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/2_Headless Chicken");
                    break;
                //right foot
                case 2:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/2_Headless Chicken");
                    break;
                //left back
                case 3:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/3_Legless Chicken");
                    break;
                //right back
                case 4:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/4_Back Cut 1 Chicken");
                    break;
                //left breast
                case 5:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/6_Chicken Chest");
                    break;
                //right break
                case 6:
                    ParentOfSlicedObjects.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MeatFabrication/Chicken/7_Chicken Chest Cut 1");
                    break;
            }
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
        Destroy(ParentOfSlicedObjects.GetComponentInChildren<PolygonCollider2D>());
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
