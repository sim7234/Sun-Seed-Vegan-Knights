using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text waveText;

   [HideInInspector] public int score;

    [HideInInspector] public int waterDropsCollected;

    [HideInInspector] public int currentWave;
    static int highScore;

    EndlessWaves wavesScript;

    // Start is called before the first frame update
    void Start()
    {
        wavesScript = FindAnyObjectByType<EndlessWaves>();
    }

    // Update is called once per frame
    void Update()
    {
        currentWave = wavesScript.waveNumber;
        waveText.text = ("Wave: " + (currentWave -1)).ToString();

        if (score > highScore)
        {
            highScore = score;
        }
    }
}
