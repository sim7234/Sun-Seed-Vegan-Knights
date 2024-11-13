using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    [SerializeField]
    private GameObject seedType;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Instantiate(seedType, transform.position, Quaternion.identity);
        }
    }
}
