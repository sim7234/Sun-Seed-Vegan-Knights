using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private UnityEvent onFire;

    [SerializeField]
    private AudioClip swordSwingSound;

    [SerializeField]
    private AudioClip swordDashAttack;

    private AudioSource audioSource;  
    private Animator weaponAnimator;

    [SerializeField]
    private float attackCooldown = 1f;
    private float baseAttackCooldown;
    private int attackCounter = 0;
    private int attacksInChain = 2;
    private float attackCounterResetTime = 1.2f;
    private float attackCounterReset;

    private float lastAttackTime = 0f;

    public PlayerInputActions playerControls;

    [SerializeField]
    private MoveOnAttack baseWeaponMoveScript;
    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
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

        baseAttackCooldown = attackCooldown;
    }
    private void Update()
    {
        attackCounterReset -= Time.deltaTime;
        if (attackCounterReset < 0)
        {
            attackCounter = 0;
        }
    }
    public void Fire()
    {
       
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            onFire?.Invoke();
            
            attackCounter++;
            
            if (weaponAnimator != null)
            {
                weaponAnimator.SetTrigger("PressedR1");
            }
            if(weapon.activeSelf)
            {
                PlaySwordSwingSound();
            }

            switch(attackCounter)
            {
                case 1:
                    baseWeaponMoveScript.strengthModifer = 0.1f;
                    attackCooldown = baseAttackCooldown * 0.3f;
                    break;
                case 2:
                    baseWeaponMoveScript.strengthModifer = 0.1f;
                    attackCooldown = baseAttackCooldown * 0.5f;
                    break;
            }
            attackCounterReset = attackCounterResetTime;
            Debug.Log("Attack: " + attackCounter);
            if (attackCounter == attacksInChain)
            {
                attackCounter = 0;
            }

            lastAttackTime = Time.time;
        }
    }
    public void Attack()
    {
        if (weaponAnimator != null)
        {
            weaponAnimator.SetTrigger("PressedR1");
        }
        if (weapon.activeSelf)
        {
            PlaySwordSwingSound();
        }

        lastAttackTime = Time.time;
    }
    public void DashAttack()
    {
        if (weaponAnimator != null)
        {
            weaponAnimator.SetTrigger("DashAttack");
        }
        if (weapon.activeSelf)
        {
            PlaySwordSwingSound();
        }

        lastAttackTime = Time.time;
    }
    private void PlaySwordSwingSound()
    {
        if (swordSwingSound != null && audioSource != null)
        {
            audioSource.pitch = (1.0f - attackCounter /10) * Random.Range(0.95f, 1.05f);
            audioSource.Play();
            //audioSource.PlayOneShot(swordSwingSound);
        }
        else
        {
            Debug.LogWarning("nosound");
        }
    }
}