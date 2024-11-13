using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField]
    private float growTime;

    private float growthTimer;

    public int waterCost;

    private float progressionValue;

    [SerializeField]
    private GameObject seedObject;

    private bool wetSeed;
    public bool inSun;

    [SerializeField]
    private GameObject displayCostText;

    [SerializeField]
    private GameObject growingEffect;
    private void Start()
    {
        displayCostText.SetActive(false);
    }
    private void Update()
    {
        if(wetSeed && inSun)
        {
            Growing();
        }
    }
    public bool WaterSeed(GameObject aPlayer)
    {
        if(aPlayer.GetComponent<PlayerWater>().TotalWater() >= waterCost)
        {
            wetSeed = true;
            aPlayer.GetComponent<PlayerWater>().TakeWater(waterCost);
            displayCostText.SetActive(false);
            growingEffect.SetActive(true);
            return true;
        }
        else
        {
            Debug.Log("Not Enough Water");
            return false;
        }
    }
    private void Growing()
    {
        growthTimer += Time.deltaTime;

        if (growthTimer > growTime)
        {
            SeedComplete();
        }
    }
    private void SeedComplete()
    {
        GameObject newSeedObject = Instantiate(seedObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void DisplayCost()
    {
        if(wetSeed == false)
        {
            displayCostText.SetActive(!displayCostText.activeSelf);
        }
    }
}
