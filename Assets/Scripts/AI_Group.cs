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
        
        foreach (Transform child in transform)
        {
            child.GetComponent<AI_State>().Walk.SetBool("Sit", false);
            child.GetComponent<AI_State>().AINavMesh.enabled = true;
            if (child.GetComponent<AI_State>().Walk.GetCurrentAnimatorStateInfo(0).length > child.GetComponent<AI_State>().Walk.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                child.GetComponent<AI_State>().Walk.SetBool("Leave", true);
            }
            child.GetComponent<AI_State>().AINavMesh.SetDestination(GameObject.Find("AI_PATH_ENDS").transform.position); 
        }

        RestaurantSpawner.Instance.AI_Count--;
        GroupTable.GetComponent<Table>().UnoccupyTable();
        Grim.GetComponent<GrimDirtManager>().enabled = true;
    }

}
