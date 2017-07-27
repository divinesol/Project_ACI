using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FabricationDatabase : MonoBehaviour{

    public List<MeatFabricationData> FabList = new List<MeatFabricationData>();
    public List<ChickenCuts> ChickenParts = new List<ChickenCuts>();

    // Use this for initialization
    void Start() { 

        FabList.Add(new MeatFabricationData("Beef", -3.2f, 2.3f, 4.2f, 0.2f, 2));
        FabList.Add(new MeatFabricationData("Chicken", 2.9f, 3.3f, 2.5f, 1.4f, 2));
        FabList.Add(new MeatFabricationData("Fish", 5.0f, 5.0f, 5.0f, 5.0f, 2));
        FabList.Add(new MeatFabricationData("Crab", 5.0f, 5.0f, 5.0f, 5.0f, 2));

        ChickenParts.Add(new ChickenCuts("Chicken Head", 3.2f, 1.7f, 3.5f, -1.0f));
        ChickenParts.Add(new ChickenCuts("Chicken Both Foot", -2.6f, 2.6f, -2.2f, -2.4f));
        ChickenParts.Add(new ChickenCuts("Chicken Left Back", -1.5f, 0.8f, 2.4f, 0.6f));
        ChickenParts.Add(new ChickenCuts("Chicken Right Back", -2.3f, -0.5f, 2.6f, -0.5f));
        ChickenParts.Add(new ChickenCuts("Chicken Left Breast", -0.8f, 0.6f, 2.6f, 0.7f));
        ChickenParts.Add(new ChickenCuts("Chicken Right Breast", -0.9f, -0.6f, 3.1f, -0.8f));

        ChickenParts.Add(new ChickenCuts("Chicken Thigh", 2.9f, 3.3f, 2.5f, 1.4f));
    }

}
