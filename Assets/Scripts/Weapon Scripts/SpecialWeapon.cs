using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SpecialWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bigSword;
    [SerializeField]
    private GameObject bigSpear;

    private WeaponType weaponType;

    private int specialWeaponAttacks = 5;
    [SerializeField]
    private int specialWeaponAttacksBase = 5;
    private int attackCounter;
    public float attackCooldown;

    [SerializeField]
    private GameObject baseWeapon;

    [SerializeField]
    private List<GameObject> weaponPickupsInRange = new List<GameObject>();

    [SerializeField]
    private AudioClip swordSwingSound;

    [SerializeField]
    private AudioClip spearThrustSound;

    private AudioSource audioSource;
    
    private PlayerInput playerInput;
    private InputAction fireAction;
    private InputAction pickUpAction;

    [SerializeField]
    private TextMeshProUGUI attacksLeftText;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            fireAction = playerInput.actions["Fire"];
            pickUpAction = playerInput.actions["PickUp"];
        }
    }

    private void OnEnable()
    {
        if (fireAction != null)
        {
            fireAction.Enable();
            fireAction.performed += Fire; 
        }
        if (pickUpAction != null)
        {
            pickUpAction.Enable();
            pickUpAction.performed += PickUpWeapon;
        }
    }
    private void OnDisable()
    {
        if (fireAction != null)
        {
            fireAction.Disable();
            fireAction.performed -= Fire;
        }
        if (pickUpAction != null)
        {
            pickUpAction.Disable();
            pickUpAction.performed -= PickUpWeapon;
        }
    }


    private void Start()
    {
        attackCounter = 0;
    }

    public void PickUpWeapon(InputAction.CallbackContext context)
    {
        if (weaponPickupsInRange.Count > 0)
        {
            attackCounter = 0;
            if (weaponPickupsInRange[0].GetComponent<WeaponPickup>().GetWeaponType() == WeaponType.Sword)
            {
                bigSword.SetActive(true);
                bigSword.GetComponent<Collider2D>().enabled = false;
                bigSpear.SetActive(false);

                specialWeaponAttacks = specialWeaponAttacksBase / 3;
                attacksLeftText.gameObject.SetActive(true);
            }
            else if (weaponPickupsInRange[0].GetComponent<WeaponPickup>().GetWeaponType() == WeaponType.Spear)
            {
                bigSpear.SetActive(true);
                bigSpear.GetComponent<Collider2D>().enabled = false;
                bigSword.SetActive(false);

                specialWeaponAttacks = specialWeaponAttacksBase;
                attacksLeftText.gameObject.SetActive(true);
            }

            baseWeapon.SetActive(false);
            if (weaponPickupsInRange[0] != null)
            {
                Destroy(weaponPickupsInRange[0]);
            }
            weaponPickupsInRange.Clear();

            //Debug.Log("Special weapon picked up!");
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (bigSword.activeSelf && attackCooldown <= 0)
        {
            attackCounter++;
            if (attackCounter == specialWeaponAttacks)
            {
                Invoke(nameof(DisableSpecialWeapon), 0.8f);
            }

            bigSword.GetComponent<Animator>().SetTrigger("Attack");
            bigSword.GetComponent<Collider2D>().enabled = true;
            Invoke(nameof(DisableWeaponCollider), 0.5f);
            PlaySwordSwingSound();
            attackCooldown = 1.25f;
            //Debug.Log("Big swing");
        }
        
        if (bigSpear.activeSelf && attackCooldown <= 0)
        {
            attackCounter++;
            if (attackCounter == specialWeaponAttacks)
            {
                Invoke(nameof(DisableSpecialWeapon), 0.8f);
            }

            bigSpear.GetComponent<Animator>().SetTrigger("Attack");
            bigSpear.GetComponent<Collider2D>().enabled = true;
            Invoke(nameof(DisableWeaponCollider), 0.5f);
            PlaySpearThrustSound();
            attackCooldown = 0.8f;
        }

    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        attacksLeftText.SetText((specialWeaponAttacks - attackCounter).ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<WeaponPickup>() != null && collision.CompareTag("WeaponPickup"))
        {
            weaponPickupsInRange.Add(collision.gameObject);
            GetComponent<PlantSeedSystem>().cantPlant = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<WeaponPickup>() != null && collision.CompareTag("WeaponPickup"))
        {
            if(weaponPickupsInRange.Contains(collision.gameObject))
            {
                weaponPickupsInRange.Remove(collision.gameObject);
                GetComponent<PlantSeedSystem>().cantPlant = false;
            }
        }
    }

    private void PlaySwordSwingSound()
    {
        if (swordSwingSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(swordSwingSound); 
        }
    }

    private void PlaySpearThrustSound()
    {
        if (spearThrustSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(spearThrustSound); 
        }
    }

    public bool IsWieldingSword()
    {
        return bigSword != null && bigSword.activeSelf;
    }

    public bool IsWieldingSpear()
    {
        return bigSpear != null && bigSpear.activeSelf;
    }

    public bool IsAttacking()
    {
        return (bigSword.activeSelf && bigSword.GetComponent<Collider2D>().enabled) ||
        (bigSpear.activeSelf && bigSpear.GetComponent<Collider2D>().enabled);

    }

    public void DisableSpecialWeapon()
    {
        if (bigSword.activeSelf || bigSpear.activeSelf)
        {
            attacksLeftText.gameObject.SetActive(false);
            bigSword.SetActive(false);
            bigSpear.SetActive(false);
            baseWeapon.SetActive(true);
        }
    }

    private void DisableWeaponCollider()
    {
        if (bigSword.activeSelf)
        {
            bigSword.GetComponent<Collider2D>().enabled = false;
        }

        if (bigSpear.activeSelf)
        {
            bigSpear.GetComponent<Collider2D>().enabled = false;
        }
    }
}