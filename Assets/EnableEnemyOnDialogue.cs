using UnityEngine;

public class EnableEnemyOnDialogue : MonoBehaviour
{
    public Dialogue dialogueSystem;
    public GameObject enemyToEnable; 
    public int dialogueIndexToEnableEnemy = 1; 

    private bool enemyEnabled = false; 

    private void Start()
    {
        if (dialogueSystem != null)
        {
            dialogueSystem.onDialogueLineChanged.AddListener(OnDialogueLineChanged);
        }

        if (enemyToEnable != null)
        {
            enemyToEnable.SetActive(false); 
        }
    }

    private void OnDialogueLineChanged(int index)
    {
        if (!enemyEnabled && index == dialogueIndexToEnableEnemy)
        {
            if (enemyToEnable != null)
            {
                enemyToEnable.SetActive(true); 
                enemyEnabled = true; 
            }
        }
    }

    private void OnDestroy()
    {
        if (dialogueSystem != null)
        {
            dialogueSystem.onDialogueLineChanged.RemoveListener(OnDialogueLineChanged);
        }
    }
}