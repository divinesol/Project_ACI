using UnityEngine;
using System.Collections;

public class MeatFabricationData {

    public string FabName;
    public float startCutPointX, endCutPointX, startCutPointY, endCutPointY;
    public int FabNumOfCuts;

    public MeatFabricationData(string name, float startPosX, float endPosX, float startPosY, float endPosY, int numOfCuts)
    {
        FabName = name;
        startCutPointX = startPosX;
        endCutPointX = endPosX;
        startCutPointY = startPosY;
        endCutPointY = endPosY;
        FabNumOfCuts = numOfCuts;
    }

    public MeatFabricationData()
    {

    }
}
