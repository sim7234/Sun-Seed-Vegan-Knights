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
    private GameObject BloomBuildUpSliderObject;
    private Slider BloomBuildUpSlider;

    [SerializeField]
    private GameObject BloomExplosionVFX;

    [SerializeField]
    private GameObject bloomGlowRim;

    private Health health;

    public int divideHealthBy = 5;

    private void Start()
    {
        health = GetComponent<Health>();
        BloomBuildUpSlider = BloomBuildUpSliderObject.GetComponent<Slider>();
    }

    private void Update()
    {
        if(bloomBuildUp <= 0)
        {
            BloomBuildUpSliderObject.SetActive(false);
        }
        else if(hasBloomed != true)
        {
            bloomBuildUp -= Time.deltaTime;
            BloomBuildUpSlider.value = bloomBuildUp / bloomResistance;
        }

    }
    public void TakeBloomDamage(float damage, int playerIndex)
    {
        if (hasBloomed != true)
        {
            if (playerIndex == BloomIndex)
            {
                bloomBuildUp += damage;
            }
            else
            {
                BloomIndex = playerIndex;
                bloomBuildUp = 0;
                bloomBuildUp += damage; 
            }
            BloomBuildUpSlider.value = bloomBuildUp / bloomResistance;
            BloomBuildUpSliderObject.SetActive(true);
            if(bloomBuildUp > bloomResistance)
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
            //BloomExplosion();
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
        BloomBuildUpSliderObject.SetActive(false);
        bloomBuildUp = 0;
        bloomGlowRim.SetActive(false);
    }
}
