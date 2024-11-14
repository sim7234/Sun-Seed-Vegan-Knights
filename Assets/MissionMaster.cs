using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MissionMaster : MonoBehaviour
{
    public static MissionMaster Instance;

    private int enemyCounter;

    [SerializeField]
    private TextMeshProUGUI enemyCounterText;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private List<GameObject> combatPoints = new List<GameObject>();

    private int combatsComplete;

    [SerializeField]
    private List<GameObject> combatSpawnObject = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        combatsComplete = 0;
        combatSpawnObject[0].SetActive(true);
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
        combatsComplete++;
        if(combatsComplete == combatPoints.Count)
        {
            SceneManager.LoadScene(0);
        }
        StartCoroutine(MoveCameraToNextPoint(cam.transform.position, combatPoints[combatsComplete].transform.position));
        Debug.Log("New stage, camera moves");
    }

    private IEnumerator MoveCameraToNextPoint(Vector3 start, Vector3 end)
    {
        float duration = Vector3.Distance(start, end) / 2;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            cam.transform.position = Vector3.Lerp(start, end, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        cam.transform.position = end;
        StartNextCombat();
    }
    private void StartNextCombat()
    {
        combatSpawnObject[combatsComplete].SetActive(true);
    }
}
