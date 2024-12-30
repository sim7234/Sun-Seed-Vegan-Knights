using UnityEngine;

public class DestroyInTime : MonoBehaviour
{
    float timeUntilDie;

    private void Awake()
    {
        timeUntilDie = 5;
    }
    void Update()
    {
        timeUntilDie -= Time.deltaTime;

        if (timeUntilDie < 0 )
        {
            Destroy(gameObject);
        }
    }
}
