using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MissionMaster : MonoBehaviour
{
    public static MissionMaster Instance;

    [HideInInspector] public int enemyCounter;


    
    private List<GameObject> enemies = new List<GameObject>();


    [Header("Mission set up")]
    [SerializeField]
    private GameObject focusPoint;

    [SerializeField]
    private List<GameObject> combatPoints = new List<GameObject>();

    [SerializeField]
    private List<GameObject> combatSpawnObject = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Objectives = new List<GameObject>();

    [SerializeField]
    private List<GameObject> actionBetweenLevels = new List<GameObject>();

    private int combatsComplete;



    [SerializeField]
    private int respawnsForMission = 4;

    [Space]
    [Header ("Mission master required")]

    [SerializeField]
    private GameObject nextStagePointer;

    private float currentCameraSpeedModifire = 3;

    [HideInInspector]
    public bool combatOver = true;

    [SerializeField]
    private AudioClip stageCompleteSound;

    private AudioSource audioSource;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    [SerializeField]
    private TextMeshProUGUI enemyCounterText;
    private bool onFinalStage;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Animator winAnimator;
    [SerializeField] private Animator gameOverAnimator;
    [SerializeField] private float screenDisplayDuration = 3f;




    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        onFinalStage = false;
        combatsComplete = 0;
        SaveData.Instance.playerDeathsBeforeGameOver = respawnsForMission;
        SaveData.Instance.UpdateRespawnCount();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        //StartCoroutine(ActivateCombatAfterDelay(10f, 0));

        //if (Objectives.Count >= 1)
        //{
        //    if (Objectives.Count >= combatsComplete)
        //    {
        //        if (Objectives[combatsComplete] != null)
        //        {
        //            Objectives[combatsComplete].SetActive(true);
        //            Objectives[combatsComplete].GetComponent<Objective>().StartObjectiveEvent();
        //        }
        //    }
        //}

        combatOver = true;
    }
    private void Update()
    {
        if(onFinalStage && enemies.Count <= 0)
        {
            WinScreen();
            onFinalStage = false;
        }
    }

    private void WinScreen()
    {
        SaveData.Instance.completedMission += 1;
        StartCoroutine(ShowScreenWithAnimation(winAnimator, true));
    }
     public void ShowGameOverScreen()
    {
        StartCoroutine(ShowScreenWithAnimation(gameOverAnimator, false));
    }

    private IEnumerator ShowScreenWithAnimation(Animator animator, bool isWin)
    {
        if (animator != null)
        {
            animator.gameObject.SetActive(true);
 
            animator.SetTrigger("FadeIn");
            yield return new WaitForSeconds(screenDisplayDuration);

            /*animator.SetTrigger("FadeOut");
            yield return new WaitForSeconds(1.0f);*/

            animator.gameObject.SetActive(false);

            if (isWin)
            {
                SceneManager.LoadScene("True Hub"); 
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    public void CheckGameOver()
    {
        if (SaveData.Instance.playerDeathsBeforeGameOver <= 0)
        {
            ShowGameOverScreen();
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
        if (enemies.Contains(aEnemy))
        {
            enemies.Remove(aEnemy);
            enemyCounter -= 1;
            UpdateText();
            if (enemyCounter <= 0)
            {
                PlayStageCompleteSound();
                //NextStage();

                combatOver = true;
            }
        }
    }

    private void UpdateText()
    {
        enemyCounterText.SetText(enemyCounter.ToString());
    }

    public void NewStageByTrigger(GameObject point)
    {
        StartCoroutine(MoveCameraToNextPoint(focusPoint, focusPoint.transform.position, point));
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
            if (combatPoints[combatsComplete].GetComponent<CameraSpeedModifier>() != null)
            {
                currentCameraSpeedModifire = combatPoints[combatsComplete].GetComponent<CameraSpeedModifier>().speed;
            }
            StartCoroutine(MoveCameraToNextPoint(focusPoint, focusPoint.transform.position, combatPoints[combatsComplete]));
            //    StartCoroutine(MoveCameraToNextPoint(focusPoint, focusPoint.transform.position, combatPoints[combatsComplete].transform.position));
        }
    }

    private IEnumerator MoveCameraToNextPoint(GameObject obj, Vector3 start, GameObject end)
    {
        combatOver = false;
        float duration = Vector3.Distance(start, end.transform.position) / 8;
        float timeElapsed = 0;
        nextStagePointer.SetActive(true);
        while (timeElapsed < duration)
        {
            obj.transform.position = Vector3.Lerp(start, end.transform.position, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
            nextStagePointer.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
            nextStagePointer.transform.up = (end.transform.position - nextStagePointer.transform.position).normalized;
        }

        obj.transform.position = end.transform.position;
        nextStagePointer.SetActive(false);
        currentCameraSpeedModifire = 3;

        int matchingIndex = FindMatchingIndex(end);
        if(end.GetComponent<FinalMissionPoint>() != null)
        {
            StartCoroutine(ActivateCombatAfterDelay(0.0f, matchingIndex));
            end.GetComponent<FinalMissionPoint>().FinalMission();
            onFinalStage = true;
        }
        else
        {
            Debug.Log(matchingIndex + " MATCHING INDEX");
            StartCoroutine(ActivateCombatAfterDelay(4f, matchingIndex));
        }
        
       
    }
    private int FindMatchingIndex(GameObject obj)
    {
        int returnIndex = -1;
        for (int i = 0; i < combatPoints.Count; i++)
        {
            if (combatPoints[i] == obj)
            {
                returnIndex = i;
                break;
            }
        }
        if(returnIndex == -1)
        {
            Debug.Log("No match found");   
            return 0;
        }
        else
        {
            return returnIndex;
        }
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
        if (combatSpawnObject[combatIndex] != null)
        {
            combatSpawnObject[combatIndex].SetActive(true);
        }
        else if (combatSpawnObject[combatIndex].GetComponent<SpawnWaves>() != null)
        {
            Camera.main.GetComponent<SpawnWaves>().startSpawning = true;
        }
    }

    private void PlayStageCompleteSound()
    {
        if (stageCompleteSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(stageCompleteSound);
        }
    }
}
