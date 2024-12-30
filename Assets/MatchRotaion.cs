using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRotaion : MonoBehaviour
{
    [SerializeField]
    private GameObject otherGameObject;
    private void Update()
    {
        transform.localEulerAngles = otherGameObject.transform.localEulerAngles;
    }
}
