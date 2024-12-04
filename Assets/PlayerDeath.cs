using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    [SerializeField]
    private GameObject allChildren;

    [SerializeField]
    private GameObject deathBomb;

    private float baseMoveSpeed;

    private void Start()
    {
        baseMoveSpeed = GetComponent<PlayerMovement>().moveSpeed;
    }
    public void onPlayerDeath()
    {

        GetComponent<PlayerHealing>().HealMax();
        TurnOfAllScripts();
        GetComponent<PlayerMovement>().moveSpeed = baseMoveSpeed;
        GetComponent<PlayerMovement>().rotationSpeed = 1;
        Invoke(nameof(TurnOnAllScripts), 5.0f);
        Invoke(nameof(SpawnExplosion), 4.8f);

        SaveData.Instance.playerDeathsBeforeGameOver--;

                    
    }
    void Respawn()
    {
        this.gameObject.SetActive(true);
        GetComponent<Health>().currentHealth = GetComponent<Health>().maxHealth;
    }

    private void TurnOfAllScripts()
    {
        allChildren.SetActive(false);
        GetComponent<PlayerWater>().enabled = false;
        GetComponent<PlantSeed>().enabled = false;
        GetComponent<Health>().enabled = false;
        GetComponent<SpecialWeapon>().enabled = false;
        //GetComponent<AudioSource>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<HPBar>().enabled = false;
        GetComponent<PlayerDash>().enabled = false;
        GetComponent<PlayerGlow>().enabled = false;
        GetComponent<PlayerHealing>().enabled = false;
        GetComponent<LookOnSystem>().enabled = false;
        GetComponent<isTarget>().enabled = false;
        GetComponent<PlayerAnimations>().enabled = false;
    }
    private void TurnOnAllScripts()
    {
        allChildren.SetActive(true);
        GetComponent<PlayerWater>().enabled = true;
        GetComponent<PlantSeed>().enabled = true;
        GetComponent<Health>().enabled = true;
        GetComponent<SpecialWeapon>().enabled = true;
        //GetComponent<AudioSource>().enabled = true;
        GetComponent<PlayerAttack>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        GetComponent<HPBar>().enabled = true;
        GetComponent<PlayerDash>().enabled = true;
        GetComponent<PlayerGlow>().enabled = true;
        GetComponent<PlayerHealing>().enabled = true;
        GetComponent<LookOnSystem>().enabled = true;
        GetComponent<isTarget>().enabled = true;
        GetComponent<PlayerAnimations>().enabled = true;
    }

    private void SpawnExplosion()
    {
        GameObject newDeathSpawn = Instantiate(deathBomb, transform.position, quaternion.identity);
        Destroy(newDeathSpawn, 0.2f);
    }
}
