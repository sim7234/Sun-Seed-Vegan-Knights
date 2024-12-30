using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class TemporaryTarget : MonoBehaviour
{
    [SerializeField]
    private CameraSystem cameraSystem;

    [SerializeField]
    private GameObject focusPoint; 

    private List<GameObject> playersInside = new List<GameObject>();

    private Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
        {
            if(playersInside.Contains(collision.gameObject) == false)
            {
                cameraSystem.AddTemporaryTarget(collision.gameObject);
                playersInside.Add(collision.gameObject);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.GetComponent<PlayerMovement>())
        //{
        //    playersInside.Remove(collision.gameObject);
        //    if(playersInside.Count <= 0)
        //    {
        //        cameraSystem.RemoveTemporaryTarget(focusPoint);
        //    }
        //}
        collider.enabled = false;
        for (int i = 0; i < playersInside.Count; i++)
        {
            cameraSystem.RemoveTemporaryTarget(playersInside[i]);
            playersInside.RemoveAt(i);
        }
        playersInside.Clear();
        collider.enabled = true;

    }

    
}
