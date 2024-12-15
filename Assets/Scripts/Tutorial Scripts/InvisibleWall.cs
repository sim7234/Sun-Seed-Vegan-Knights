using System.Collections;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    [SerializeField]
    private string bigSwordTag = "BigSword";
    [SerializeField]
    private string bigSpearTag = "BigSpear";

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDisappearing && 
            (collision.CompareTag(bigSwordTag) || collision.CompareTag(bigSpearTag)))
        {
            SpecialWeapon specialWeapon = collision.GetComponentInParent<SpecialWeapon>();

            if (specialWeapon != null && specialWeapon.IsAttacking())
            {
                if (specialWeapon.IsWieldingSword())
                {
                    TakeDamage(25f); 
                }
                else if (specialWeapon.IsWieldingSpear())
                {
                    TakeDamage(25f); 
                }
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