using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            goToMenu();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            goToHub();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            goToLevel1();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            killPlayers();
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            resetPathfinding();
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            infiniteHealth();
        }
        if(Input.GetKeyDown(KeyCode.Keypad9))
        {
            RegainAllStats();
        }
    }
    void goToMenu()
    {
        SceneManager.LoadScene(3);
    }

    void goToHub()
    {
        SceneManager.LoadScene(0);
    }

    void goToLevel1()
    {
        SceneManager.LoadScene(1);
    }

    void killPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            Destroy(p);
        }
    }

    void resetPathfinding()
    {
        FindTargets[] getTargets = FindObjectsOfType<FindTargets>();
        foreach (FindTargets target in getTargets)
        {
            target.GetTargets();
        }
    }

    void infiniteHealth()
    {
        Health[] health = FindObjectsOfType<Health>();

        foreach (var item in health)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.maxHealth = 100000;
                item.currentHealth = item.maxHealth;
            }

        }
    }

    private void RegainAllStats()
    {
        PlantSeed[] seedStats = FindObjectsOfType<PlantSeed>();

        foreach (var item in seedStats)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.plantingTimer = 0;
            }
        }
        PlayerWater[] waterStats = FindObjectsOfType<PlayerWater>();

        foreach (var item in waterStats)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.MaxFill();
            }
        }

        Health[] health = FindObjectsOfType<Health>();

        foreach (var item in health)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.currentHealth = item.maxHealth;
            }

        }
    }
}
