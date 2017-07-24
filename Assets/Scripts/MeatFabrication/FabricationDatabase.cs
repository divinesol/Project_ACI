using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FabricationDatabase : MonoBehaviour{

    public List<MeatFabricationData> FabList = new List<MeatFabricationData>();

    // Use this for initialization
    void Start() { 

        FabList.Add(new MeatFabricationData("Beef", 5.0f, 5.0f, 5.0f, 5.0f, 1));
        FabList.Add(new MeatFabricationData("Chicken", 5.0f, 5.0f, 5.0f, 5.0f, 2));

    }

}
