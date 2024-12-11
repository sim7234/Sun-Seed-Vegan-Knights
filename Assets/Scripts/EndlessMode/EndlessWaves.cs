using UnityEngine;

public class EndlessWaves : MonoBehaviour
{

    SpawnWaves spawnScript;

    int waveNumber = 0;

    bool waveStart;

    void Start()
    {
        spawnScript = GetComponent<SpawnWaves>();
    }


    void Update()
    {
        
    }
}
