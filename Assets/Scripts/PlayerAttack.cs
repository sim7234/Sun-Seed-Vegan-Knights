using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private UnityEvent onFire;

    [SerializeField]
    private AudioClip swordSwingSound;

    private AudioSource audioSource;  
    private Animator weaponAnimator;

    private float attackCooldown = 1f; 

    private float lastAttackTime = 0f; 

    

    private void Start()
    {
        weaponAnimator = weapon.GetComponent<Animator>();


        audioSource = weapon.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = weapon.AddComponent<AudioSource>();
        }

        if (onFire == null)
        {
            Debug.LogWarning("No event Fire");
        }
    }

     public void Fire()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            onFire?.Invoke();

            if (weaponAnimator != null)
            {
                weaponAnimator.SetTrigger("PressedR1");
            }
            if(weapon.activeSelf)
            {
                PlaySwordSwingSound();
            }

    
            lastAttackTime = Time.time;
        }
        //else
        //{
        //    Debug.Log("Attack on cooldown");
        //}
    }

    private void PlaySwordSwingSound()
    {
        if (swordSwingSound != null && audioSource != null)
        {
            audioSource.pitch = Random.Range(0.90f, 1.1f);
            audioSource.Play();
            //audioSource.PlayOneShot(swordSwingSound);
        }
        else
        {
            Debug.LogWarning("nosound");
        }
    }
}