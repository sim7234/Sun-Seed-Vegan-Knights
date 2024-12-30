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

        if (SaveData.Instance.playerDeathsBeforeGameOver <= 0)
        {
            HandleFinalDeath();
            return;
        }

        GetComponent<PlayerHealing>().HealMax();
        TurnOfAllScripts();
        GetComponent<PlayerMovement>().moveSpeed = baseMoveSpeed;
        GetComponent<PlayerMovement>().rotationSpeed = 1;
        Invoke(nameof(TurnOnAllScripts), 5.0f);
        Invoke(nameof(SpawnExplosion), 3.0f);

        SaveData.Instance.playerDeathsBeforeGameOver--;
        SaveData.Instance.UpdateRespawnCount();

                    
    }
    void Respawn()
    {
        this.gameObject.SetActive(true);
        GetComponent<Health>().currentHealth = GetComponent<Health>().maxHealth;
    }

    private void HandleFinalDeath()
    {
        TurnOfAllScripts();
        if (MissionMaster.Instance != null)
        {
            MissionMaster.Instance.ShowGameOverScreen();
        }
    }

    private void TurnOfAllScripts()
    {
        allChildren.SetActive(false);
        GetComponent<WaterSystem>().enabled = false;
        GetComponent<PlantSeedSystem>().enabled = false;
        GetComponent<Health>().enabled = false;
        GetComponent<SpecialWeapon>().enabled = false;
        //GetComponent<AudioSource>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;

        gameObject.layer = 18;

        GetComponent<Collider2D>().enabled = false;

        GetComponent<HPBar>().enabled = false;
        GetComponent<PlayerDash>().enabled = false;
        GetComponent<PlayerGlow>().enabled = false;
        GetComponent<PlayerHealing>().enabled = false;
        GetComponent<LookOnSystem>().enabled = false;
        GetComponent<isTarget>().enabled = false;
        GetComponent<PlayerAnimations>().enabled = false;
        GetComponent<EnteredSun>().enabled = false;
    }
    private void TurnOnAllScripts()
    {
        allChildren.SetActive(true);
        GetComponent<WaterSystem>().enabled = true;
        GetComponent<PlantSeedSystem>().enabled = true;
        GetComponent<Health>().enabled = true;
        GetComponent<SpecialWeapon>().enabled = true;
        //GetComponent<AudioSource>().enabled = true;
        GetComponent<PlayerAttack>().enabled = true;

        GetComponent<Collider2D>().enabled = true;

        gameObject.layer = 9;

        GetComponent<HPBar>().enabled = true;
        GetComponent<PlayerDash>().enabled = true;
        GetComponent<PlayerGlow>().enabled = true;
        GetComponent<PlayerHealing>().enabled = true;
        GetComponent<LookOnSystem>().enabled = true;
        GetComponent<isTarget>().enabled = true;
        GetComponent<PlayerAnimations>().enabled = true;
        GetComponent<EnteredSun>().enabled = true;
    }

    private void SpawnExplosion()
    {
        GetComponent<PlayerMovement>().moveSpeed = 0;
        Invoke(nameof(ResetMoveSpeed), 2.0f);
        GameObject newDeathSpawn = Instantiate(deathBomb, transform.position, quaternion.identity);
        Destroy(newDeathSpawn, 2.3f);
        Screenshake.Instance.Shake(0.25f, 2.0f, 2.3f);
    }

    private void ResetMoveSpeed()
    {
        GetComponent<PlayerMovement>().moveSpeed = baseMoveSpeed;
    }
}
