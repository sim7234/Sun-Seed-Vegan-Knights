using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    public static Screenshake Instance { get; set; }

    Vector3 startPos;

    float magnitude, time, priority;
    float startMagnitude, startTime, startPriority;

    [SerializeField]
    public bool EpelepticFilterOn;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startPos = transform.localPosition;
        EpelepticFilterOn = SaveData.Instance.epelepticFilterOn;
    }

    public void Shake(float magnitude, float time, float priority)
    {
        if (priority > this.priority)
        {
            startMagnitude = magnitude;
            startTime = time;
            this.time = time;
            startPriority = priority;
        }
    }

    public void Shake(float magnitude, float time)
    {
        if (1 > priority)
        {
            startMagnitude = magnitude;
            startTime = time;
            this.time = time;
        }
    }

    public void Stop()
    {
        time = 0;
        magnitude = 0;
        priority = 0;
    }

    private void Update()
    {
        if (!EpelepticFilterOn)
        {
            if (startTime == 0)
            {
                return;
            }

            if (time > 0)
            {
                time -= Time.deltaTime / startTime;
                priority = Mathf.Lerp(startPriority, 0, 1 - (time / startTime));
                magnitude = Mathf.Lerp(startMagnitude, 0, 1 - (time / startTime));
            }
            else
            {
                time = 0;
                magnitude = 0;
                priority = 0;
            }

            if (time > 0)
            {
                transform.localPosition = startPos + (Vector3)Random.insideUnitCircle * magnitude;
            }
            else
            {
                transform.localPosition = startPos;
            }
        }
    }
}
