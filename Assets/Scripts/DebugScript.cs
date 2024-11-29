using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    // Update is called once per frame

    [SerializeField] GameObject[] enemies;

    Vector2 mousePos;
    Camera camera;

    bool noPlantCooldown;

    private void Start()
    {
        Application.targetFrameRate = 144;
        camera = Camera.main;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = camera.ScreenToWorldPoint(mousePos);

        if (noPlantCooldown)
        {
            PlantSeed[] seedStats = FindObjectsOfType<PlantSeed>();

            foreach (var item in seedStats)
            {
                if (item.gameObject.CompareTag("Player"))
                {
                    item.plantingTimer = 0;
                }
            }
        }

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
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            goToSandbox();
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
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            Nocd();
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

    void goToSandbox()
    {
        SceneManager.LoadScene(5);
    }

    void killPlayers()
    {
        SaveData.Instance.playerAmount = 0;

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


    void Nocd()
    {
        PlantSeed[] seedStats = FindObjectsOfType<PlantSeed>();

        foreach (var item in seedStats)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.plantSpeed = 1;
            }
        }

        noPlantCooldown = !noPlantCooldown;

        PlayerWater[] waterStats = FindObjectsOfType<PlayerWater>();

        foreach (var item in waterStats)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.waterGainTime = 0.1f;
            }
        }
    }
}
