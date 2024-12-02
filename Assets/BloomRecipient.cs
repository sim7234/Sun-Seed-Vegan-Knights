using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BloomRecipient : MonoBehaviour
{
    [SerializeField]
    private float bloomResistance;

    [SerializeField]
    private float bloomBuildUp;

    [SerializeField]
    private bool hasBloomed;

    private int BloomIndex;

    [SerializeField]
    private GameObject BloomExplosionVFX;

    [SerializeField]
    private GameObject bloomGlowRim;

    private Health health;

    public int divideHealthBy = 5;


    [SerializeField]
    private GameObject flowerPelletsObject;

    [SerializeField]
    private List<GameObject> flowerPelltes = new List<GameObject>();
    private void Start()
    {
        health = GetComponent<Health>();
        //BloomBuildUpSlider = BloomBuildUpSliderObject.GetComponent<Slider>();
    }

    private void Update()
    {
        if(bloomBuildUp <= 0)
        {
            flowerPelletsObject.SetActive(false);
        }
        else if(hasBloomed != true)
        {
            bloomBuildUp -= Time.deltaTime;
            flowerPelletsObject.SetActive(true);
        }

    }
    public void TakeBloomDamage(float damage, int playerIndex)
    {
        if (hasBloomed != true)
        {
            if (playerIndex != BloomIndex)
            {
                BloomIndex = playerIndex;
                bloomBuildUp = 0;
                ColorPellets(playerIndex);
            }

            bloomBuildUp += damage;
            PrintPellets();

            if (bloomBuildUp > bloomResistance)
            {
                hasBloomed = true;
                bloomGlowRim.SetActive(true);
                changeColorOfRim(BloomIndex);
            }

        }
        else
        {
            if (playerIndex != BloomIndex)
            {
                BloomExplosion();
            }
        }
    }
    private void changeColorOfRim(int playerIndex)
    {
        switch (playerIndex)
        {
            case 1:
                bloomGlowRim.GetComponent<SpriteRenderer>().color = Color.green;

                break;
            case 2:
                bloomGlowRim.GetComponent<SpriteRenderer>().color = Color.red;

                break;
            case 3:
                bloomGlowRim.GetComponent<SpriteRenderer>().color = Color.cyan;

                break;
            case 4:
                bloomGlowRim.GetComponent<SpriteRenderer>().color = Color.yellow;

                break;
            default:
                bloomGlowRim.GetComponent<SpriteRenderer>().color = Color.white;

                break;
        }
    }
    private void BloomExplosion()
    {
        GameObject newExplosion = Instantiate(BloomExplosionVFX, transform.position, Quaternion.identity);
        Destroy(newExplosion, 1);
        health.TakeDamage(health.maxHealth/divideHealthBy + 25);
        hasBloomed = false;
        flowerPelletsObject.SetActive(false);
        bloomBuildUp = 0;
        bloomGlowRim.SetActive(false);
    }

    private void PrintPellets()
    {

        foreach (var item in flowerPelltes)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < Mathf.FloorToInt(flowerPelltes.Count * (bloomBuildUp / bloomResistance)); i++)
        {
            if(i < flowerPelltes.Count)
            {
                flowerPelltes[i].SetActive(true);
            }
        }
    }

    private void ColorPellets(int playerIndex)
    {
        foreach (var item in flowerPelltes)
        {
            switch (playerIndex)
            {
                case 1:
                    item.GetComponent<SpriteRenderer>().color = Color.green;

                    break;
                case 2:
                    item.GetComponent<SpriteRenderer>().color = Color.red;

                    break;
                case 3:
                    item.GetComponent<SpriteRenderer>().color = Color.cyan;

                    break;
                case 4:
                    item.GetComponent<SpriteRenderer>().color = Color.yellow;

                    break;
                default:
                    item.GetComponent<SpriteRenderer>().color = Color.white;

                    break;
            }
        }
    }
}
