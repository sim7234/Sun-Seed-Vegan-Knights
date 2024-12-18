using UnityEngine;

public class ActivateDeathArea : MonoBehaviour
{
    public GameObject targetGameObject;

    public float activationDelay = 0.5f;

    void Start()
    {
        if (targetGameObject != null)
        {
            Invoke("ActivateGameObject", activationDelay);
        }
    }
    void ActivateGameObject()
    {
        targetGameObject.SetActive(true);
    }
}