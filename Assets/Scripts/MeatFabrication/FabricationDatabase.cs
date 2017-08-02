﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FabricationDatabase : MonoBehaviour{

    public List<ChickenCuts> ChickenParts = new List<ChickenCuts>();
    public List<ShellfishCuts> ShellfishParts = new List<ShellfishCuts>();
    public List<FishCuts> FishParts = new List<FishCuts>();

    // Use this for initialization
    void Start() { 
        //Chicken
        ChickenParts.Add(new ChickenCuts("Chicken Head", 3.2f, 1.7f, 3.5f, -1.0f));
        ChickenParts.Add(new ChickenCuts("Chicken Both Foot", -2.6f, 2.6f, -2.2f, -2.4f));
        ChickenParts.Add(new ChickenCuts("Chicken Left Back", -1.5f, 0.8f, 2.4f, 0.6f));
        ChickenParts.Add(new ChickenCuts("Chicken Right Back", -2.3f, -0.5f, 2.6f, -0.5f));
        ChickenParts.Add(new ChickenCuts("Chicken Left Breast", -0.8f, 0.6f, 2.6f, 0.7f));
        ChickenParts.Add(new ChickenCuts("Chicken Right Breast", -0.9f, -0.6f, 3.1f, -0.8f));
        //ChickenParts.Add(new ChickenCuts("Chicken Thigh", 2.9f, 3.3f, 2.5f, 1.4f));

        //Shellfish
        ShellfishParts.Add(new ShellfishCuts("Kill Crab", 0.1f, 0.8f, 0.1f, -0.3f));
        ShellfishParts.Add(new ShellfishCuts("Remove Crab Shell", 0.2f, -1.5f, 0.5f, 1.5f));
        ShellfishParts.Add(new ShellfishCuts("Chop Crab in Half", 0.0f, 0.0f, 0.0f, 0.0f));
        ShellfishParts.Add(new ShellfishCuts("Break Crab Claw", -0.4f, 2.4f, 0.0f, 0.0f));

        //Fish
        FishParts.Add(new FishCuts("Fish Stomach", 0.0f, 0.0f, 0.0f, 0.0f));
        FishParts.Add(new FishCuts("Fish Debone", 0.0f, 0.0f, 0.0f, 0.0f));
    }

}
