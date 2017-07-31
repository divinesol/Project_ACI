using UnityEngine;
using System.Collections;

public class Chef_Meat : MonoBehaviour
{
    public static Chef_Meat Instance;

    public NavMeshAgent MeatNavMesh;
    public KitStocks KitStocks;
    public GameObject Stocks, GiveChef, FoodPrep;
    public bool CheckGetStocks;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckGetStocks)
        {
            if (PathComplete())
            { 
                //Debug.Log("Going to prepare food");
                KitStocks.ReduceStock();
                PrepareFood();
                CheckGetStocks = false;
            }
        }
    }
    public void GetStocks()
    {
        if (StocknPopularityManager.stockValue > 0)
        {
            //Debug.Log("I AM Getting Food");
            transform.rotation.Set(0, 180, 0,0);
            MeatNavMesh.updateRotation = false;
            //transform.rotation = Quaternion.Lerp(transform.rotation, Stocks.transform.rotation, Time.time * MeatNavMesh.speed);
            MeatNavMesh.SetDestination(Stocks.transform.position);
            CheckGetStocks = true;
        }
    }
    public void PrepareFood()
    {
       // Debug.Log("I AM PREPARING FOOD");
        MeatNavMesh.SetDestination(FoodPrep.transform.position);
        StartCoroutine(PrepTime(4));
        //Do Animation Here;
        //If Animation Ended

    }
    public void SentFood()
    {
        MeatNavMesh.SetDestination(GiveChef.transform.position);
        Chef_AI.Instance.PrepFood(false);
        if (PathComplete())
        {
            MeatNavMesh.SetDestination(FoodPrep.transform.position);
            //IDLE Animation
        }
    }

    public IEnumerator PrepTime(float a)
    {
        yield return new WaitForSeconds(a);
        SentFood();
    }

    public bool PathComplete()
    {
        //Debug.Log(MeatNavMesh.remainingDistance);

        
        if (!MeatNavMesh.pathPending)
        {
          
            if (Vector3.Distance(MeatNavMesh.destination, MeatNavMesh.transform.position) <= MeatNavMesh.stoppingDistance + 0.4f)
            {
                if (!MeatNavMesh.hasPath || MeatNavMesh.velocity.sqrMagnitude == 0f)
                {
                    //Debug.Log("PathComplete");
                    MeatNavMesh.updateRotation = false;
                    return true;
                }
            }
        }
        return false;
    }


}
