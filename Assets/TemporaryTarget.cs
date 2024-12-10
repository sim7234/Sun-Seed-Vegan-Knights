using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryTarget : MonoBehaviour
{
    [SerializeField]
    private CameraSystem cameraSystem;

    [SerializeField]
    private GameObject focusPoint; 

    private List<GameObject> playersInside = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
        {
            cameraSystem.AddTemporaryTarget(focusPoint);
            playersInside.Add(collision.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            playersInside.Remove(collision.gameObject);
            if(playersInside.Count <= 0)
            {
                cameraSystem.RemoveTemporaryTarget(focusPoint);
            }
        }
    }
}
