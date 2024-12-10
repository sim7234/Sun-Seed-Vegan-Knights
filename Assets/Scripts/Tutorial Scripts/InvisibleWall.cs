using System.Collections;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    [SerializeField]
    private string specialWeaponTag = "BigSword";

    [SerializeField]
    private Sprite destroyedSprite; 

    [SerializeField]
    private float wallHealth = 30f;

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
        // Ensure collision is with the special weapon and the weapon is actively attacking
        if (!isDisappearing && collision.collider.CompareTag(specialWeaponTag))
        {
            SpecialWeapon specialWeapon = collision.collider.GetComponentInParent<SpecialWeapon>();

            // Only take damage if the weapon is actively attacking
            if (specialWeapon != null && specialWeapon.IsWieldingSword() && specialWeapon.IsAttacking())
            {
                TakeDamage(25f); // Adjust the damage amount as necessary
            }
        }
    }

    private void TakeDamage(float damageAmount)
    {
        wallHealth -= damageAmount;

        if (wallHealth <= 0 && !isDisappearing)
        {
            isDisappearing = true;
            StartCoroutine(ChangeToDestroyedSprite());
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