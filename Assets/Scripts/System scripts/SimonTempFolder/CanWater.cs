using UnityEngine;

public class CanWater : MonoBehaviour
{
    [HideInInspector] public bool canBeWatered = true;
    //this script is referenced in "WaterSystem" script.
    public int totalWaterCost;
    //this can be made public if you want something to need a different minimum amount of water.
    [HideInInspector] public int waterCostPerAction = 1;
    public int currentWater;
}
