using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class StageDialogue
{
    [TextArea(3, 10)]
    public string[] dialogues;
}

public class CameraMoverOnEnemyDeath : MonoBehaviour
{
    public GameObject[] enemiesStage1;
    public GameObject[] enemiesStage2;
    public GameObject[] enemiesStage3;
    public Transform[] cameraPositions;
    public float cameraSpeed = 2f;
    public Dialogue dialogueSystem;

    public StageDialogue[] stageDialogues;

    private int currentStage = 0;
    private bool moveCamera = false;

    void Start()
    {
        if (dialogueSystem == null)
        {
            return;
        }

        if (stageDialogues == null || stageDialogues.Length == 0)
        {
            return;
        }

        dialogueSystem.gameObject.SetActive(false);
        StartCoroutine(ShowDialogueAfterDelay(10f));
    }

    void Update()
    {
        if (currentStage < cameraPositions.Length && AreAllEnemiesDead(GetCurrentEnemies()))
        {
            moveCamera = true;
        }

        if (moveCamera)
        {
            MoveCameraToTarget();
        }
    }

    IEnumerator ShowDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogueSystem.gameObject.SetActive(true);

        if (stageDialogues.Length > 0)
        {
            dialogueSystem.StartDialogue(stageDialogues[currentStage].dialogues);
        }
    }

    void MoveCameraToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, cameraPositions[currentStage].position, cameraSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, cameraPositions[currentStage].position) <= 0.1f)
        {
            transform.position = cameraPositions[currentStage].position;
            moveCamera = false;
            TriggerNextStage();
        }
    }

    void TriggerNextStage()
    {
        currentStage++;

        if (currentStage < stageDialogues.Length && dialogueSystem != null)
        {
            dialogueSystem.StartDialogue(stageDialogues[currentStage].dialogues);
        }
    }

    GameObject[] GetCurrentEnemies()
    {
        switch (currentStage)
        {
            case 0: return enemiesStage1;
            case 1: return enemiesStage2;
            case 2: return enemiesStage3;
            default: return new GameObject[0];
        }
    }

    bool AreAllEnemiesDead(GameObject[] enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }
}