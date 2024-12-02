using System.Collections;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    [SerializeField]
    private string specialWeaponTag = "Player";

    private bool isDisappearing = false; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDisappearing && collision.collider.CompareTag(specialWeaponTag) && collision.collider.GetComponent<SpecialWeapon>() != null)
        {
            SpecialWeapon specialWeapon = collision.collider.GetComponent<SpecialWeapon>();

            if (specialWeapon != null && specialWeapon.IsWieldingSword())
            {
                isDisappearing = true; 
                StartCoroutine(DisappearWithDelay());
            }
        }
    }

    private IEnumerator DisappearWithDelay()
    {
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);

    }
}