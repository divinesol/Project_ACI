using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FabricationDatabase : MonoBehaviour{

    public List<MeatFabricationData> FabList = new List<MeatFabricationData>();

    // Use this for initialization
    void Start() { 

        FabList.Add(new MeatFabricationData("Beef", -3.2f, 2.3f, 4.2f, 0.2f, 2));
        FabList.Add(new MeatFabricationData("Chicken", 2.9f, 3.3f, 2.5f, 1.4f, 2));
        FabList.Add(new MeatFabricationData("Fish", 5.0f, 5.0f, 5.0f, 5.0f, 2));
        FabList.Add(new MeatFabricationData("Crab", 5.0f, 5.0f, 5.0f, 5.0f, 2));

    }

}
