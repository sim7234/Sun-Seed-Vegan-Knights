using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotationZero : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;

    private void Update()
    {
        transform.rotation = new Quaternion(0, 0, -parent.transform.rotation.z,0);
    }
}
