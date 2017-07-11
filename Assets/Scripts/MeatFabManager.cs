using UnityEngine;
using System.Collections;

public class MeatFabManager : MonoBehaviour {

    public static MeatFabManager Instance;

    public enum MEAT_CUT_TYPE
    {
        CHICKEN_MAIN,
        CHICKEN_THIGH,
        BEEF_TEST

    };

    MEAT_CUT_TYPE meatCutTypes;

    Vector3 startCutPoint, endCutPoint;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckIfCutIsCorrect();
    }

    public void CheckIfCutIsCorrect()
    {
        switch (meatCutTypes)
        {
            case MEAT_CUT_TYPE.BEEF_TEST:
                break;

            case MEAT_CUT_TYPE.CHICKEN_MAIN:
                break;

            case MEAT_CUT_TYPE.CHICKEN_THIGH:
                break;
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit != false && hit.collider != null)
        {
            Debug.Log("object clicked: " + hit.collider.tag);
        }
    }
}
