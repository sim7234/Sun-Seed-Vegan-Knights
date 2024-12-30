using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour
{
    //[SerializeField]
    //private float timeToComplete;

    private bool hasStarted;

    //[SerializeField]
    //private TextMeshProUGUI text;

    private void Update()
    {


        
    }
    public void StartObjectiveEvent()
    {
        hasStarted = true;
        if (hasStarted)
        {
            GetComponent<isTarget>().enabled = true;
            GetComponent<Collider2D>().enabled = true;

            if(GetComponent<WaterObjective>() != null)
            {
                GetComponent<WaterObjective>().enabled = true;
            }
        }
    }
    public void onObjectiveDeath()
    {
        SceneManager.LoadScene(0);
    }
}
