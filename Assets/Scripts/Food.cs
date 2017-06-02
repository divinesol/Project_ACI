using UnityEngine;
using System.Collections;

[System.Serializable]
public class Food {

    public int foodID;
    public string foodName;
    public FoodType foodType;
    public double foodRarity;
    public GameObject foodPrefab;
    public Sprite foodIconType;
    public Material foodARImage;
    public FoodPlacement foodPlacement;

    public enum FoodType
    {
        DRY,
        COLD,
        FREEZE,
    }

    public enum FoodPlacement
    {
        TOP,
        BOTTOM,
    }  

    public Food(int id, string name, FoodType type, double rarity, string prefab, FoodPlacement placement)
    {
        foodID = id;
        foodName = name;
        foodType = type;
        foodRarity = rarity;
        foodPrefab = Resources.Load<GameObject>("ModelPrefab/" + prefab);
        foodIconType = Resources.Load<Sprite>("StorageIcon/" + foodType.ToString());
        foodARImage = Resources.Load<Material>("AR Selection Images/Materials/" + prefab);
        foodPlacement = placement;
    }
    public Food()
    {

    }
}
