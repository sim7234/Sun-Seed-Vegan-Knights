using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MissionMaster : MonoBehaviour
{
    public static MissionMaster Instance;

    [HideInInspector] public int enemyCounter;


    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    [SerializeField]
    private TextMeshProUGUI enemyCounterText;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private List<GameObject> combatPoints = new List<GameObject>();

    private int combatsComplete;

    [SerializeField]
    private List<GameObject> combatSpawnObject = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Objectives = new List<GameObject>();

    [SerializeField]
    private List<GameObject> actionBetweenLevels = new List<GameObject>();

    [SerializeField]
    private AudioClip stageCompleteSound;

    private AudioSource audioSource;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    [SerializeField]
    private int respawnsForMission = 4;

    [SerializeField]
    private GameObject nextStagePointer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        combatsComplete = 0;
        SaveData.Instance.playerDeathsBeforeGameOver = respawnsForMission;
        SaveData.Instance.UpdateRespawnCount();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(ActivateCombatAfterDelay(10f, 0));

        if (Objectives.Count >= 1)
        {
            if (Objectives.Count >= combatsComplete)
            {
                if (Objectives[combatsComplete] != null)
                {
                    Objectives[combatsComplete].SetActive(true);
                    Objectives[combatsComplete].GetComponent<Objective>().StartObjectiveEvent();
                }
            }
        }
    }
    public void AddEnemy(GameObject enemyObject)
    {
        enemyCounter += 1;
        UpdateText();
        enemies.Add(enemyObject);
    }
    public void EnemyKilled(GameObject aEnemy)
    {
        if(enemies.Contains(aEnemy))
        {
            enemies.Remove(aEnemy);
            enemyCounter -= 1;
            UpdateText();
            if (enemyCounter <= 0)
            {
                PlayStageCompleteSound();
                NextStage();
            }
        }
    }

    private void UpdateText()
    {
        enemyCounterText.SetText(enemyCounter.ToString());
    }

    private void NextStage()
    {
        combatsComplete++;
        if (combatsComplete == combatPoints.Count)
        {
            //TODO: Win Screen

            SaveData.Instance.completedMission += 1;
            SceneManager.LoadScene(0);
        }
        if (combatsComplete < combatPoints.Count)
        {
            if (actionBetweenLevels[combatsComplete] != null)
            {
                actionBetweenLevels[combatsComplete].SetActive(true);
            }

            StartCoroutine(MoveCameraToNextPoint(cam.transform.position, combatPoints[combatsComplete].transform.position));
        }
    }

    private IEnumerator MoveCameraToNextPoint(Vector3 start, Vector3 end)
    {
        float duration = Vector3.Distance(start, end) / 2;
        float timeElapsed = 0;
        nextStagePointer.SetActive(true);
        while (timeElapsed < duration)
        {
            cam.transform.position = Vector3.Lerp(start, end, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
            nextStagePointer.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
            nextStagePointer.transform.up = (end - nextStagePointer.transform.position).normalized;
        }

        cam.transform.position = end;
        nextStagePointer.SetActive(false);
        StartCoroutine(ActivateCombatAfterDelay(10f, combatsComplete));
    }
    private IEnumerator ActivateCombatAfterDelay(float delay, int combatIndex)
    {
        float remainingTime = delay;


        while (remainingTime > 0)
        {
            countdownText.SetText("Prepare for battle: " + Mathf.FloorToInt(remainingTime).ToString());
            yield return new WaitForEndOfFrame();
            remainingTime -= Time.deltaTime;
        }

        countdownText.SetText("");
        combatSpawnObject[combatIndex].SetActive(true);
    }

    private void PlayStageCompleteSound()
    {
        if (stageCompleteSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(stageCompleteSound);
        }
    }
}
