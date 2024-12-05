using System.Collections;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    [SerializeField]
    private string specialWeaponTag = "BigSword";

    [SerializeField]
    private Sprite destroyedSprite; 

    private SpriteRenderer spriteRenderer;
    private Collider2D wallCollider;
    private bool isDisappearing = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        wallCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDisappearing && collision.collider.CompareTag(specialWeaponTag) && collision.collider.GetComponent<SpecialWeapon>() != null)
        {
            SpecialWeapon specialWeapon = collision.collider.GetComponent<SpecialWeapon>();

            if (specialWeapon != null && specialWeapon.IsWieldingSword())
            {
                isDisappearing = true;
                StartCoroutine(ChangeToDestroyedSprite());
            }
        }
    }

    private IEnumerator ChangeToDestroyedSprite()
    {
        yield return new WaitForSeconds(0f);

        if (destroyedSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = destroyedSprite; 
            spriteRenderer.sortingOrder = -1;
        }

        if (wallCollider != null)
        {
            wallCollider.enabled = false;
        }

        isDisappearing = false;
    }
}