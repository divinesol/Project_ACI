using UnityEngine;
using System.Collections;

public class Chef_AI : MonoBehaviour {
    public static Chef_AI Instance;

    public WaiterAI_States Waiter;
    public KitStocks kit;
    public float Timer = 5;
    public GameObject SentFood, Cooking, GetStocks, OrderUI;
    public NavMeshAgent ChefNavMesh;
    public int ToCook, CookedFood;
    public Chef_Meat MeatChef;
    bool PrepareFood;

    public Animator ChefAnimation;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        PrepareFood = false;
	}
	
	// Update is called once per frame
	void Update () {
        ReceiveOrder();
    }

    public void ReceiveOrder()
    {
        if (ToCook > 0 && ! PrepareFood)
        {
            TimeToCook();
        }
        for (int i = 0; i < ToCook; ++i)
        {
            OrderUI.SetActive(true);
        }
    }

    public IEnumerator OrderPopups(float a)
    {
        yield return new WaitForSeconds(a);
        OrderUI.SetActive(false);

    }
    public void TimeToCook()
    {
        PrepFood(true);
        ChefNavMesh.SetDestination(SentFood.transform.position);
        ChefAnimation.SetBool("CookWalk", true);
        if (PathComplete())
            OrderUI.SetActive(false);
        StartCoroutine(MakeFood());
    }
    public IEnumerator MakeFood()
    {
        while (!PathComplete())
            yield return null;

        ChefNavMesh.SetDestination(Cooking.transform.position);
        ChefAnimation.SetBool("CookWalk", false);
        MeatChef.GetStocks();
        //End
        ToCook--;
        CookedFood++;

    }

    public bool PrepFood(bool r)
    {
        PrepareFood = r;
        return r;
    }

    public bool PathComplete()
    {
        if (!ChefNavMesh.pathPending)
        {
            if (ChefNavMesh.remainingDistance <= ChefNavMesh.stoppingDistance + 0.1f)
            {
                ChefAnimation.SetBool("CookWalk", false);
                return true;
            }
        }
        return false;
    }
}

