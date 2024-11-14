using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionMaster : MonoBehaviour
{
    public static MissionMaster Instance;

   
    private int enemyCounter;

    [SerializeField]
    private TextMeshProUGUI enemyCounterText;


    private void Awake()
    {
        Instance = this;
    }


    public void AddEnemy()
    {
        enemyCounter += 1;
        UpdateText();
    }
    public void EnemyKilled()
    {
        enemyCounter -= 1;
        UpdateText();
        if (enemyCounter <= 0)
        {
            NextStage();
        }
    }

    private void UpdateText()
    {
        enemyCounterText.SetText(enemyCounter.ToString());
    }

    private void NextStage()
    {
        Debug.Log("New stage, camera moves");
    }
}
