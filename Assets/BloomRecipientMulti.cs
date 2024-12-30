using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BloomRecipientMulti : MonoBehaviour
{
    [SerializeField]
    private float bloomResistance;


    private float bloomBuildUp1;
    private float bloomBuildUp2;
    private float bloomBuildUp3;
    private float bloomBuildUp4;

    [SerializeField]
    private bool hasBloomed1;
    private bool hasBloomed2;
    private bool hasBloomed3;
    private bool hasBloomed4;

    private int BloomIndex1;
    private int BloomIndex2;
    private int BloomIndex3;
    private int BloomIndex4;

    [SerializeField]
    private GameObject BloomExplosionVFX;

    private Health health;

    public int divideHealthBy = 5;


    [SerializeField]
    private GameObject flowerPelletsObject1;

    [SerializeField]
    private GameObject flowerPelletsObject2;

    [SerializeField]
    private GameObject flowerPelletsObject3;

    [SerializeField]
    private GameObject flowerPelletsObject4;

    [SerializeField]
    private List<GameObject> flowerPelltes1 = new List<GameObject>();
    [SerializeField]
    private List<GameObject> flowerPelltes2 = new List<GameObject>();
    [SerializeField]
    private List<GameObject> flowerPelltes3 = new List<GameObject>();
    [SerializeField]
    private List<GameObject> flowerPelltes4 = new List<GameObject>();


    [SerializeField] GameObject[] bloomOutline = new GameObject[4];

    private void Start()
    {
        health = GetComponent<Health>();
        //BloomBuildUpSlider = BloomBuildUpSliderObject.GetComponent<Slider>();
    }

    private void Update()
    {
        cheackAllBloomValues();
    }
    public void TakeBloomDamage(float damage, int playerIndex)
    {
        if (BloomIndex1 != playerIndex || BloomIndex2 != playerIndex
            || BloomIndex3 != playerIndex || BloomIndex4 != playerIndex)
        {
            switch (playerIndex)
            {
                case 1:
                    BloomIndex1 = playerIndex;
                    ColorPellets(playerIndex, flowerPelltes1);
                    break;
                case 2:
                    BloomIndex2 = playerIndex;
                    ColorPellets(playerIndex, flowerPelltes2);
                    break;
                case 3:
                    BloomIndex3 = playerIndex;
                    ColorPellets(playerIndex, flowerPelltes3);
                    break;
                case 4:
                    BloomIndex4 = playerIndex;
                    ColorPellets(playerIndex, flowerPelltes4);
                    break;
            }
        }

        switch (playerIndex)
        {
            case 1:
                bloomBuildUp1 += damage;
                PrintPellets(flowerPelltes1, bloomBuildUp1);
                if (bloomBuildUp1 > bloomResistance)
                {
                    bloomOutline[0].SetActive(true);
                    hasBloomed1 = true;
                    flowerPelletsObject1.SetActive(true);
                }
                break;
            case 2:
                bloomBuildUp2 += damage;
                PrintPellets(flowerPelltes2, bloomBuildUp2);
                if (bloomBuildUp2 > bloomResistance)
                {
                    bloomOutline[1].SetActive(true);
                    hasBloomed2 = true;
                    flowerPelletsObject2.SetActive(true);
                }
                break;
            case 3:
                bloomBuildUp3 += damage;
                PrintPellets(flowerPelltes3, bloomBuildUp3);
                if (bloomBuildUp3 > bloomResistance)
                {
                    bloomOutline[2].SetActive(true);
                    hasBloomed3 = true;
                    flowerPelletsObject3.SetActive(true);
                }
                break;
            case 4:
                bloomBuildUp4 += damage;
                PrintPellets(flowerPelltes4, bloomBuildUp4);
                if (bloomBuildUp4 > bloomResistance)
                {
                    bloomOutline[3].SetActive(true);
                    hasBloomed4 = true;
                    flowerPelletsObject4.SetActive(true);
                }
                break;
        }

        if (hasBloomed1 && playerIndex != BloomIndex1)
        {
            BloomExplosion(BloomIndex1);
        }
        if (hasBloomed2 && playerIndex != BloomIndex2)
        {
            BloomExplosion(BloomIndex2);
        }
        if (hasBloomed3 && playerIndex != BloomIndex3)
        {
            BloomExplosion(BloomIndex3);
        }
        if (hasBloomed4 && playerIndex != BloomIndex4)
        {
            BloomExplosion(BloomIndex4);
        }
    }

    private void BloomExplosion(int playerIndex)
    {
        Debug.Log("Boom");
        GameObject newExplosion = Instantiate(BloomExplosionVFX, transform.position, Quaternion.identity);
        Destroy(newExplosion, 1);
        health.TakeDamage(health.maxHealth / divideHealthBy + 25);
        switch (playerIndex)
        {
            case 1:
                hasBloomed1 = false;
                bloomBuildUp1 = 0;
                flowerPelletsObject1.SetActive(false);
                break;
            case 2:
                hasBloomed2 = false;
                bloomBuildUp2 = 0;
                flowerPelletsObject2.SetActive(false);
                break;
            case 3:
                hasBloomed3 = false;
                bloomBuildUp3 = 0;
                flowerPelletsObject3.SetActive(false);
                break;
            case 4:
                hasBloomed4 = false;
                bloomBuildUp4 = 0;
                flowerPelletsObject4.SetActive(false);
                break;
        }
    }

    private void PrintPellets(List<GameObject> flowerPelltes, float bloomBuildUp)
    {
        foreach (var item in flowerPelltes)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < Mathf.FloorToInt(flowerPelltes.Count * (bloomBuildUp / bloomResistance)); i++)
        {
            if (i < flowerPelltes.Count)
            {
                flowerPelltes[i].SetActive(true);
            }
        }
    }

    private void ColorPellets(int playerIndex, List<GameObject> flowerPelltes)
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

    private void cheackAllBloomValues()
    {
        if (bloomBuildUp1 <= 0 && hasBloomed1 != true)
        {
            flowerPelletsObject1.SetActive(false);
        }
        else if (hasBloomed1 != true)
        {
            bloomBuildUp1 -= Time.deltaTime;
            flowerPelletsObject1.SetActive(true);
            PrintPellets(flowerPelltes1, bloomBuildUp1);
        }

        if (bloomBuildUp2 <= 0 && hasBloomed2 != true)
        {
            flowerPelletsObject2.SetActive(false);
        }
        else if (hasBloomed2 != true)
        {
            bloomBuildUp2 -= Time.deltaTime;
            flowerPelletsObject2.SetActive(true);
            PrintPellets(flowerPelltes2, bloomBuildUp2);
        }

        if (bloomBuildUp3 <= 0 && hasBloomed3 != true)
        {
            flowerPelletsObject3.SetActive(false);
        }
        else if (hasBloomed3 != true)
        {
            bloomBuildUp3 -= Time.deltaTime;
            flowerPelletsObject3.SetActive(true);
            PrintPellets(flowerPelltes3, bloomBuildUp3);
        }

        if (bloomBuildUp4 <= 0 && hasBloomed4 != true)
        {
            flowerPelletsObject4.SetActive(false);
        }
        else if (hasBloomed4 != true)
        {
            bloomBuildUp4 -= Time.deltaTime;
            flowerPelletsObject4.SetActive(true);
            PrintPellets(flowerPelltes4, bloomBuildUp4);
        }
    }
}