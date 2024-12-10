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
        if (hasStarted)
        {
            GetComponent<isTarget>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
            //timeToComplete -= Time.deltaTime;

            //text.SetText(timeToComplete.ToString("0"));
            //if (timeToComplete <= 0)
            //{
            //    Destroy(gameObject);
            //}
        }
    }

    public void StartObjectiveEvent()
    {
        hasStarted = true;
    }

    public void onObjectiveDeath()
    {
        SceneManager.LoadScene(0);
    }
}
