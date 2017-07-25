﻿using UnityEngine;
using System.Collections;

public class AI_Group : MonoBehaviour {

    public Transform GroupTable;
    public int GroupSize = 0;
    public bool GroupSeated = false;
    public int GrpSeatedCount = 0;

    public GameObject Grim;

    // Use this for initialization
    void Start () {
        GroupSeated = false;
        GroupSize = 0;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Transform child in transform)
        {
            if (Vector3.Distance(child.transform.position, GameObject.Find("AI_PATH_ENDS").transform.position) <= 1.0f)
            {
                Debug.Log("DESTROYING CUSTOMERS");
                Destroy(child.gameObject);
            }
        }

    }

    public bool IsEveryoneSeated()
    {
        foreach (Transform child in transform)
        {
            //True if not Seated
            if (!child.GetComponent<AI_State>().Seated)
                 return false;//Break this Function
        }
        return true;
    }

    public IEnumerator WalkingToTable()
    {
        //Loops if AI in gruop not seated
        while (!IsEveryoneSeated())
            yield return null;

        // -- Everyone in Group Seated --
        Debug.Log("Everyone Seated!");


        //Add this Group as one of the Waiter's Process (1)
        WaiterAI_States.Instance.AddToProcess(GroupTable);
    }

    public void CustomerLeft()
    {
        //float closeEnough = 0.005f;

        Debug.Log("CUSTOMERS LEFT");
        foreach (Transform child in transform)
        {
            child.GetComponent<AI_State>().ChanceToGetPopPoints();
            child.GetComponent<AI_State>().Walk.SetBool("Sit", false);
            child.GetComponent<AI_State>().AINavMesh.enabled = true;
            if (child.GetComponent<AI_State>().Walk.GetCurrentAnimatorStateInfo(0).length > child.GetComponent<AI_State>().Walk.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                child.GetComponent<AI_State>().Walk.SetBool("Leave", true);
            }
            child.GetComponent<AI_State>().AINavMesh.SetDestination(GameObject.Find("AI_PATH_ENDS").transform.position);
            //Debug.Log(GameObject.Find("AI_PATH_ENDS").transform.position);
            //Debug.Log(child.GetComponent<AI_State>().AINavMesh.destination.z);
            //if (child.GetComponent<AI_State>().Ai_Path_Ends.transform.position == child.GetComponent<Transform>().position && child.GetComponent<AI_State>().AINavMesh.remainingDistance < 5)
            //if(Mathf.Abs(child.GetComponent<NavMeshAgent>().transform.position.x - -3.1f) <= closeEnough && Mathf.Abs(child.GetComponent<AI_State>().AINavMesh.transform.position.z - -4.0f) <= closeEnough)
            if(Vector3.Distance(child.transform.position, GameObject.Find("AI_PATH_ENDS").transform.position) <= 6.0f)
            {
                //Debug.Log("DESTROYING CUSTOMERS");
                //Destroy(child.gameObject);
            }
        }

        RestaurantSpawner.Instance.AI_Count--;
        GroupTable.GetComponent<Table>().UnoccupyTable();
       // Grim.GetComponent<GrimDirtManager>().enabled = true;
    }

}
