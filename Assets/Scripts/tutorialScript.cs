using System.Collections;
using UnityEngine;

public class tutorialScript : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialText1;
    [SerializeField] GameObject[] tutorialText2;


    MissionMaster missionMasterScript;

    bool onStageOne = true;

    // Start is called before the first frame update
    void Start()
    {
        missionMasterScript = GetComponent<MissionMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (missionMasterScript.enemyCounter <= 0 && onStageOne == true)
        {
            turnOffFirstStage();
            StartCoroutine(goNext());
            onStageOne = false;
        }
    }

    private IEnumerator goNext()
    {
        yield return new WaitForSeconds(14.5f);

        nextTutorialStage();
    }

    void turnOffFirstStage()
    {
        foreach (var tutorial in tutorialText1)
        {
            tutorial.SetActive(!tutorial.activeSelf);
        }
    }

    void nextTutorialStage()
    {
        
        foreach (var tutorial2 in tutorialText2)
        {
            tutorial2.SetActive(!tutorial2.activeSelf);
        }
    }


}
