using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreakWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToActivate; 
    [SerializeField] private float activationDelay = 5f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpecialWeapon specialWeapon = collision.GetComponent<SpecialWeapon>();
        if (specialWeapon != null && specialWeapon.IsWieldingSword())
        {
            specialWeapon.DisableSpecialWeapon();
            StartCoroutine(ActivateObjectsAfterDelay());
        }
        else if (specialWeapon != null && specialWeapon.IsWieldingSpear())
        {
            specialWeapon.DisableSpecialWeapon();
            StartCoroutine(ActivateObjectsAfterDelay());
        }
        else
        {
            StartCoroutine(ActivateObjectsAfterDelay());
        }
    }

    private IEnumerator ActivateObjectsAfterDelay()
    {
        yield return new WaitForSeconds(activationDelay); 
        
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true); 
            }
        }
    }
}