using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private UnityEvent onFire;

    private Animator weaponAnimator;

    private void Start()
    {
        weaponAnimator = weapon.GetComponent<Animator>();

        if (onFire == null)
        {
            Debug.LogWarning("No event assigned to OnFire Unity Event.");
        }
    }

    public void Fire()
    {
        onFire?.Invoke();

        if (weaponAnimator != null)
        {
            weaponAnimator.SetTrigger("PressedR1");
        }
    }
}