using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpecialWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bigSword;
    [SerializeField]
    private GameObject bigSpear;

    private WeaponType weaponType;

    private int specialWeaponAttacks = 5;
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
                bigSpear.SetActive(false);

                specialWeaponAttacks = 5;
            }
            else if (weaponPickupsInRange[0].GetComponent<WeaponPickup>().GetWeaponType() == WeaponType.Spear)
            {
                bigSpear.SetActive(true);
                bigSword.SetActive(false);
                specialWeaponAttacks = 10;
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
            PlaySwordSwingSound();
            attackCooldown = 1.5f;
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
            PlaySpearThrustSound();
            attackCooldown = 1.5f;
            Debug.Log("Big swing");
        }
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<WeaponPickup>() != null && collision.CompareTag("WeaponPickup"))
        {
            weaponPickupsInRange.Add(collision.gameObject);
            GetComponent<PlantSeed>().cantPlant = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<WeaponPickup>() != null && collision.CompareTag("WeaponPickup"))
        {
            if(weaponPickupsInRange.Contains(collision.gameObject))
            {
                weaponPickupsInRange.Remove(collision.gameObject);
                GetComponent<PlantSeed>().cantPlant = false;
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

    public void DisableSpecialWeapon()
    {
        if (bigSword.activeSelf || bigSpear.activeSelf)
        {
            bigSword.SetActive(false);
            bigSpear.SetActive(false);
            baseWeapon.SetActive(true);
        }
    }
}