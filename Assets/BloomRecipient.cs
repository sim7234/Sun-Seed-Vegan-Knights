using System.Collections;
using System.Collections.Generic;
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

    private Health health;

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
            }

        }
        else
        {
            //if(playerIndex != BloomIndex)
            //{
            //    BloomExplosion();
            //}
            BloomExplosion();
        }
    }

    private void BloomExplosion()
    {
        GameObject newExplosion = Instantiate(BloomExplosionVFX, transform.position, Quaternion.identity);
        Destroy(newExplosion, 1);
        health.TakeDamage(health.maxHealth/5 + 25);
        hasBloomed = false;
        BloomBuildUpSliderObject.SetActive(false);
        bloomBuildUp = 0;
    }
}
