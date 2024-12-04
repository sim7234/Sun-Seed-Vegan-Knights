using TMPro;
using UnityEngine;

public class SeedSystem : MonoBehaviour
{
    //Requirements: CanWater && IsSun Scripts
    //Interacts with: WaterSystem, PlantSeedSystem

    private float growTime = 5f;
    private float growthTimer;
    private float progressionValue;

    [SerializeField]
    private GameObject seedObject;

    public bool wetSeed;
    public bool inSun;

    [SerializeField]
    private TMP_Text displayCostText;

    [SerializeField]
    private GameObject growingEffect;

    CanWater canWaterScript;

    private void Start()
    {
        if (GetComponent<CanWater>() != null)
        {
            canWaterScript = GetComponent<CanWater>();
        }
    }
    private void Update()
    {
        if (canWaterScript != null)
        {
            displayCostText.text = (canWaterScript.currentWater.ToString() + " / " + canWaterScript.totalWaterCost.ToString());
        }


        if (wetSeed && inSun)
        {
            Growing();
        }

        if (canWaterScript != null && canWaterScript.currentWater >= canWaterScript.totalWaterCost)
        {
            wetSeed = true;
            canWaterScript.canBeWatered = false;
            displayCostText.enabled = false;
            growingEffect.SetActive(true);
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

   public void DisplayCost(bool active)
    {
        if (active == true && wetSeed == false)
        {
            displayCostText.enabled = true;
        }
        else
        {
            displayCostText.enabled = false;
        }   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IsSun>() != null)
        {
            inSun = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<IsSun>() != null)
        {
            inSun = false;
        }
    }
}
