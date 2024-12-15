using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;

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

        [SerializeField]
    private GameObject stompArea; 

    [SerializeField]
    private int stompDamage = 10; 

    [SerializeField]
    private float stompDuration = 0.3f; 

    [SerializeField]
    private float stompCooldown = 10f; 
    private bool canStomp = true; 


    private void Awake()
    {
        playerControls = new PlayerInputActions();

        playerControls.ControlActions1.Fire.performed += ctx =>
        {
            if (ctx.duration >= 0.3f) 
            {
                FireHold();
            }
            else
            {
                FireTap();
            }
        };

        playerControls.ControlActions1.Stomp.performed += ctx => Stomp();
    }

    private void FireTap()
    {
        Attack();
    }

    private void FireHold()
    {
        StartCoroutine(HoldAttackSequence());
    }

    private IEnumerator HoldAttackSequence()
    {
        while (playerControls.ControlActions1.Fire.ReadValue<float>() > 0)
        {
            Fire(); 
            yield return new WaitForSeconds(attackCooldown); 
        }
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

    public void Stomp()
    {
    if (!canStomp)
        {
            Debug.Log("Stomp CD");
            return;
        }

        if (stompArea != null)
        {
            stompArea.SetActive(true); 

            Damage stompDamageComponent = stompArea.GetComponent<Damage>();
            if (stompDamageComponent != null)
            {
                stompDamageComponent.damage = stompDamage;
                stompDamageComponent.TurnOnCollider();
            }

            StartCoroutine(DeactivateStompAreaAfterDelay());
        }

        Screenshake.Instance.Shake(0.7f, 0.25f, 1.5f);

        canStomp = false;
        StartCoroutine(StompCooldownCoroutine());
    }

    private IEnumerator StompCooldownCoroutine()
    {
        yield return new WaitForSeconds(stompCooldown);
        canStomp = true;
    }

    private IEnumerator DeactivateStompAreaAfterDelay()
    {
        yield return new WaitForSeconds(stompDuration);

        if (stompArea != null)
        {
            Damage stompDamageComponent = stompArea.GetComponent<Damage>();
            if (stompDamageComponent != null)
            {   
                stompDamageComponent.TurnOfCollider();
            }
            stompArea.SetActive(false);
        }
    }

    private void OnEnable()
    {
        playerControls.ControlActions1.Stomp.performed += ctx =>
        {
            Debug.Log("Stomp");
            Stomp();
        };
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.ControlActions1.Stomp.performed -= ctx => Stomp();
        playerControls.Disable();
    }

    private void PlaySwordSwingSound()
    {
        if (swordSwingSound != null && audioSource != null)
        {
            audioSource.pitch = (1.0f - attackCounter /10) * Random.Range(0.95f, 1.05f);
            audioSource.Play();
        }
    }
}