// Ignore Spelling: Collider

using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    public int damage = 1;

    private Collider2D damageCollider;

    [SerializeField]
    private bool isTriggerd;

    private void Awake()
    {
        damageCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>() != null && this.tag != other.tag)
        {
            if(other.GetComponent<Health>().isActiveAndEnabled)
            {
                other.GetComponent<Health>().TakeDamage(damage);
                Screenshake.Instance.Shake(0.3f, 0.1f, 1.0f);
            }
        }
        else
        {
            return;
        }
    }
    
    public void TurnOnCollider()
    {
        damageCollider.enabled = true;
    }
    public void TurnOfCollider()
    {
        damageCollider.enabled = false;
    }
}