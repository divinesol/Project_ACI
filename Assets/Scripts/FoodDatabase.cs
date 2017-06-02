using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodDatabase : MonoBehaviour
{
    public List<Food> food = new List<Food>();

    void Start()
    {
        food.Add(new Food(0, "Tomato A", Food.FoodType.COLD, 5, "Tomato",   Food.FoodPlacement.TOP));
        food.Add(new Food(1, "Tomato B", Food.FoodType.COLD, 4, "Tomato 1", Food.FoodPlacement.TOP));
        food.Add(new Food(2, "Tomato C", Food.FoodType.COLD, 3, "Tomato 2", Food.FoodPlacement.TOP));
        food.Add(new Food(3, "Tomato D", Food.FoodType.COLD, 2, "Tomato 3", Food.FoodPlacement.TOP));
        food.Add(new Food(4, "Tomato E", Food.FoodType.COLD, 1, "Tomato 4", Food.FoodPlacement.TOP));
    
        food.Add(new Food(5, "Canned Food A", Food.FoodType.DRY, 5, "Canned_Food"  , Food.FoodPlacement.TOP));
        food.Add(new Food(6, "Canned Food B", Food.FoodType.DRY, 4, "Canned_Food 1", Food.FoodPlacement.TOP));
        food.Add(new Food(7, "Canned Food C", Food.FoodType.DRY, 3, "Canned_Food 2", Food.FoodPlacement.TOP));
        food.Add(new Food(8, "Canned Food D", Food.FoodType.DRY, 2, "Canned_Food 3", Food.FoodPlacement.TOP));
        food.Add(new Food(9, "Canned Food E", Food.FoodType.DRY, 1, "Canned_Food 4", Food.FoodPlacement.TOP));

        food.Add(new Food(10, "Steak A", Food.FoodType.FREEZE, 5, "Steak"  , Food.FoodPlacement.BOTTOM));
        food.Add(new Food(11, "Steak B", Food.FoodType.FREEZE, 4, "Steak 1", Food.FoodPlacement.BOTTOM));
        food.Add(new Food(12, "Steak C", Food.FoodType.FREEZE, 3, "Steak 2", Food.FoodPlacement.BOTTOM));
        food.Add(new Food(13, "Steak D", Food.FoodType.FREEZE, 2, "Steak 3", Food.FoodPlacement.BOTTOM));
        food.Add(new Food(14, "Steak E", Food.FoodType.FREEZE, 1, "Steak 4", Food.FoodPlacement.BOTTOM));

        food.Add(new Food(15, "Mushroom A", Food.FoodType.COLD, 5, "Mushroom"  , Food.FoodPlacement.TOP));
        food.Add(new Food(16, "Mushroom B", Food.FoodType.COLD, 4, "Mushroom 1", Food.FoodPlacement.TOP));
        food.Add(new Food(17, "Mushroom C", Food.FoodType.COLD, 3, "Mushroom 2", Food.FoodPlacement.TOP));
        food.Add(new Food(18, "Mushroom D", Food.FoodType.COLD, 2, "Mushroom 3", Food.FoodPlacement.TOP));
        food.Add(new Food(19, "Mushroom E", Food.FoodType.COLD, 1, "Mushroom 4", Food.FoodPlacement.TOP));

        food.Add(new Food(20, "Chicken A", Food.FoodType.FREEZE, 5, "Chicken"  , Food.FoodPlacement.BOTTOM));
        food.Add(new Food(21, "Chicken B", Food.FoodType.FREEZE, 4, "Chicken 1", Food.FoodPlacement.BOTTOM));
        food.Add(new Food(22, "Chicken C", Food.FoodType.FREEZE, 3, "Chicken 2", Food.FoodPlacement.BOTTOM));
        food.Add(new Food(23, "Chicken D", Food.FoodType.FREEZE, 2, "Chicken 3", Food.FoodPlacement.BOTTOM));
        food.Add(new Food(24, "Chicken E", Food.FoodType.FREEZE, 1, "Chicken 4", Food.FoodPlacement.BOTTOM));

        food.Add(new Food(25, "Cheese A", Food.FoodType.COLD, 5, "Cheese",   Food.FoodPlacement.TOP));
        food.Add(new Food(26, "Cheese B", Food.FoodType.COLD, 4, "Cheese 1", Food.FoodPlacement.TOP));
        food.Add(new Food(27, "Cheese C", Food.FoodType.COLD, 3, "Cheese 2", Food.FoodPlacement.TOP));
        food.Add(new Food(28, "Cheese D", Food.FoodType.COLD, 2, "Cheese 3", Food.FoodPlacement.TOP));
        food.Add(new Food(29, "Cheese E", Food.FoodType.COLD, 1, "Cheese 4", Food.FoodPlacement.TOP));
    }
}
