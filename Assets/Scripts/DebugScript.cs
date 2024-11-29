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
<<<<<<< Updated upstream
=======
        if(Input.GetKeyDown(KeyCode.Keypad9))
        {
            RegainAllStats();
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            Nocd();
        }
        if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {
            killEnemies();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(enemies[0], mousePos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(enemies[1], mousePos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(enemies[2], mousePos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(enemies[3], mousePos, Quaternion.identity);
        }
>>>>>>> Stashed changes
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

    void killEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
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
}
