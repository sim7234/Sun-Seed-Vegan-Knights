using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private int playersInTrigger = 0;

    [SerializeField] Image waterDropImage;
    [SerializeField] Image waterDropOutline;

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

            Debug.Log(canWaterScript.currentWater / canWaterScript.totalWaterCost);

            //we multiply by 1.001 to return a float instead of int

            if (canWaterScript.currentWater * 1.001 / canWaterScript.totalWaterCost * 1.001 < 0.6f)
            {
                if (waterDropImage.fillAmount < (canWaterScript.currentWater * 1.001 / canWaterScript.totalWaterCost * 1.001) - 0.05f)
                {
                    waterDropImage.fillAmount += Time.deltaTime * 0.3f;
                }
            }
            else if (waterDropImage.fillAmount < (canWaterScript.currentWater * 1.001 / canWaterScript.totalWaterCost * 1.001) - 0.05f)
            {
                waterDropImage.fillAmount += Time.deltaTime * 0.6f;
            }

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
            waterDropImage.enabled = false;
            waterDropOutline.enabled = false;
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
            waterDropImage.enabled = true;
            waterDropOutline.enabled = true;
        }
        else
        {
            displayCostText.enabled = false;
            waterDropImage.enabled = false;
            waterDropOutline.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Only react to objects tagged as "Player"
        {
            playersInTrigger++;
            Debug.Log($"Player entered. Players in trigger: {playersInTrigger}");
            DisplayCost(true);
        }

        if (other.GetComponent<IsSun>() != null)
        {
            inSun = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Only react to objects tagged as "Player"
        {
            playersInTrigger--;
            Debug.Log($"Player exited. Players in trigger: {playersInTrigger}");

            if (playersInTrigger <= 0)
            {
                DisplayCost(false);
            }
        }

        if (other.GetComponent<IsSun>() != null)
        {
            inSun = false;
        }
    }
}
