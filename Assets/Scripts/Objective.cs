using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField]
    private float timeToComplete;

    private bool hasStarted;

    [SerializeField]
    private TextMeshProUGUI text;

    private void Update()
    {
        if(hasStarted)
        {
            timeToComplete -= Time.deltaTime;

            text.SetText(timeToComplete.ToString("0"));
            if (timeToComplete <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void StartObjectiveEvent()
    {
        hasStarted = true;
    }
}
