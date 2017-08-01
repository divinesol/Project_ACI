using UnityEngine;
using System.Collections;

//For storing classes of the different cuts of meat
public class MeatFabricationData {

    //public string FabName;
    //public float startCutPointX, endCutPointX, startCutPointY, endCutPointY;
    //public int FabNumOfCuts;

    //public MeatFabricationData(string name, float startPosX, float endPosX, float startPosY, float endPosY, int numOfCuts)
    //{
    //    FabName = name;
    //    startCutPointX = startPosX;
    //    endCutPointX = endPosX;
    //    startCutPointY = startPosY;
    //    endCutPointY = endPosY;
    //    FabNumOfCuts = numOfCuts;
    //}

    //public MeatFabricationData()
    //{

    //}
}

//Class for the different cuts of chicken
public class ChickenCuts
{
    public string ChickenName;
    public float startCutPointX, endCutPointX, startCutPointY, endCutPointY;

    public ChickenCuts(string name, float startPosX, float startPosY, float endPosX, float endPosY)
    {
        ChickenName = name;
        startCutPointX = startPosX;
        startCutPointY = startPosY;
        endCutPointX = endPosX;
        endCutPointY = endPosY;
    }

    public ChickenCuts()
    {

    }
}
//Class for the different cuts of shellfish
public class ShellfishCuts
{
    public string ShellfishName;
    public float startCutPointX, endCutPointX, startCutPointY, endCutPointY;

    public ShellfishCuts(string name, float startPosX, float startPosY, float endPosX, float endPosY)
    {
        ShellfishName = name;
        startCutPointX = startPosX;
        startCutPointY = startPosY;
        endCutPointX = endPosX;
        endCutPointY = endPosY;
    }

    public ShellfishCuts()
    {

    }
}
//Class for the different cuts of fish
public class FishCuts
{
    public string FishName;
    public float startCutPointX, endCutPointX, startCutPointY, endCutPointY;

    public FishCuts(string name, float startPosX, float startPosY, float endPosX, float endPosY)
    {
        FishName = name;
        startCutPointX = startPosX;
        startCutPointY = startPosY;
        endCutPointX = endPosX;
        endCutPointY = endPosY;
    }

    public FishCuts()
    {

    }
}
