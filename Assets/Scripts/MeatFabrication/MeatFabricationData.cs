using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeatFabricationData : MonoBehaviour{

    string FabName;
    Vector2 startCutPoint, endCutPoint;
    int FabNumOfCuts;

    public MeatFabricationData(string name, Vector2 startPos, Vector2 endPos, int numOfCuts)
    {
        FabName = name;
        startCutPoint = startPos;
        endCutPoint = endPos;
        FabNumOfCuts = numOfCuts;
    }

}
