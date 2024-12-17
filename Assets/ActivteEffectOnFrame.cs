using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivteEffectOnFrame : MonoBehaviour
{
    public GameObject effect;
    [SerializeField]
    private GameObject crackedFloorEffect;

    [SerializeField]
    private GameObject rotationPoint;
    public void Activate()
    {
        GameObject newCrack =  Instantiate(crackedFloorEffect, rotationPoint.transform.position, Quaternion.identity);
        newCrack.transform.up = rotationPoint.transform.up;
        Destroy(newCrack, 1.0f);

        //Destroy(newCrack, 100f);
        effect.SetActive(false);
        effect.SetActive(true);
    }
}
